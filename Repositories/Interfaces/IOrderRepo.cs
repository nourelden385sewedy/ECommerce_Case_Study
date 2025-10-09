using ECommerce_Case_Study.Data.Models;
using ECommerce_Case_Study.Repositories.GenericRepository;

namespace ECommerce_Case_Study.Repositories.Interfaces
{
    public interface IOrderRepo : IGenericRepo<Order>
    {
        Task<IEnumerable<Order>> GetAllOrdersByCustomerIdAsync(int id);
        void RemoveOrder(Order order);

        Task<Order> GetOrderByIdAsync(int id);
    }
}
