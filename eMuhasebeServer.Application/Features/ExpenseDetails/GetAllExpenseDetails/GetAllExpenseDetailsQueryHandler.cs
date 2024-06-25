using eMuhasebeServer.Domain.Entities;
using eMuhasebeServer.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eMuhasebeServer.Application.Features.ExpenseDetails.GetAllExpenseDetails;

internal sealed class GetAllExpenseDetailsQueryHandler(
    IExpenseRepository expenseRepository
) : IRequestHandler<GetAllExpenseDetailsQuery, Result<Expense>>
{
    public async Task<Result<Expense>> Handle(GetAllExpenseDetailsQuery request, CancellationToken cancellationToken)
    {
        var query = expenseRepository.Where(p => p.Id == request.ExpenseId);

        // Tarih filtrelemesi (daha sağlam)
        if (!request.StartDate.Equals(default(DateOnly)) && !request.EndDate.Equals(default(DateOnly)))
        {
            query = query.Include(p => p.Details!
                .OrderBy(d => d.Date)
                .Where(d => d.Date >= request.StartDate && d.Date <= request.EndDate));
        }
        else
        {
            query = query.Include(p => p.Details!.OrderBy(d => d.Date)); // Artan tarihe göre sıralama
        }

        Expense? expense = await query.FirstOrDefaultAsync(cancellationToken);

        if (expense is null)
        {
            return Result<Expense>.Failure("Gider hareketi bulunamadı");
        }

        return expense;
    }
}