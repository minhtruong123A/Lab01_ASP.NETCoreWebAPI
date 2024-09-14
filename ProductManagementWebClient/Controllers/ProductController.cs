using BusinessObjects;
using BusinessObjects.Dtos.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ProductManagementWebClient.Controllers
{
    public class ProductController : Controller
    {
        private readonly HttpClient _httpClient = null;
        private readonly string productApiUrl = "";

        public ProductController()
        {
            _httpClient = new HttpClient(); 
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            _httpClient.DefaultRequestHeaders.Accept.Add(contentType);
            productApiUrl = "https://localhost:7093/api/9_11_2024/Products";
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(productApiUrl);

                if (!response.IsSuccessStatusCode)
                {
                    ViewBag.ErrorMessage = "Error retrieving products from the API.";
                    return View(new List<Product>());
                }

                string strData = await response.Content.ReadAsStringAsync();

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                List<Product> listProducts = JsonSerializer.Deserialize<List<Product>>(strData, options) ?? new List<Product>();

                return View(listProducts);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred while fetching products.";
                return View(new List<Product>());
            }
        }
        //public async Task<ActionResult> Details(int id);
        //public async Task<ActionResult> Create();
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(AddProductDto p);
        //public async Task<ActionResult> Edit(UpdateProductDto p);
        //public async Task<ActionResult> Delete(int id, IFormCollection collection);
    }
}
