using eMuhasebeServer.Domain.Entities;
using eMuhasebeServer.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eMuhasebeServer.Application.Features.ExpensesDashboards.GetAllExpenses;

internal sealed class GetAllExpensesQueryHandler(
    IExpenseDetailRepository expenseDetailRepository
) : IRequestHandler<GetAllExpensesQuery, Result<List<ExpenseDetail>>> // Dönüş tipini List<ExpenseDetail> olarak değiştirin
{
    public async Task<Result<List<ExpenseDetail>>> Handle(GetAllExpensesQuery request, CancellationToken cancellationToken)
    {
        var expenseDetails = await expenseDetailRepository.GetAllWithTacking().ToListAsync(cancellationToken);

        if (expenseDetails is null || !expenseDetails.Any())
        {
            return Result<List<ExpenseDetail>>.Failure("Gider detayı bulunamadı."); // Hata durumunda boş liste döndürmeyin
        }

        return Result<List<ExpenseDetail>>.Succeed(expenseDetails); // expenseDetails listesini olduğu gibi döndürün
    }
}
