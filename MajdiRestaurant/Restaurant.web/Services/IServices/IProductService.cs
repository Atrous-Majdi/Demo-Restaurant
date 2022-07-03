using Restaurant.web.Models;

namespace Restaurant.web.Services.IServices
{
    public interface IProductService : IBaseService
    {
        Task<T> GetProductsAsync<T>(string token);
        Task<T> GetProductAsync<T>(int  id, string token);
        Task<T> CreateProductAsync<T>(ProductDto productDto, string token);
        Task<T> UpdateProductAsync<T>(ProductDto productDto, string token);
        Task<T> DeleteProductAsync<T>(int id, string token);
    }
}
