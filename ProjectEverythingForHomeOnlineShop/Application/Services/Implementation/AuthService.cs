using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.IdentityModel.Tokens.Jwt;
using ProjectEverythingForHomeOnlineShop.DataAccess.Repositories;
using ProjectEverythingForHomeOnlineShop.Infrastructure.JwtAuthentication;
using ProjectEverythingForHomeOnlineShop.Application.DTOs.IdentetyDTOs.RegisterAdminDTOs;
using ProjectEverythingForHomeOnlineShop.Application.DTOs.IdentetyDTOs.RegisterCustomerDTOs;
using ProjectEverythingForHomeOnlineShop.Application.DTOs.IdentetyDTOs;
using ProjectEverythingForHomeOnlineShop.DataAccess.Persistence.Identity;

namespace ProjectEverythingForHomeOnlineShop.Application.Services.Implementation
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly ICustomerService _customerService;

        private readonly TokenGenerator _tokenGenerator;

        ILogger<AuthService> _logger;


        public AuthService(UserManager<ApplicationUser> userManager,
                            ICustomerService customerService,
                            TokenGenerator tokenGenerator,
                            ILogger<AuthService> logger)
        {
            _userManager = userManager;
            _customerService = customerService;
            _tokenGenerator = tokenGenerator;
            _logger = logger;
        }


        /// <summary>
        /// create new user in Identety's with "user" role for authentication
        /// and new customer with detail data in Customers table for business workflow
        /// </summary>
        public async Task<RegisterCustomerResultDTO> RegisterCustomerAsync(RegisterCustomerDTO registerCustomerDTO)
        {

            var user = new ApplicationUser
            {
                UserName = registerCustomerDTO.UserName,
                Email = registerCustomerDTO.CustomerEmail

            };

            /// using Identety's class UserManager foe creating a new user
            /// password is saved with hashing

            var result = await _userManager.CreateAsync(user, registerCustomerDTO.Password);

            /// if user was created successful
            /// create a new row in customers for this user
            /// asssign role "User"

            if (result.Succeeded)
            {
                await _customerService.AddCustomerAsync(registerCustomerDTO, user.Id);
                await _userManager.AddToRoleAsync(user, "User");
                _logger.LogInformation("Customer is created successful.");
            }

            return new RegisterCustomerResultDTO
            {
                Result = result,
                UserId = user.Id
            };
        }

        /// <summary>
        /// create a new user in Identety with "admin" role for authentication
        /// <return user.Id temporaryPassword> for sending to new admin </return>
        /// </summary>
        /// 
        public async Task<RegisterAdminResultDTO> RegisterAdminAsync(RegisterAdminDTO model)
        {
            var user = new ApplicationUser
            {
                UserName = model.AdminName,
                Email = model.AdminEmail
            };

            var temporaryPassword = "TemporaryPassword1!";

            var result = await _userManager.CreateAsync(user, temporaryPassword);


            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Admin");
                _logger.LogInformation("Admistrator is created successful.");
            }

            return new RegisterAdminResultDTO
            {
                Result = result,
                AdminId = user.Id,
                AdminPassword = temporaryPassword
            };
        }


        /// <summary>
        /// authentication with either "admin" or "user" role
        /// </summary>
        
        public async Task<TokenResponseDTO?> AuthenticateUserAsync(LoginDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.UserEmail);

            /// check e-mail and password for authentication

            if (user != null && await _userManager.CheckPasswordAsync(user, model.UserPassword))
            {
                /// get user's role
                
                var roles = await _userManager.GetRolesAsync(user);

                var role = roles.First();

                _logger.LogInformation("User logged with role:" + role!);

                /// generate token with user's data and role

                var token = _tokenGenerator.GenerateJwtToken(user, role);

                return new TokenResponseDTO
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    Expiration = token.ValidTo,
                    UserID = user.Id
                };
            }
            /// if email or password does not fit
            return null;
        }

        /// <summary>
        /// update password with either "admin" or "user" role
        /// </summary>

        public async Task<IdentityResult> UpdateUserPasswordAsync(string userId, UpdatePasswordDTO model)
        {
            var user = await _userManager.FindByIdAsync(userId);

            var result = await _userManager.ChangePasswordAsync(user, model.CurrentUserPassword, model.NewUserPassword);

            if (result.Succeeded)
            {
                _logger.LogInformation($"User {user.Email} changed password successfully.");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    _logger.LogError($"Error changing password: {error.Description}");
                }
            }

            return result;
        }
    }
}
