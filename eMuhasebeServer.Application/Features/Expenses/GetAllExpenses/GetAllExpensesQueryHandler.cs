using eMuhasebeServer.Application.Services;
using eMuhasebeServer.Domain.Entities;
using eMuhasebeServer.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eMuhasebeServer.Application.Features.Expenses.GetAllExpenses;

internal sealed class GetAllExpensesQueryHandler(
    IExpenseRepository expenseRepository,
    ICacheService cacheService
) : IRequestHandler<GetAllExpensesQuery, Result<List<Expense>>>
{
    public async Task<Result<List<Expense>>> Handle(GetAllExpensesQuery request, CancellationToken cancellationToken)
    {
        List<Expense>? expenses;
        expenses = cacheService.Get<List<Expense>>("expenses");
        
        if (expenses is null)
        {
            expenses = await expenseRepository.GetAll().OrderBy(x => x.Name).ToListAsync(cancellationToken);
            cacheService.Set("expenses", expenses);
        }
        
        return expenses;
    }
}