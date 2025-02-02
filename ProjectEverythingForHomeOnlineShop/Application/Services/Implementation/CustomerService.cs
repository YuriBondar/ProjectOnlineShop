using Microsoft.EntityFrameworkCore;
using ProjectEverythingForHomeOnlineShop.Application.DTOs;
using ProjectEverythingForHomeOnlineShop.Application.DTOs.IdentetyDTOs;
using ProjectEverythingForHomeOnlineShop.Application.DTOs.IdentetyDTOs.RegisterCustomerDTOs;
using ProjectEverythingForHomeOnlineShop.Core.Models;
using ProjectEverythingForHomeOnlineShop.DataAccess.Repositories;
using ProjectEverythingForHomeOnlineShop.DataAccess.Repositories.Implementation;
using ProjectEverythingForHomeOnlineShop.Infrastructure;

namespace ProjectEverythingForHomeOnlineShop.Application.Services.Implementation
{
    public class CustomerService : ICustomerService
    {
        ICustomerRepository _customerRepository;
        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        /// <summary>
        /// add a new customer with userID for regestrared user 
        /// </summary>
   
        public async Task AddCustomerAsync(RegisterCustomerDTO model, string userID)
        {
            var customer = new Customer
            {

                IdentityUserID = userID,
                CustomerEmail = model.CustomerEmail,
                CustomerLastName = model.CustomerLastName,
                CustomerFirstName = model.CustomerFirstName,
                CustomerCity = model.CustomerCity,
                CustomerStreet = model.CustomerStreet,
                CustomerHausNumber = model.CustomerHausNumber,
                CustomerPostIndex = model.CustomerPostIndex,
                CustomerPhone = model.CustomerPhone
            };
            await _customerRepository.AddCustomerAsync(customer);
        }


        /// <summary>
        /// get information about customer by userID 
        /// </summary>
       
        public async Task<Customer?> GetCustomerByUserIDAsync(string userId)
        {
            return await _customerRepository.GetCustomerByUserID(userId);

        }

        /// <summary>
        /// get information about all customers
        /// </summary>
        
        public async Task<ServiceResult<List<Customer>>> GetAllCustomersAsync()
        {
            try
            {
                var allCustomers = await _customerRepository.GetAllCustomersAsync();

                /// check if any customer is found
              
                if (allCustomers.Count != 0)
                {
                    return ServiceResult<List<Customer>>.SuccessResult(allCustomers);
                }
                else
                    return ServiceResult<List<Customer>>.SuccessResult(allCustomers, "There are no customers in database");

            }
            catch (Exception ex)
            {
                return ServiceResult<List<Customer>>.ErrorResult(ex.Message);

            }
        }


        /// <summary>
        /// search all customers by user query
        /// </summary>
        
        public async Task<ServiceResult<List<Customer>>> SearchCustomersAsync(SearchCustomerDTO searchCustomerDTO)
        {
            try
            {
                /// check if is at least one field in user's query
                
                if (IsEmptySearchQuery(searchCustomerDTO))
                    return ServiceResult<List<Customer>>.ErrorResult("Empty query");

                var foundCustomers = await _customerRepository.SearchCustomersAsync(searchCustomerDTO);

                /// check if at least one customer for query was found
                
                if (foundCustomers.Count != 0)
                {
                    return ServiceResult<List<Customer>>.SuccessResult(foundCustomers);
                }
                else
                    return ServiceResult<List<Customer>>.SuccessResult(foundCustomers, "There are no customers in the database for this query.");

            }
            catch (Exception ex)
            {
                return ServiceResult<List<Customer>>.ErrorResult(ex.Message);

            }
        }


        /// <summary>
        /// check if all fields in user's query are empty
        /// </summary>
        /// 
        public bool IsEmptySearchQuery(SearchCustomerDTO customerSearchDto)
        {
            if (customerSearchDto.CustomerID == null && customerSearchDto.CustomerID == 0 &&
                string.IsNullOrEmpty(customerSearchDto.IdentityUserID) && 
                string.IsNullOrEmpty(customerSearchDto.CustomerFirstName) &&
                string.IsNullOrEmpty(customerSearchDto.CustomerLastName) &&
                string.IsNullOrEmpty(customerSearchDto.CustomerStreet) &&
                string.IsNullOrEmpty(customerSearchDto.CustomerHausNumber) &&
                string.IsNullOrEmpty(customerSearchDto.CustomerPostIndex) &&
                string.IsNullOrEmpty(customerSearchDto.CustomerCity) &&
                string.IsNullOrEmpty(customerSearchDto.CustomerEmail))
            {
                return true;
            }
            return false;
        }
    }
}
