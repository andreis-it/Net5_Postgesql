using Net5Api.Context;
using Net5Api.Infrastructure.Persistence;
using Net5Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Net5Api.ProductMaster
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAll();
        Task<Product> GetSingle(string id);
        Product Create(Product product);
        void Update(Product product);
        void Delete(Product product);
        void SaveChanges();

    }

    public class ProductService : IProductService
    {
        private IProductRepository _repository;
        private IUnitOfWork _unitOfWork;

        public ProductService(IProductRepository repository, IUnitOfWork unitOfWork)
        {
            this._repository = repository;
            this._unitOfWork = unitOfWork;
        }

        public Product Create(Product product)
        {
           return _repository.Create(product);
        }

        public void Update(Product product)
        {
            _repository.Update(product);
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<Product> GetSingle(string id)
        {
            return await _repository.GetSingle(id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Delete(Product product)
        {
            _repository.Delete(product);
        }
    }
}
