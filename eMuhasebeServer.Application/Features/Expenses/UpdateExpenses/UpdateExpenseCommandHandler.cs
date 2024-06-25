using AutoMapper;
using eMuhasebeServer.Application.Services;
using eMuhasebeServer.Domain.Entities;
using eMuhasebeServer.Domain.Repositories;
using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.Expenses.UpdateExpenses;

internal sealed class UpdateExpenseCommandHandler(
    IExpenseRepository expenseRepository,
    IUnitOfWorkCompany unitOfWorkCompany,
    ICacheService cacheService,
    IMapper mapper
) : IRequestHandler<UpdateExpenseCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateExpenseCommand request, CancellationToken cancellationToken)
    {
        Expense? expense = await expenseRepository.GetByExpressionWithTrackingAsync(x => x.Id == request.Id, cancellationToken);
        if (expense is null)
        {
            return Result<string>.Failure("Gider bulunamadı.");
        }

        if (expense.Name != request.Name)
        {
            if (await expenseRepository.AnyAsync(x => x.Name == request.Name, cancellationToken))
            {
                return Result<string>.Failure("Bu isimde bir gider zaten mevcut.");
            }
        }

        mapper.Map(request, expense);
        await unitOfWorkCompany.SaveChangesAsync(cancellationToken);
        cacheService.Remove("expenses");
        return "Gider bilgileri başarıyla güncellendi.";
    }
}