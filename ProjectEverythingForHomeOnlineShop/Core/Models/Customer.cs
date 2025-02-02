using System.ComponentModel.DataAnnotations;

namespace ProjectEverythingForHomeOnlineShop.Core.Models
{
    public class Customer
    {

        [Key]
        public int CustomerID { get; set; }

        [Required, MaxLength(50)]
        public string IdentityUserID { get; set; } = null!;

        [Required, MaxLength(50)]
        public string CustomerEmail { get; set; } = null!;

        [Required, MaxLength(50)]
        public string CustomerLastName { get; set; } = null!;

        [Required, MaxLength(50)]
        public string CustomerFirstName { get; set; } = null!;

        [Required, MaxLength(50)]
        public string CustomerCity { get; set; } = null!;

        [Required, MaxLength(50)]
        public string CustomerStreet { get; set; } = null!;

        [Required, MaxLength(30)]
        public string CustomerHausNumber { get; set; } = null!;

        [Required, MaxLength(30)]
        public string CustomerPostIndex { get; set; } = null!;

        [Required, MaxLength(30)]
        public string CustomerPhone { get; set; } = null!;

        public ICollection<ShopOrder> ShopOrders { get; set; }


        /*

        
        */
    }
}
