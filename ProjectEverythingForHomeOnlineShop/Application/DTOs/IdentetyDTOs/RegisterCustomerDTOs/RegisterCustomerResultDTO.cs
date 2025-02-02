using Microsoft.AspNetCore.Identity;

namespace ProjectEverythingForHomeOnlineShop.Application.DTOs.IdentetyDTOs.RegisterCustomerDTOs
{
    public class RegisterCustomerResultDTO
    {
        public IdentityResult Result { get; set; }
        public string UserId { get; set; }
    }
}
