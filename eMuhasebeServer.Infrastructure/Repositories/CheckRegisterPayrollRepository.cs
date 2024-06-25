using eMuhasebeServer.Domain.Entities;
using eMuhasebeServer.Domain.Repositories;
using eMuhasebeServer.Infrastructure.Context;
using GenericRepository;

namespace eMuhasebeServer.Infrastructure.Repositories;

public sealed class CheckRegisterPayrollRepository: Repository<CheckRegisterPayroll, CompanyDbContext>, ICheckRegisterPayrollRepository
{
    public CheckRegisterPayrollRepository(CompanyDbContext context) : base(context)
    {
    }
}