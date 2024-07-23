using skinet.Models;

namespace skinet.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsynch();

        Task<Product> GetByIdAsynch(int id);

        Task AddProductAsynch(Product product);
        Task UpdateProductAsynch(Product product);
        Task DeleteProductAsynch(int id);
    }
}
