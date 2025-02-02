using ProjectEverythingForHomeOnlineShop.Core.Models;

namespace ProjectEverythingForHomeOnlineShop.DataAccess.Repositories
{
    public interface IOrderRepository
    {
        Task AddNewOrderAsync(ShopOrder order);

        Task<List<ShopOrder>> GetOrdersWithProductsByCustomerID(int customerID);

        Task<ShopOrder> GetOrderWithProductsByIdAsync(int orderID);

        Task<List<int>> GetOrderIdsForCustomerAsync(int customerID);
    }
}
