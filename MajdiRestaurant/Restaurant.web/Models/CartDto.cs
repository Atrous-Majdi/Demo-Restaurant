namespace Restaurant.web.Models
{
    public class CartDto
    {
        public CartHeaderDto Header { get; set; }
        public IEnumerable<CartDetailsDto> Details { get; set; }
    }
}
