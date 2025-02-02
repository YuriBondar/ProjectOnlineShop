using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectEverythingForHomeOnlineShop.Application.DTOs.OrderDTOs;
using ProjectEverythingForHomeOnlineShop.Application.Services;

namespace ProjectEverythingForHomeOnlineShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        /// <summary>
        /// Create a new order.
        /// A new order should have one product at least.
        /// </summary>

        [HttpPost("{userId}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> AddNewOrder(string userId, ShopOrderDTO newOrderDTO)
        {
            var resultAddNewOrder = await _orderService.AddNewOrderAsync(userId, newOrderDTO);

            if (resultAddNewOrder.Success)
                return Ok(new { Message = resultAddNewOrder.Message });

            else
            {
                if (resultAddNewOrder.Data == null)
                    return BadRequest(new { Message = resultAddNewOrder.Message });
                else
                    return BadRequest(new { Message = resultAddNewOrder.Message, Data = resultAddNewOrder.Data});
            }
        }

        /// <summary>
        /// Get all orders with status and list of products for particular customer by userID
        /// </summary>
        [HttpGet("getAllFor{userId}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetAllOrdersForOneCustomer(string userId)
        {
            var resultGetAllOrdersForOneCustomer = await _orderService.GetAllOrdersForOneCustomerAsync(userId);

            if (resultGetAllOrdersForOneCustomer.Success)
                return Ok(new { Message = resultGetAllOrdersForOneCustomer.Message, Data = resultGetAllOrdersForOneCustomer.Data });

            else
            {            
                return BadRequest(new { Message = resultGetAllOrdersForOneCustomer.Message });      
            }
        }
    }
}
