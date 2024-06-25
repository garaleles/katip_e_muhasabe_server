using eMuhasebeServer.Domain.Entities;
using GenericRepository;

namespace eMuhasebeServer.Domain.Repositories;

public interface ICheckRepository : IRepository<Check>
{
    Task<Check> GetByIdAsync(Guid checkId, CancellationToken cancellationToken);
    IQueryable<Check> Query();
    void UpdateCheckAndDetail(Check check, CheckDetail checkDetail);
}
