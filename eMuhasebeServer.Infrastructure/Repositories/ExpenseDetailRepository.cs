using eMuhasebeServer.Domain.Entities;
using eMuhasebeServer.Domain.Repositories;
using eMuhasebeServer.Infrastructure.Context;
using GenericRepository;

namespace eMuhasebeServer.Infrastructure.Repositories;

public sealed class ExpenseDetailRepository:Repository<ExpenseDetail, CompanyDbContext>,IExpenseDetailRepository
{
    public ExpenseDetailRepository(CompanyDbContext context) : base(context)
    {
    }
}