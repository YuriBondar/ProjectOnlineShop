using System.ComponentModel.DataAnnotations;
using ProjectEverythingForHomeOnlineShop.Application.DTOs;
using ProjectEverythingForHomeOnlineShop.Application.DTOs.OrderDTOs;
using ProjectEverythingForHomeOnlineShop.Core.Models;
using ProjectEverythingForHomeOnlineShop.DataAccess.Repositories;
using ProjectEverythingForHomeOnlineShop.Infrastructure;

namespace ProjectEverythingForHomeOnlineShop.Application.Services.Implementation
{
    public class ProductService : IProductService
    {
        IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }


        /// <summary>
        /// add a new product to database
        /// </summary>
        
        public async Task<ServiceResult> AddNewProductAsync(CreateProductDTO model)
        {
            try
            {           
                /// check if SCU number does not already exist in database
                
                if (await _productRepository.IsProductSCUOnStock(model.ProductSCU))
                    return ServiceResult.ErrorResult("There is product on stock with the same SCU number");

                var product = new Product
                {
                    ProductSCU = model.ProductSCU,
                    ProductName = model.ProductName,
                    Category = model.Category,
                    UnitPrice = model.UnitPrice,
                    StockQuantity = model.StockQuantity
                };

                await _productRepository.AddProductToStockAsync(product);

                return ServiceResult.SuccessResult();
            }
            catch (Exception ex)
            {
                return ServiceResult.ErrorResult(ex.Message);
            }
        }


        /// <summary>
        /// update any pruduct's properties 
        /// </summary>

        public async Task<ServiceResult> UpdateProductAsync(int productId, UpdateProductDTO updateProduct)
        {
            try
            {
                /// find the product whose properties need to be updated
                
                var currentProduct = await _productRepository.FindProductByIdAsync(productId);

                if (currentProduct == null)
                    return ServiceResult.ErrorResult($"There is product on stock with Id {productId}");

                /// if product SCU needs to be updated, check if does not already exist a new SCU namber in database
                
                if (await _productRepository.IsProductSCUOnStock(updateProduct.ProductSCU))
                    return ServiceResult.ErrorResult("There is product on stock with the same SCU number");

                /// update all properties which are not null in data we recieved for updating

                if (updateProduct.ProductSCU != null)
                    currentProduct.ProductSCU = updateProduct.ProductSCU;

                if (updateProduct.ProductName != null)
                    currentProduct.ProductName = updateProduct.ProductName;

                if (updateProduct.Category != null)
                    currentProduct.Category = updateProduct.Category;

                if (updateProduct.UnitPrice != null)
                    currentProduct.UnitPrice = (decimal)updateProduct.UnitPrice;

                if (updateProduct.StockQuantity != null)
                    currentProduct.StockQuantity = (int)updateProduct.StockQuantity;


                await _productRepository.UpdateProductInStockAsync(currentProduct);

                return ServiceResult.SuccessResult();
            }
            catch (Exception ex)
            {
                return ServiceResult.ErrorResult(ex.Message);

            }
        }


        /// <summary>
        /// get information about all products
        /// </summary>
        
        public async Task<ServiceResult<List<Product>>> GetAllProductAsync()
        {
            try
            {
                var allProducts = await _productRepository.GetAllProductsAsync();

                return ServiceResult<List<Product>>.SuccessResult(allProducts);
            }
            catch (Exception ex)
            {
                return ServiceResult<List<Product>>.ErrorResult(ex.Message);
            }
        }

        /// <summary>
        /// get information about product by productID
        /// </summary>
        public async Task<Product?> GetProductByIDAsync(int productId)
        {
            try
            {

                var product = await _productRepository.FindProductByIdAsync(productId);

                return product;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// get list of products
        /// whose quantity on stock are less then quantity in order
        /// </summary>

        public async Task<List<ProductInShopOrderDTO>?> GetLowStockProducts(List<ProductInShopOrderDTO> productsInNewOrderDTO)
        {
            List<ProductInShopOrderDTO> lowStockProducts = new List<ProductInShopOrderDTO>();

            foreach (var product in productsInNewOrderDTO)
            {
                {
                    Product? productOnStock = await _productRepository.FindProductByIdAsync(product.ProductID);
                    if (productOnStock.StockQuantity < product.ProductQuantity)
                        lowStockProducts.Add(product);
                }
            }
            if (lowStockProducts.Count > 0)
                return lowStockProducts;
            else
                return null;
        }


        /// <summary>
        /// update stock quantity after order acception
        /// </summary>
        public async Task UpdateStockAfterOrderAsync(List<ShopOrderProduct> newShopOrderProductList)
        {
            await _productRepository.UpdateStockAfterOrderAsync(newShopOrderProductList);
        }
    }
}
