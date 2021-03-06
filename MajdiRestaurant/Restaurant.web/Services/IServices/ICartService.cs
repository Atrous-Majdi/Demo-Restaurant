using Restaurant.web.Models;

namespace Restaurant.web.Services.IServices
{
    public interface ICartService
    {
        Task<T> GetCartByUserIdAsync<T>(string userId, string token);
        Task<T> AddToCartAsync<T>(CartDto cartDto, string token);
        Task<T> UpdateCartAsync<T>(CartDto cartDto, string token);
        Task<T> RemoveFromCartAsync<T>(int cartId, string token);
    }
}
