using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ProjectEverythingForHomeOnlineShop.Application.DTOs.OrderDTOs;
using ProjectEverythingForHomeOnlineShop.Core.Models;
using ProjectEverythingForHomeOnlineShop.DataAccess.Persistence;
using ProjectEverythingForHomeOnlineShop.DataAccess.Repositories;
using ProjectEverythingForHomeOnlineShop.Infrastructure;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProjectEverythingForHomeOnlineShop.Application.Services.Implementation
{
    public class OrderService : IOrderService
    {
        IOrderRepository _orderRepository;
        IProductService _productService;
        ICustomerService _customerService;
        ILogger<AuthService> _logger;
        OnlineShopMySQLDatabaseContext _dbcontext;
        public OrderService(OnlineShopMySQLDatabaseContext dbcontext,
                            IOrderRepository orderRepository, 
                            IProductService productService,
                            ICustomerService customerService,
                            ILogger<AuthService> logger) 
        {
            _orderRepository = orderRepository;
            _productService = productService;
            _customerService = customerService;
            _logger = logger;
            _dbcontext = dbcontext;
        }



        /// <summary>
        /// create and add a new order
        /// </summary>
        
        public async Task<ServiceResult<List<ProductInShopOrderDTO>>> AddNewOrderAsync(string userId, ShopOrderDTO newOrderDTO)
        {

            using var transaction = await _dbcontext.Database.BeginTransactionAsync();
          
            try
            {
                /// check if some products are not enough on stock
                
                var lowStockProducts = await _productService.GetLowStockProducts(newOrderDTO.Products);

                /// order is not executed, sent list of products with lack of quantuity to customer
                
                if (lowStockProducts != null)
                {
                    _logger.LogError($"Not enough these products in stock.");
                    return ServiceResult<List<ProductInShopOrderDTO>>.ErrorResult(lowStockProducts,
                                                                                "Not enough these products in stock.");
                    
                }

                /// get the customer with userId was recieved 
                
                var customer = await _customerService.GetCustomerByUserIDAsync(userId);
                if (customer == null)
                {
                    _logger.LogError($"There is no customer ib Databaase with this userID");
                    return ServiceResult <List<ProductInShopOrderDTO>>.ErrorResult("There is no customer ib Databaase with this userID");
                }


                /// make and fill list with all products for a new order
                
                List<ShopOrderProduct> newShopOrderProductList = new List<ShopOrderProduct>();

                foreach (var product in newOrderDTO.Products)                
                    newShopOrderProductList.Add(new ShopOrderProduct
                    {
                        ProductID = product.ProductID,
                        ProductOrderQuantity = product.ProductQuantity
                    });

                /// create new order with products for saving it to database
                
                ShopOrder newShopOrder = new ShopOrder 
                { 
                    CustomerID = customer!.CustomerID,
                    ShopOrderStatusID = 1,
                    ShopOrderAcceptedAt = DateTime.Now,
                    ShopOrderProducts = newShopOrderProductList
                };

                await _orderRepository.AddNewOrderAsync(newShopOrder);

                /// reduce the number of products in stock by the quantity that was ordered
                
                await _productService.UpdateStockAfterOrderAsync(newShopOrderProductList);
                await transaction.CommitAsync();

                return ServiceResult<List<ProductInShopOrderDTO>>.SuccessResult();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return ServiceResult<List<ProductInShopOrderDTO>>.ErrorResult(ex.Message);
            }
        }


        /// <summary>
        /// get all orders with products for customer by userID
        /// </summary>

        public async Task<ServiceResult<ShopOrdersDTO>> GetAllOrdersForOneCustomerAsync(string userId)
        {
            try
            {
                /// get customer id or return error if customer id does not excist
                
                var customer = await _customerService.GetCustomerByUserIDAsync(userId);
                if (customer == null)
                {
                    _logger.LogError($"There is no customer ib Databaase with this userID");
                    return ServiceResult<ShopOrdersDTO>.ErrorResult("There is no customer ib Databaase with this userID");
                }

                /// get list of order's id for particular customer by customerid

                List<int> orderIds = await _orderRepository.GetOrderIdsForCustomerAsync(customer.CustomerID);

                /// get all orders with complete information for all orderids
                
                ShopOrdersDTO shopOrdersDTO = new ShopOrdersDTO();
               
                foreach (int orderId in orderIds)
                    shopOrdersDTO.ShopOrders.Add(await GetOrderWithProductsByIDAsync(orderId));

                return ServiceResult<ShopOrdersDTO>.SuccessResult(shopOrdersDTO);
            }
            catch (Exception ex)
            {
                return ServiceResult<ShopOrdersDTO>.ErrorResult(ex.Message);
            }
        }


        /// <summary>
        /// get information about order and its products ba orderID
        /// </summary>

        public async Task<ShopOrderDTO> GetOrderWithProductsByIDAsync(int orderID)
        {
            try
            {

                var foundOrder = await _orderRepository.GetOrderWithProductsByIdAsync(orderID);

                var products = new List<ProductInShopOrderDTO>();


                /// getting information about all products in order
                /// generation a list with products

                foreach (ShopOrderProduct product in foundOrder.ShopOrderProducts)
                {
                    var productName = (await _productService.GetProductByIDAsync(product.ProductID))!.ProductName;

                    products.Add(ShopOrderProduct.ToProductInShopOrderDTO(product, productName));
                }

                /// creating data with information about order and its products for sending 

                return new ShopOrderDTO
                {
                    ShopOrderID = foundOrder.ShopOrderID,
                    ShopOrderStatus = foundOrder.ShopOrderStatus.ShopOrderStatusName,
                    Products = products
                };
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
