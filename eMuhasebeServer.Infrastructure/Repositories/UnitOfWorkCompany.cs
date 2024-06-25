using eMuhasebeServer.Domain.Repositories;
using eMuhasebeServer.Infrastructure.Context;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;

namespace eMuhasebeServer.Infrastructure.Repositories
{
    public class UnitOfWorkCompany : IUnitOfWorkCompany
    {
        private readonly CompanyDbContext _dbContext;

        public UnitOfWorkCompany(CompanyDbContext dbContext)
        {
            _dbContext = dbContext;
        }



        public Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            return _dbContext.Database.BeginTransactionAsync();
        }

        public EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class
        {
            return _dbContext.Entry(entity);
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
