using Microsoft.EntityFrameworkCore;
using ProjectEverythingForHomeOnlineShop.Application.DTOs;
using ProjectEverythingForHomeOnlineShop.Application.Services.Implementation;
using ProjectEverythingForHomeOnlineShop.Core.Models;
using ProjectEverythingForHomeOnlineShop.DataAccess.Persistence;

namespace ProjectEverythingForHomeOnlineShop.DataAccess.Repositories.Implementation
{
    public class CustomerRepository : ICustomerRepository
    {
        OnlineShopMySQLDatabaseContext _dbcontext;
        ILogger<AuthService> _logger;

        public CustomerRepository(OnlineShopMySQLDatabaseContext dbcontext, 
                                  ILogger<AuthService> logger)
        {
            _dbcontext = dbcontext;
            _logger = logger;
        }


        /// <summary>
        /// add a new customer todatabase 
        /// </summary>

        public async Task AddCustomerAsync(Customer customer)
        {
            await _dbcontext.Customers.AddAsync(customer);
            await _dbcontext.SaveChangesAsync();
        }

        /// <summary>
        /// add a new customer todatabase
        /// <return null> if customer with such userId does not exist in database </return> 
        /// </summary>
        
        public async Task<Customer?> GetCustomerByUserID(string userId)
        {
            var customer = await _dbcontext.Customers.FirstOrDefaultAsync(c => c.IdentityUserID == userId);
            return customer;
        }

        /// <summary>
        /// get all customers from database
        /// </summary>
        public async Task<List<Customer>> GetAllCustomersAsync()
        {
            try
            {
                return await _dbcontext.Customers.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during getting all products from database");
                throw;
            }
        }


        /// <summary>
        ///  retrieve customers who meet the criteria of the request based on the model
        /// </summary>
        public async Task<List<Customer>> SearchCustomersAsync(SearchCustomerDTO searchCustomerDTO) 
        {
            try
            {
                var query = _dbcontext.Customers.AsQueryable();

                if (!(searchCustomerDTO.CustomerID == null || searchCustomerDTO.CustomerID == 0))
                    query = query.Where(c => c.CustomerID == searchCustomerDTO.CustomerID);  

                if (!string.IsNullOrEmpty(searchCustomerDTO.CustomerFirstName))
                    query = query.Where(c => c.CustomerFirstName.Contains(searchCustomerDTO.CustomerFirstName));

                if (!string.IsNullOrEmpty(searchCustomerDTO.CustomerLastName))
                    query = query.Where(c => c.CustomerLastName.Contains(searchCustomerDTO.CustomerLastName));

                if (!string.IsNullOrEmpty(searchCustomerDTO.CustomerStreet))
                    query = query.Where(c => c.CustomerStreet.Contains(searchCustomerDTO.CustomerStreet));

                if (!string.IsNullOrEmpty(searchCustomerDTO.CustomerHausNumber))
                    query = query.Where(c => c.CustomerHausNumber.Contains(searchCustomerDTO.CustomerHausNumber));

                if (!string.IsNullOrEmpty(searchCustomerDTO.CustomerPostIndex))
                    query = query.Where(c => c.CustomerPostIndex.Contains(searchCustomerDTO.CustomerPostIndex));

                if (!string.IsNullOrEmpty(searchCustomerDTO.CustomerCity))
                    query = query.Where(c => c.CustomerCity.Contains(searchCustomerDTO.CustomerCity));

                if (!string.IsNullOrEmpty(searchCustomerDTO.CustomerEmail))
                    query = query.Where(c => c.CustomerEmail.Contains(searchCustomerDTO.CustomerEmail));

                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during searching customers in database");
                throw;

            }
        }

    }
}
