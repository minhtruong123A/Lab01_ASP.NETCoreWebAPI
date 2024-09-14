using BusinessObjects;
using DataAccess.Interfaces;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public Task AddProduct(Product p) => _productRepository.AddProduct(p);
        public Task UpdateProduct(Product p) => _productRepository.UpdateProduct(p);
        public Task DeleteProduct(int id) => _productRepository.DeleteProduct(id);
        public Task<Product?> GetProductById(int id) => _productRepository.GetProductById(id);
        public Task<List<Product>> GetAllProducts() => _productRepository.GetAllProducts();
    }
}
