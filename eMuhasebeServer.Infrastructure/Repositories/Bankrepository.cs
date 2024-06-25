using eMuhasebeServer.Domain.Entities;
using eMuhasebeServer.Domain.Repositories;
using eMuhasebeServer.Infrastructure.Context;
using GenericRepository;

namespace eMuhasebeServer.Infrastructure.Repositories;

public sealed class Bankrepository: Repository<Bank, CompanyDbContext>, IBankRepository
{
    public Bankrepository(CompanyDbContext context) : base(context)
    {
    }
}