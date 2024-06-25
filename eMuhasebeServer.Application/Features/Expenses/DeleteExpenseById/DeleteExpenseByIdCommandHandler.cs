using eMuhasebeServer.Application.Services;
using eMuhasebeServer.Domain.Entities;
using eMuhasebeServer.Domain.Repositories;
using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.Expenses.DeleteExpenseById;

internal sealed class DeleteExpenseByIdCommandHandler(
    IExpenseRepository expenseRepository,
    IUnitOfWorkCompany unitOfWorkCompany,
    ICacheService cacheService
) : IRequestHandler<DeleteExpenseByIdCommand, Result<string>>
{
    public async Task<Result<string>> Handle(DeleteExpenseByIdCommand request, CancellationToken cancellationToken)
    {
        Expense? expense = await expenseRepository.GetByExpressionWithTrackingAsync(x => x.Id == request.Id, cancellationToken);
        if (expense is null)
        {
            return Result<string>.Failure("Gider bulunamadı.");
        }
            
        expense.IsDeleted = true;
        await unitOfWorkCompany.SaveChangesAsync(cancellationToken:cancellationToken);
        cacheService.Remove("expenses");

        return "Gider başarıyla silindi.";
    }
}