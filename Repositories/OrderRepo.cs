using ECommerce_Case_Study.Data;
using ECommerce_Case_Study.Data.Models;
using ECommerce_Case_Study.Repositories.GenericRepository;
using ECommerce_Case_Study.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerce_Case_Study.Repositories
{
    public class OrderRepo : GenericRepo<Order>, IOrderRepo
    {
        public OrderRepo(MyAppDbContext context) : base(context) { }

        public async Task<IEnumerable<Order>> GetAllOrdersByCustomerIdAsync(int id)
        {
            var orders = await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Products)
                .Where(o => o.CustomerId == id)
                .OrderBy(d => d.DateOfOrder)
                .ToListAsync();

            var orderss = await _context.Orders
                .Where(o => o.CustomerId == id)
                .OrderBy(d => d.DateOfOrder)
                .GroupBy(o => o.Customer)
                .Select(o => new
                {
                    Customer_id = o.Key.Id,
                    Customer = o.Key.Name,
                    Total_Spending = o.Sum(s => s.TotalPrice),
                    Orders = o.Select(v => new
                    {
                        OrderId = v.Id,
                        Total_Price = v.TotalPrice,
                        Number_of_products = v.Products.Count()
                    }).ToList()
                }).ToListAsync();

            return orders;

        }

    }
}
