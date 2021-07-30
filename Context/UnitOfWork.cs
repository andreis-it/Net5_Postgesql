using Net5Api.Context;

namespace Net5Api.Infrastructure.Persistence
{
    public interface IUnitOfWork
    {
        ProductDbContext GetDbContext();
        void Commit();
    }

    public class UnitOfWork : IUnitOfWork
    {
        public ProductDbContext DatabaseContext { get; private set; }

        public UnitOfWork(ProductDbContext _dbContext)
        {
            DatabaseContext = _dbContext;
        }

        public void Commit()
        {
            DatabaseContext.SaveChanges();
        }

        ProductDbContext IUnitOfWork.GetDbContext()
        {
            return DatabaseContext;
        }
    }
}
