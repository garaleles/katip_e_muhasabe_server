using eMuhasebeServer.Domain.Entities;
using eMuhasebeServer.Domain.Repositories;
using eMuhasebeServer.Infrastructure.Context;
using GenericRepository;

namespace eMuhasebeServer.Infrastructure.Repositories;

public sealed class CompanyCheckAccountRepository: Repository<CompanyCheckAccount, CompanyDbContext>, ICompanyCheckAccountRepository
{
    public CompanyCheckAccountRepository(CompanyDbContext context) : base(context)
    {
    }
}