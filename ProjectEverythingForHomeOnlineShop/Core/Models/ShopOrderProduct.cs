using System.ComponentModel.DataAnnotations;
using ProjectEverythingForHomeOnlineShop.Application.DTOs.OrderDTOs;

namespace ProjectEverythingForHomeOnlineShop.Core.Models
{
    public class ShopOrderProduct
    {
        [Key, MaxLength(20)]
        public int ShopOrderID { get; set; }
        public ShopOrder ShopOrder { get; set; }



        [Required, MaxLength(20)]
        public int ProductID { get; set; }

        public Product Product { get; set; }


        [Required, MaxLength(50)]
        public int ProductOrderQuantity { get; set; }


        public static ProductInShopOrderDTO ToProductInShopOrderDTO(ShopOrderProduct shopOrderProduct, string productName)
        {
            return new ProductInShopOrderDTO
            {
                ProductID = shopOrderProduct.ProductID,
                ProductName = productName,
                ProductQuantity = shopOrderProduct.ProductOrderQuantity
            };
        }

    }
}
