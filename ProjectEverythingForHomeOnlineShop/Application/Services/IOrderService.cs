using ProjectEverythingForHomeOnlineShop.Application.DTOs;
using ProjectEverythingForHomeOnlineShop.Application.DTOs.OrderDTOs;
using ProjectEverythingForHomeOnlineShop.Core.Models;
using ProjectEverythingForHomeOnlineShop.Infrastructure;

namespace ProjectEverythingForHomeOnlineShop.Application.Services
{
    public interface IOrderService
    {
        Task<ServiceResult<List<ProductInShopOrderDTO>>> AddNewOrderAsync(string userId, ShopOrderDTO newOrderDTO);
        Task<ServiceResult<ShopOrdersDTO>> GetAllOrdersForOneCustomerAsync(string userId);
    }
}
