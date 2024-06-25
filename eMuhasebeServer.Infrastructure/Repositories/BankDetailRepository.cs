using eMuhasebeServer.Domain.Entities;
using eMuhasebeServer.Domain.Repositories;
using eMuhasebeServer.Infrastructure.Context;
using GenericRepository;

namespace eMuhasebeServer.Infrastructure.Repositories;

public sealed class BankDetailRepository: Repository<BankDetail, CompanyDbContext>, IBankDetailRepository
{
    public BankDetailRepository(CompanyDbContext context) : base(context)
    {
    }
}