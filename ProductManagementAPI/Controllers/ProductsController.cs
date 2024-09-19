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
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts(int pageNumber = 1, int pageSize = 10)
        {
            if (pageNumber <= 0) pageNumber = 1;
            if (pageSize <= 0) pageSize = 10;

            var products = await _productService.GetAllProducts();
            var totalProducts = products.Count();
            int totalPages = (int)Math.Ceiling(totalProducts / (double)pageSize);
            var paginatedProducts = products
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(p => new
                {
                    p.ProductId,
                    p.ProductName,
                    p.UnitsInStock,
                    p.UnitPrice,
                    Category = new
                    {
                        p.Category.CategoryId,
                        p.Category.CategoryName
                    }
                })
                .ToList();
            var paginationMetadata = new
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages,
                TotalProducts = totalProducts
            };

            return Ok(new
            {
                Products = paginatedProducts,
                Pagination = paginationMetadata
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductById(int id)
        {
            var gogo = await _productService.GetProductById(id);

            if (gogo == null)
            {
                return NotFound(new { Message = $"Product with ID {id} not found." });
            }

            return Ok(new
            {
                ProductId = gogo.ProductId,
                ProductName = gogo.ProductName,
                UnitsInStock = gogo.UnitsInStock,
                UnitPrice = gogo.UnitPrice,
                Category = gogo.Category,
            });
        }

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

        [HttpPut]
        public async Task<IActionResult> UpdateProduct(UpdateProductDto dto)
        {
            var p = await _productService.GetProductById(dto.ProductId);
            if(p == null)
                return NotFound();

            if (!string.IsNullOrEmpty(dto.ProductName))
                p.ProductName = dto.ProductName;
            if (dto.CategoryId.HasValue && dto.CategoryId > 0) 
                p.CategoryId = dto.CategoryId.Value;
            if (dto.UnitsInStock > 0) 
                p.UnitsInStock = dto.UnitsInStock;
            if (dto.UnitPrice > 0)
                p.UnitPrice = dto.UnitPrice;

            await _productService.UpdateProduct(p);
            return NoContent();
        }
    }
}
