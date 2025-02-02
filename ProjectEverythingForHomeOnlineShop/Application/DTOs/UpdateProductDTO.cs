using System.ComponentModel.DataAnnotations;

namespace ProjectEverythingForHomeOnlineShop.Application.DTOs
{
    public class UpdateProductDTO
    {
        [MaxLength(50)]
        public string? ProductSCU { get; set; } = null!;

        [MaxLength(50)]
        public string? ProductName { get; set; } = null!;

        [MaxLength(50)]
        public string? Category { get; set; } = null!;

        [Range(0, double.MaxValue, ErrorMessage = "UnitPrice must be a positive number.")]
        public decimal? UnitPrice { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "StockQuantity must be greater than 0.")]
        public int? StockQuantity { get; set; }
    }
}
