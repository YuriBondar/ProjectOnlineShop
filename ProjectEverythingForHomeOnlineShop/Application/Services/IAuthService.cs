using Microsoft.AspNetCore.Identity;
using ProjectEverythingForHomeOnlineShop.Application.DTOs.IdentetyDTOs;
using ProjectEverythingForHomeOnlineShop.Application.DTOs.IdentetyDTOs.RegisterAdminDTOs;
using ProjectEverythingForHomeOnlineShop.Application.DTOs.IdentetyDTOs.RegisterCustomerDTOs;

namespace ProjectEverythingForHomeOnlineShop.Application.Services
{
    public interface IAuthService
    {
        Task<RegisterCustomerResultDTO> RegisterCustomerAsync(RegisterCustomerDTO model);

        Task<RegisterAdminResultDTO> RegisterAdminAsync(RegisterAdminDTO model);

        Task<TokenResponseDTO?> AuthenticateUserAsync(LoginDTO model);

        Task<IdentityResult> UpdateUserPasswordAsync(string userId, UpdatePasswordDTO model);
    }
}
