using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IProductRepository
    {
        Task AddProduct(Product p);
        Task UpdateProduct(Product p);
        Task DeleteProduct(int id);
        Task<Product?> GetProductById(int id);
        Task<List<Product>> GetAllProducts();
    }
}
