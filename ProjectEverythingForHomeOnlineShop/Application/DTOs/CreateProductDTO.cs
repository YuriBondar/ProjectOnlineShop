using System.ComponentModel.DataAnnotations;

namespace ProjectEverythingForHomeOnlineShop.Application.DTOs
{
    public class CreateProductDTO
    {
        [Required, MaxLength(50)]
        public string ProductSCU { get; set; } = null!;

        [Required, MaxLength(50)]
        public string ProductName { get; set; } = null!;

        [Required, MaxLength(50)]
        public string Category { get; set; } = null!;

        [Required]
        public decimal UnitPrice { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "StockQuantity must be greater than 0.")]
        public int StockQuantity { get; set; }
    }
}
