using skinet.Data;
using skinet.Models;
using Microsoft.EntityFrameworkCore;
using skinet.Interfaces;

namespace skinet.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreContext _storeContext;
        public ProductRepository(StoreContext storeContext) 
        { 
            _storeContext = storeContext;
        }

        public async Task AddProductAsynch(Product product)
        {
            await _storeContext.Products.AddAsync(product);
            await _storeContext.SaveChangesAsync();
        }

        public async Task DeleteProductAsynch(int id)
        {
            var productInDb = await _storeContext.Products.FindAsync(id);
            if (productInDb != null) 
            {
                throw new KeyNotFoundException($"Product with {id} was not found.");
            }
            _storeContext.Products.Remove(productInDb);
            await _storeContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Product>> GetAllAsynch()
        {
            return await _storeContext.Products
                .Include(p => p.ProductType)
                .Include(p => p.ProductBrand)
                .ToListAsync();
        }

        public async Task<Product?> GetByIdAsynch(int id)
        {
            return await _storeContext.Products
                .Include(p => p.ProductType)
                .Include(p => p.ProductBrand)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task UpdateProductAsynch(Product product)
        {
            _storeContext.Products.Update(product);
            await _storeContext.SaveChangesAsync();
        }
    }
}
