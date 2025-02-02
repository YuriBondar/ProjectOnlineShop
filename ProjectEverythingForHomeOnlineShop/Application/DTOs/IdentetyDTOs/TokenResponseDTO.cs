namespace ProjectEverythingForHomeOnlineShop.Application.DTOs.IdentetyDTOs
{
    public class TokenResponseDTO
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }

        public string UserID { get; set; }
    }
}
