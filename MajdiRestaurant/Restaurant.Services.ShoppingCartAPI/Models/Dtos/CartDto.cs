namespace Restaurant.Services.ShoppingCartAPI.Models.Dtos
{
    public class CartDto
    {
        public CartHeaderDto Header { get; set; }
        public IEnumerable<CartDetailsDto> Details { get; set; }
    }
}
