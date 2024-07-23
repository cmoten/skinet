using Microsoft.EntityFrameworkCore;
using skinet.Data;
using skinet.Interfaces;
using skinet.Models;

namespace skinet.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext _context;

        public GenericRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<T> GetByIdAsynch(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> ListAllAsynch()
        {
            return await _context.Set<T>().ToListAsync();
        }
    }
}
