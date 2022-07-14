namespace Restaurant.web.Models
{
    public class CartHeaderDto
    {
        public int CardHeaderId { get; set; }
        public string UserId { get; set; }
        public string CouponCode { get; set; }
        public double OrderTotal { get; set; }
    }
}
