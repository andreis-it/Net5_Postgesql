using Net5Api.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Net5Api.Models;

namespace Net5Api.ProductMaster
{
    public interface IProductRepository
    {
        Product Create(Product product);
        void Update(Product product);
        Task<List<Product>> GetAll();
        Task<Product> GetSingle(string id);
        void Delete(Product product);
    }

    public class ProductRepository : IProductRepository
    {
        private readonly ProductDbContext  _context; 
        private readonly DbSet<Product> dbSet;

        public ProductRepository(ProductDbContext context)
        {
            this._context = context;
            dbSet = _context.Set<Product>();
        }

        public Product Create(Product  product)
        {
            return  dbSet.Add(product).Entity;
        }

        public void Update(Product product)
        {
             _context.Entry(product).CurrentValues.SetValues(product);
        }

        public Task<List<Product>> GetAll()
        {
            return dbSet.ToListAsync();
        }

        public Task<Product> GetSingle(string id)
        {
            return dbSet.FirstOrDefaultAsync(x => x.Id == id);
        }

        public void Delete(Product product)
        {
            dbSet.Remove(product);
        }
    }
}
