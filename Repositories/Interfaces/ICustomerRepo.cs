using ECommerce_Case_Study.Data.Models;
using ECommerce_Case_Study.Repositories.GenericRepository;

namespace ECommerce_Case_Study.Repositories.Interfaces
{
    public interface ICustomerRepo : IGenericRepo<Customer> 
    {
        Task<Customer?> GetCustomerWithProfileByIdAsync(int id);
        Task<Customer?> GetByEmailAsync(string email);
    }
}
