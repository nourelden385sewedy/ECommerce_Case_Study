
using ECommerce_Case_Study.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ECommerce_Case_Study.Repositories.GenericRepository
{
    public class GenericRepo<T> : IGenericRepo<T> where T : class
    {
        protected readonly MyAppDbContext _context;

        public GenericRepo(MyAppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        public void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        
    }
}
