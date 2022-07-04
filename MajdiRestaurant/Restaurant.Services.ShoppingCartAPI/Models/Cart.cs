namespace Restaurant.Services.ShoppingCartAPI.Models
{
    public class Cart
    {
        public CartHeader Header { get; set; }
        public IEnumerable<CartDetails> Details { get; set; }
    }
}
