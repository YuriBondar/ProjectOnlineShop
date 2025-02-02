namespace ProjectEverythingForHomeOnlineShop.Application.DTOs.OrderDTOs
{
    public class ShopOrderDTO
    {

        public int? ShopOrderID { get; set; }
        public int? CustomerID { get; set; }
        public string? UserID { get; set; }

        public string? ShopOrderStatus { get; set; }
        public List<ProductInShopOrderDTO> Products { get; set; } = null!;
    }
}
