using eMuhasebeServer.Domain.Entities;
using GenericRepository;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace eMuhasebeServer.Domain.Repositories;

public interface ICheckDetailRepository : IRepository<CheckDetail>
{
    Task<CheckDetail> GetByIdAsync(Guid checkDetailId, CancellationToken cancellationToken);
    CheckDetail FirstOrDefault(Expression<Func<CheckDetail, bool>> predicate);
    Task<CheckDetail> FirstOrDefaultAsync(Expression<Func<CheckDetail, bool>> predicate, CancellationToken cancellationToken = default);

    EntityEntry<CheckDetail> Entry(CheckDetail entity);

}
