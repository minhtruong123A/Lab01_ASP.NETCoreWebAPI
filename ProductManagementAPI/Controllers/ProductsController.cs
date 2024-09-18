using BusinessObjects;
using BusinessObjects.Dtos.Products;
using DataAccess.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace ProductManagementAPI.Controllers
{
    [ApiController]
    [Route("api/2024_09_11/products")]
    public class ProductsController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;

        public ProductsController(ICategoryService categoryService, IProductService productService)
        {
            _categoryService = categoryService;
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts() => await _productService.GetAllProducts();
        
        [HttpPost]
        public async Task<IActionResult> AddProduct(AddProductDto p)
        {
            var product = new Product
            {
                ProductName = p.ProductName,
                CategoryId = p.CategoryId,
                UnitsInStock = p.UnitsInStock,
                UnitPrice = p.UnitPrice
            };
            await _productService.AddProduct(product);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _productService.DeleteProduct(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(UpdateProductDto id)
        {
            var p = await _productService.GetProductById(id.ProductId);
            if(p == null)
                return NotFound();

            if (!string.IsNullOrEmpty(id.ProductName))
                p.ProductName = id.ProductName;
            if (id.CategoryId.HasValue) 
                p.CategoryId = id.CategoryId.Value;
            if (id.UnitsInStock > 0) 
                p.UnitsInStock = id.UnitsInStock;
            if (id.UnitPrice > 0)
                p.UnitPrice = id.UnitPrice;

            await _productService.UpdateProduct(p);
            return NoContent();
        }
    }
}
