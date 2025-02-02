using ProjectEverythingForHomeOnlineShop.Application.DTOs;
using ProjectEverythingForHomeOnlineShop.Application.DTOs.OrderDTOs;
using ProjectEverythingForHomeOnlineShop.Core.Models;
using ProjectEverythingForHomeOnlineShop.Infrastructure;

namespace ProjectEverythingForHomeOnlineShop.Application.Services
{
    public interface IProductService
    {
        Task<ServiceResult> AddNewProductAsync(CreateProductDTO model);

        Task<ServiceResult> UpdateProductAsync(int productId, UpdateProductDTO updateProduct);

        Task<ServiceResult<List<Product>>> GetAllProductAsync();
        Task<Product?> GetProductByIDAsync(int productId);

        Task<List<ProductInShopOrderDTO>> GetLowStockProducts(List<ProductInShopOrderDTO> productsInNewOrderDTO);
        Task UpdateStockAfterOrderAsync(List<ShopOrderProduct> newShopOrderProductList);
    }
}
