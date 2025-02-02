using System.ComponentModel.DataAnnotations;

namespace ProjectEverythingForHomeOnlineShop.Application.DTOs.IdentetyDTOs
{
    public class LoginDTO
    {
        public string UserName { get; set; } = null!;

        public string UserPassword { get; set; }

        public string UserEmail { get; set; } = null!;
    }
}
