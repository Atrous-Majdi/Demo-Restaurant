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

        public async Task<IActionResult> ProductCreate()
        {          
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductCreate(ProductDto product)
        {
            if(ModelState.IsValid)
            {
                var response = await _productService.CreateProductAsync<ResponseDto>(product);

                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(ProductIndex));
                }               
            }
            return View(product);
        }


        public async Task<IActionResult> ProductEdit(int productId)
        {
            var response = await _productService.GetProductAsync<ResponseDto>(productId);

            if (response != null && response.IsSuccess)
            {
                ProductDto model = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
                return View(model);
            }

            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductEdit(ProductDto product)
        {
            if (ModelState.IsValid)
            {
                var response = await _productService.UpdateProductAsync<ResponseDto>(product);

                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(ProductIndex));
                }
            }
            return View(product);
        }

        public async Task<IActionResult> ProductDelete(int productId)
        {
            var response = await _productService.GetProductAsync<ResponseDto>(productId);

            if (response != null && response.IsSuccess)
            {
                ProductDto model = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
                return View(model);
            }

            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductDelete(ProductDto product)
        {
            if (ModelState.IsValid)
            {
                var response = await _productService.DeleteProductAsync<ResponseDto>(product.ProductId);

                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(ProductIndex));
                }
            }
            return View(product);
        }



    }
}
