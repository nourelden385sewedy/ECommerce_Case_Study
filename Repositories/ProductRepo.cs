using ECommerce_Case_Study.Data;
using ECommerce_Case_Study.Data.Models;
using ECommerce_Case_Study.Repositories.GenericRepository;
using ECommerce_Case_Study.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerce_Case_Study.Repositories
{
    public class ProductRepo : GenericRepo<Product>, IProductRepo
    {
        public ProductRepo(MyAppDbContext context) : base(context) { }

        public async Task<IEnumerable<Product>> GetAllProductsAsync(decimal min, decimal max)
        {
            return await _context.Products
                .Include(p => p.Category)
                .Where(p => p.Price >= min && p.Price <= max)
                .OrderByDescending(p => p.Price).ToListAsync();

        }
    }
}
