using BusinessObjects;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductStoreContext _storeContext;
        public ProductRepository(ProductStoreContext storeContext)
        {
            _storeContext = storeContext;
        }
        public async Task AddProduct(Product p)
        {
            var Category = await _storeContext.Categories.FirstOrDefaultAsync(a => a.CategoryId == p.CategoryId);
            if(Category != null)
            {
                await _storeContext.Products.AddAsync(p);
                await _storeContext.SaveChangesAsync();
            }
        }
        public async Task UpdateProduct(Product p)
        {
            var Category = await _storeContext.Categories.FirstOrDefaultAsync(a => a.CategoryId == p.CategoryId);
            if (Category != null)
            {
                _storeContext.Products.Update(p);
                await _storeContext.SaveChangesAsync();
            }    
        }
        public async Task DeleteProduct(int id)
        {
            var entity = await GetProductById(id);
            if (entity != null)
            {
                _storeContext.Products.Remove(entity);
                await _storeContext.SaveChangesAsync();
            }
        }
        public async Task<Product?> GetProductById(int id) => await _storeContext.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.ProductId == id);
        public async Task<List<Product>> GetAllProducts() => await _storeContext.Products.Include(a => a.Category).ToListAsync();
    }
}
