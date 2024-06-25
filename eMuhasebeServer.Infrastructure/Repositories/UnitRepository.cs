using eMuhasebeServer.Domain.Entities;
using eMuhasebeServer.Domain.Repositories;
using eMuhasebeServer.Infrastructure.Context;
using GenericRepository;

namespace eMuhasebeServer.Infrastructure.Repositories;

public sealed class UnitRepository: Repository<Unit, CompanyDbContext>, IUnitRepository
{
    public UnitRepository(CompanyDbContext context) : base(context)
    {
    }
}
