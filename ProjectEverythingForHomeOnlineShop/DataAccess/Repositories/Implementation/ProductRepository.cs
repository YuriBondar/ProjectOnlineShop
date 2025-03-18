using Microsoft.EntityFrameworkCore;
using ProjectEverythingForHomeOnlineShop.Application.DTOs;
using ProjectEverythingForHomeOnlineShop.Application.Services.Implementation;
using ProjectEverythingForHomeOnlineShop.Core.Models;
using ProjectEverythingForHomeOnlineShop.DataAccess.Persistence;
using ProjectEverythingForHomeOnlineShop.Infrastructure;

namespace ProjectEverythingForHomeOnlineShop.DataAccess.Repositories.Implementation
{
    public class ProductRepository : IProductRepository
    {
        OnlineShopMySQLDatabaseContext _dbcontext;
        ILogger<AuthService> _logger;

        public ProductRepository(OnlineShopMySQLDatabaseContext dbcontext,
                                 ILogger<AuthService> logger)
        {
            _dbcontext = dbcontext;
            _logger = logger;
        }

        /// <summary>
        /// check if does already exist product with some SCU in database
        /// </summary>
        
        public async Task<bool> IsProductSCUOnStock(string productSCU)
        {
            try
            {
                var isProductSCUOnStock = await _dbcontext.Products.FirstOrDefaultAsync(p => p.ProductSCU == productSCU);

                if (isProductSCUOnStock == null)
                    return false;

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during searchin SCU number in Database");
                throw;
            }
        }


        /// <summary>
        /// add prodact to database
        /// </summary>

        public async Task AddProductToStockAsync(Product model)
        {
            try
            {
                await _dbcontext.Products.AddAsync(model);
                await _dbcontext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during adding new product to database");
                throw;
            }
        }


        /// <summary>
        /// find product in database by productID
        /// </summary>

        public async Task<Product?> FindProductByIdAsync(int productId)
        {
            try
            {
                var product = await _dbcontext.Products.FirstOrDefaultAsync(p => p.ProductID == productId);

                return product;
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "Error during searchin product ID in Database");
                throw;
            }
        }


        /// <summary>
        /// update all product's properties in database
        /// </summary>

        public async Task UpdateProductInStockAsync(Product updatedProduct)
        {
            try
            {
                await _dbcontext.Products
                                .Where(p => p.ProductID == updatedProduct.ProductID)
                                .ExecuteUpdateAsync(setters => setters
                                    .SetProperty(p => p.ProductSCU, updatedProduct.ProductSCU)
                                    .SetProperty(p => p.ProductName, updatedProduct.ProductName)
                                    .SetProperty(p => p.Category, updatedProduct.Category)
                                    .SetProperty(p => p.UnitPrice, updatedProduct.UnitPrice)
                                    .SetProperty(p => p.StockQuantity, updatedProduct.StockQuantity)
                                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during updating product to database");
                throw;
            }
        }


        /// <summary>
        /// get all products from database
        /// </summary>

        public async Task<List<Product>> GetAllProductsAsync()
        {
            try
            {
                return await _dbcontext.Products.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during getting all products from database");
                throw;
            }
        }


        /// <summary>
        /// get all products from database
        /// </summary>

        public async Task UpdateStockAfterOrderAsync(List<ShopOrderProduct> newShopOrderProductList)
        {
            try
            {
                /// select all products from database with pruductIds from order
                var productIds = newShopOrderProductList.Select(p => p.ProductID).ToList();

                /// get all products with selected ids
                var productsOnStock = await _dbcontext.Products
                    .Where(p => productIds.Contains(p.ProductID))
                    .ToListAsync();

                /// reduce the stock quantity by the amount ordered for the corresponding product.
                foreach (var orderProduct in newShopOrderProductList)
                {
                    var product = productsOnStock.FirstOrDefault(p => p.ProductID == orderProduct.ProductID);
                   
                    product!.StockQuantity -= orderProduct.ProductOrderQuantity; 
                }

                await _dbcontext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new InvalidOperationException("Update conflict. Please try again.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating stock after order.");
                throw;
            }
        }
    }
}
