namespace ProjectEverythingForHomeOnlineShop.Core.Models
{
    public class ShopOrderStatus
    {
        public int ShopOrderStatusID { get; set; }

        public string ShopOrderStatusName { get; set; }

        public ICollection<ShopOrder> ShopOrders { get; set; }
    }
}
