namespace ProjectEverythingForHomeOnlineShop.Application.DTOs.OrderDTOs
{
    public class ProductInShopOrderDTO
    {
        public int ProductID { get; set; }

        public string ProductName { get; set; } = null!;

        public int ProductQuantity { get; set; }
    }
}
