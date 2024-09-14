using BusinessObjects;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ProductStoreContext _storeContext;

        public CategoryRepository(ProductStoreContext storeContext)
        {
            _storeContext = storeContext;
        }
        public async Task<List<Category>> GetAllCategories() => await _storeContext.Categories.ToListAsync();
    }
}
