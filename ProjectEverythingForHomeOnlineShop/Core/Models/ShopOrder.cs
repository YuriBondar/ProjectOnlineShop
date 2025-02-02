using System.ComponentModel.DataAnnotations;

namespace ProjectEverythingForHomeOnlineShop.Core.Models
{
    public class ShopOrder
    {
        [Key, MaxLength(20)]
        public int ShopOrderID { get; set; }


        [Required, MaxLength(20)]
        public int CustomerID { get; set; }
        public Customer Customer { get; set; }


        [Required, MaxLength(20)]
        public int ShopOrderStatusID { get; set; }
        public ShopOrderStatus ShopOrderStatus { get; set; }



        [Required, MaxLength(50)]
        public DateTime ShopOrderAcceptedAt { get; set; }



        [MaxLength(30)]
        public DateTime ShopOrderShippedAt { get; set; }



        [MaxLength(30)]
        public DateTime ShopOrderCompletedAt { get; set; }



        public ICollection<ShopOrderProduct> ShopOrderProducts { get; set; }

        

    }
}
