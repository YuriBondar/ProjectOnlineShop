using ProjectEverythingForHomeOnlineShop.Core.Models;

namespace ProjectEverythingForHomeOnlineShop.DataAccess.Repositories
{
    public interface IProductRepository
    {
        Task<bool> IsProductSCUOnStock(string productSCU);

        Task AddProductToStockAsync(Product model);

        Task<Product?> FindProductByIdAsync(int productId);

        Task UpdateProductInStockAsync(Product updatedProduct);

        Task<List<Product>> GetAllProductsAsync();

        Task UpdateStockAfterOrderAsync(List<ShopOrderProduct> newShopOrderProductList);
    }
        
}
