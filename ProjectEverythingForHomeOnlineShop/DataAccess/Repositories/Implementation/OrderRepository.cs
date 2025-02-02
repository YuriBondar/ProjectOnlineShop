using Microsoft.EntityFrameworkCore;
using ProjectEverythingForHomeOnlineShop.Application.Services.Implementation;
using ProjectEverythingForHomeOnlineShop.Core.Models;
using ProjectEverythingForHomeOnlineShop.DataAccess.Persistence;

namespace ProjectEverythingForHomeOnlineShop.DataAccess.Repositories.Implementation
{
    public class OrderRepository : IOrderRepository
    {
        OnlineShopMySQLDatabaseContext _dbcontext;
        ILogger<AuthService> _logger;

        public OrderRepository(OnlineShopMySQLDatabaseContext dbcontext,
                                 ILogger<AuthService> logger)
        {
            _dbcontext = dbcontext;
            _logger = logger;
        }

        /// <summary>
        /// add new order to database
        /// </summary>
  
        public async Task AddNewOrderAsync(ShopOrder order)
        {
            try
            {
                await _dbcontext.ShopOrders.AddAsync(order);
                await _dbcontext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during adding new order to database");
                throw;
            }
        }

        /// <summary>
        /// get all orders from otders table joined with orderProduct and orderStatus tables
        /// for particular customer
        /// </summary>

        public async Task<List<ShopOrder>> GetOrdersWithProductsByCustomerID(int customerID)
        {
            try
            {
                return await _dbcontext.ShopOrders
                                       .Where(so => so.CustomerID == customerID)
                                       .Include(so => so.ShopOrderProducts)
                                       .Include(so => so.ShopOrderStatus)
                                       .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during selecting orders with products from database");
                throw;
            }
        }

        /// <summary>
        /// get all order and its pruducts from database by orderID
        /// </summary>

        public async Task<ShopOrder?> GetOrderWithProductsByIdAsync(int orderID)
        {
            try
            {
                return await _dbcontext.ShopOrders
                                       .Where(so => so.ShopOrderID == orderID)
                                       .Include(so => so.ShopOrderProducts)
                                       .Include(so => so.ShopOrderStatus)
                                       .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during selecting order with products from database");
                throw;
            }
        }


        /// <summary>
        /// get all orderIDs for particular customer
        /// </summary>

        public async Task<List<int>> GetOrderIdsForCustomerAsync(int customerID)
        {
            try
            {
                return await _dbcontext.ShopOrders
                                       .Where(so => so.CustomerID == customerID)
                                       .Select(so => so.ShopOrderID)
                                       .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during selecting order with products from database");
                throw;
            }
        }
    }
}
