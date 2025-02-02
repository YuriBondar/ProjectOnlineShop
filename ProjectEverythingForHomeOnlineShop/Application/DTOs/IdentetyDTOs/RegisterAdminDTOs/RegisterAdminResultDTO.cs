using Microsoft.AspNetCore.Identity;

namespace ProjectEverythingForHomeOnlineShop.Application.DTOs.IdentetyDTOs.RegisterAdminDTOs
{
    public class RegisterAdminResultDTO
    {
        public IdentityResult Result { get; set; } = null!;
        public string AdminId { get; set; } = null!;
        public string AdminPassword { get; set; } = null!;
    }
}
