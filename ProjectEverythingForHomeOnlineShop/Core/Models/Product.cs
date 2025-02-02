using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectEverythingForHomeOnlineShop.Core.Models
{
    public class Product
    {

        [Key, MaxLength(20)]
        public int ProductID { get; set; }

        [Required, MaxLength(50)]
        public string ProductSCU { get; set; } = null!;

        [Required, MaxLength(50)]
        public string ProductName { get; set; } = null!;

        [Required, MaxLength(50)]
        [Column("Category")]
        public string Category { get; set; } = null!;

        [Required, MaxLength(30)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }

        [Required, MaxLength(30)]
        public int StockQuantity { get; set; }

        public ICollection<ShopOrderProduct> ShopOrderProducts { get; set; }
       
        
    }
}
