using ProjectEverythingForHomeOnlineShop.Application.DTOs;
using ProjectEverythingForHomeOnlineShop.Application.DTOs.IdentetyDTOs;
using ProjectEverythingForHomeOnlineShop.Application.DTOs.IdentetyDTOs.RegisterCustomerDTOs;
using ProjectEverythingForHomeOnlineShop.Core.Models;
using ProjectEverythingForHomeOnlineShop.Infrastructure;

namespace ProjectEverythingForHomeOnlineShop.Application.Services
{
    public interface ICustomerService
    {
        Task AddCustomerAsync(RegisterCustomerDTO model, string userID);

        Task<Customer?> GetCustomerByUserIDAsync(string userId);

        Task<ServiceResult<List<Customer>>> GetAllCustomersAsync();

        Task<ServiceResult<List<Customer>>> SearchCustomersAsync(SearchCustomerDTO model);
    }
}
