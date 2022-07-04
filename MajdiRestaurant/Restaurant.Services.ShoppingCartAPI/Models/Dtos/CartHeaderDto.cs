using System.ComponentModel.DataAnnotations;

namespace Restaurant.Services.ShoppingCartAPI.Models.Dtos
{
    public class CartHeaderDto
    {
        public int CardHeaderId { get; set; }
        public string UserId { get; set; }
        public string CouponCode { get; set; }
    }
}
