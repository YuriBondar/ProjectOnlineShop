using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectEverythingForHomeOnlineShop.Application.DTOs;
using ProjectEverythingForHomeOnlineShop.Application.Services;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace ProjectEverythingForHomeOnlineShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Add a new product to database
        /// </summary>

        [HttpPost("add")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddNewProduct(CreateProductDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Message = "ModelState not Valid" });
            }

            var result = await _productService.AddNewProductAsync(model);

            if (result.Success)
                return Ok(new { Message = result.Message });

            else
                return BadRequest(new { Message = result.Message });
        }


        /// <summary>
        /// Update information about product. A productID is required. 
        /// Any number of product properties can be updated.
        /// The update data is validated for correct format.
        /// </summary>

        [HttpPost("update/{productId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateProduct(int productId, [FromBody] UpdateProductDTO model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                                       .SelectMany(v => v.Errors)
                                       .Select(e => e.ErrorMessage);
                return BadRequest(new
                {
                    Message = "Validation failed",
                    Errors = errors
                });
            }

            var result = await _productService.UpdateProductAsync(productId, model);

            if (result.Success)
                return Ok(new { Message = result.Message });

            else
                return BadRequest(new { Message = result.Message });
        }

        /// <summary>
        /// Get information about all products in database. 
        /// </summary>

        [HttpGet("getAll")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllProducts()
        {
            
            var result = await _productService.GetAllProductAsync();

            if (result.Success)
                return Ok(new { Message = result.Message, Products = result.Data });

            else
                return BadRequest(new { Message = result.Message });
        }
    }
}
