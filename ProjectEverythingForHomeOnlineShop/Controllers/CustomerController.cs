
using System.ComponentModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectEverythingForHomeOnlineShop.Application.DTOs;
using ProjectEverythingForHomeOnlineShop.Application.DTOs.IdentetyDTOs;
using ProjectEverythingForHomeOnlineShop.Application.Services;
using ProjectEverythingForHomeOnlineShop.Application.Services.Implementation;
using ProjectEverythingForHomeOnlineShop.DataAccess.Persistence;

namespace ProjectEverythingForHomeOnlineShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        /// <summary>
        /// Get information about all customers
        /// </summary>
        
        [Authorize(Roles = "Admin")]
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllCustomers()
        {
            var getAllCustomersResult = await _customerService.GetAllCustomersAsync();
            
            if (getAllCustomersResult.Success)
                return Ok(new { Message = getAllCustomersResult.Message, Customers = getAllCustomersResult.Data });

            else
                return BadRequest(new { Message = getAllCustomersResult.Message });
        }


        /// <summary>
        /// Get information about particulat customer by userID for this customer
        /// </summary>
        
        [Authorize(Roles = "User, Admin")]
        [HttpGet("get/{userID}")]
        public async Task<IActionResult> GetCustomerByID(string userID)
        {
            var foundCustomer = await _customerService.GetCustomerByUserIDAsync(userID);

            if (foundCustomer == null)
                return NotFound();
            return Ok(foundCustomer);
        }


        /// <summary>
        /// Searching for a customer using any number and combination of their properties. 
        /// For string fields, a match is considered positive if the field equals or contains the search string from the request.
        /// </summary>
                
        [HttpGet("search")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> SearchCustomer([FromQuery] SearchCustomerDTO model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                              .Select(e => e.ErrorMessage);
                return BadRequest(new
                {
                    Message = "Validation failed",
                    Errors = errors
                });
            }

            var searchCustomerResult = await _customerService.SearchCustomersAsync(model);

            if (searchCustomerResult.Success)
                return Ok(new { Message = searchCustomerResult.Message, Customers = searchCustomerResult.Data });

            else
                return BadRequest(new { Message = searchCustomerResult.Message });
        }
    }
}
