using ECommerce_Case_Study.Data;
using ECommerce_Case_Study.Data.Models;
using ECommerce_Case_Study.Repositories.GenericRepository;
using ECommerce_Case_Study.Repositories.Interfaces;

namespace ECommerce_Case_Study.Repositories
{
    public class CustomerRepo : GenericRepo<Customer>, ICustomerRepo
    {
        public CustomerRepo(MyAppDbContext context) : base(context) { }

        public Task Custom()
        {
            throw new NotImplementedException();
        }
    }
}
