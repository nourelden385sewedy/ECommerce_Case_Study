using ECommerce_Case_Study.Data;
using ECommerce_Case_Study.Data.Models;
using ECommerce_Case_Study.Repositories.GenericRepository;
using ECommerce_Case_Study.Repositories.Interfaces;

namespace ECommerce_Case_Study.Repositories
{
    public class CategoryRepo : GenericRepo<Category>, ICategoryRepo
    {
        public CategoryRepo(MyAppDbContext context) : base(context) { }

       
    }
}
