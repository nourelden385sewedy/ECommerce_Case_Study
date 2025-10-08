using ECommerce_Case_Study.Data;
using ECommerce_Case_Study.Data.Models;
using ECommerce_Case_Study.Repositories.GenericRepository;
using ECommerce_Case_Study.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerce_Case_Study.Repositories
{
    public class CustomerRepo : GenericRepo<Customer>, ICustomerRepo
    {
        public CustomerRepo(MyAppDbContext context) : base(context) { }

        public async Task<Customer?> GetCustomerWithProfileByIdAsync(int id)
        {
            return await _context.Customers.Include(c => c.CustomerProfile).FirstOrDefaultAsync(x=>x.Id==id);
        }

        public async Task<Customer?> GetByEmailAsync(string email)
        {
            return await _context.Customers.FirstOrDefaultAsync(l => l.Email == email);
        }

    }
}
