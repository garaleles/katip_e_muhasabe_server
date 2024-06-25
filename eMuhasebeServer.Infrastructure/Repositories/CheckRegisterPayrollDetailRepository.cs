using eMuhasebeServer.Domain.Entities;
using eMuhasebeServer.Domain.Repositories;
using eMuhasebeServer.Infrastructure.Context;
using GenericRepository;

namespace eMuhasebeServer.Infrastructure.Repositories;

public sealed class CheckRegisterPayrollDetailRepository : Repository<CheckRegisterPayrollDetail, CompanyDbContext>, ICheckRegisterPayrollDetailRepository
{

    public CheckRegisterPayrollDetailRepository(CompanyDbContext context) : base(context)
    {
    }
}
