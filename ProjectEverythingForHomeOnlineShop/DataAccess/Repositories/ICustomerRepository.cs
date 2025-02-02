using ProjectEverythingForHomeOnlineShop.Application.DTOs;
using ProjectEverythingForHomeOnlineShop.Core.Models;

namespace ProjectEverythingForHomeOnlineShop.DataAccess.Repositories
{
    public interface ICustomerRepository
    {
        Task AddCustomerAsync(Customer customer);

        Task<Customer?> GetCustomerByUserID(string userId);

        Task<List<Customer>> GetAllCustomersAsync();

        Task<List<Customer>> SearchCustomersAsync(SearchCustomerDTO searchCustomerDTO);
    }
}
