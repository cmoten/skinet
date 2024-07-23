using skinet.Models;

namespace skinet.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsynch(int id);
        Task<IEnumerable<T>> ListAllAsynch();
    }
}
