using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectEverythingForHomeOnlineShop.Application.DTOs.IdentetyDTOs;
using ProjectEverythingForHomeOnlineShop.Application.DTOs.IdentetyDTOs.RegisterAdminDTOs;
using ProjectEverythingForHomeOnlineShop.Application.DTOs.IdentetyDTOs.RegisterCustomerDTOs;
using ProjectEverythingForHomeOnlineShop.Application.Services;


namespace ProjectEverythingForHomeOnlineShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Register a new user with "user" role.
        /// Checking all users properties with properties validation and RegEx.
        /// Checking if already exist email in database.
        /// If any validation fails, an error message is returned 
        /// with a description of the issue in the data sent by the client.
        /// <value UserId> send to user in respond</value>
        /// </summary>
        
        [HttpPost("registerCustomer")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterNewCustomer([FromBody] RegisterCustomerDTO model)
        {
            /// check data from customer
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var registerUserResultDTO = await _authService.RegisterCustomerAsync(model);
            if (!registerUserResultDTO.Result.Succeeded)
            {
                return BadRequest(registerUserResultDTO.Result.Errors);
            }

            return Ok(new { Message = "User registered successfully",
                            UserId = registerUserResultDTO.UserId
            });
        }

        /// <summary>
        /// Register a new user with "admin" role.
        /// <value AdminId> send to user in respond.</value>
        /// <value TemporaryPassword> send to user in respond. A new admin gets a temporary password.</value>
        /// </summary>
        
        [Authorize(Roles = "Admin")]
        [HttpPost("registerAdmin")]
        public async Task<IActionResult> RegisterNewAdmin([FromBody] RegisterAdminDTO model)
        {
            

            var registerUserResultDTO = await _authService.RegisterAdminAsync(model);
            
            if (!registerUserResultDTO.Result.Succeeded)
            {
                return BadRequest(registerUserResultDTO.Result.Errors);
            }

            return Ok(new
            {
                Message = "User registered successfully",
                AdminId = registerUserResultDTO.AdminId,
                TemporaryPassword = registerUserResultDTO.AdminPassword
            });
        }


        /// <summary>
        /// Authentication with either 'admin' or 'user' role.
        /// <value token> A user gets a access token</value>
        /// </summary>
        
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginUser([FromBody] LoginDTO loginDTO)
        { 
            var token = await _authService.AuthenticateUserAsync(loginDTO);
            if (token == null)
            { 
                return Unauthorized("Invalid credentials");
            }
            return Ok(new { token });
        }


        /// <summary>
        /// Change password for either 'admin' or 'user' role.
        /// For changing are needed old and new passwords.
        /// </summary>
        
        [Authorize(Roles = "Admin,User")]
        [HttpPost("updatePassword/{userID}")]

        public async Task<IActionResult> UpdateUserPassword(string userId, [FromBody] UpdatePasswordDTO model)
        {
            var changePasswordResult = await _authService.UpdateUserPasswordAsync(userId, model);

            if (!changePasswordResult.Succeeded)
            {
                return BadRequest(changePasswordResult.Errors);
            }

            return Ok(new { message = "Password changed successfully" });
        }
    }
}
