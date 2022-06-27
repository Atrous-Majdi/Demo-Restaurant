using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Restaurant.web.Models;
using Restaurant.web.Services.IServices;

namespace Restaurant.web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            this._productService = productService;
        }
        public async Task<IActionResult> ProductIndex()
        {
            List<ProductDto> list = new();
          
            var response = await _productService.GetProductsAsync<ResponseDto>();

            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(response.Result));
            }
            return View(list);
        }
    }
}
