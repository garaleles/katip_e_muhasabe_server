using eMuhasebeServer.Domain.Entities;
using eMuhasebeServer.Domain.Repositories;
using eMuhasebeServer.Infrastructure.Context;
using GenericRepository;

namespace eMuhasebeServer.Infrastructure.Repositories;

public sealed class ExpenseRepository:Repository<Expense, CompanyDbContext>,IExpenseRepository
{
    public ExpenseRepository(CompanyDbContext context) : base(context)
    {
    }
}