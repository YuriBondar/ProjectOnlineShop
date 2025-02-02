using System.ComponentModel.DataAnnotations;

namespace ProjectEverythingForHomeOnlineShop.Application.DTOs
{
    public class SearchCustomerDTO
    {
      
        public int? CustomerID { get; set; }

        public string? IdentityUserID { get; set; } 

        public string? CustomerEmail { get; set; } 

        public string? CustomerLastName { get; set; } 

        public string? CustomerFirstName { get; set; } 

        public string? CustomerCity { get; set; } 

        public string? CustomerStreet { get; set; } 

        public string? CustomerHausNumber { get; set; } 

        public string? CustomerPostIndex { get; set; } 

        public string? CustomerPhone { get; set; } 
    }
}
