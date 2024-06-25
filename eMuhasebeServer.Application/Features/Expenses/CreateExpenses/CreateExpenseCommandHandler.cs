using AutoMapper;
using eMuhasebeServer.Application.Services;
using eMuhasebeServer.Domain.Entities;
using eMuhasebeServer.Domain.Repositories;
using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.Expenses.CreateExpenses;

internal sealed class CreateExpenseCommandHandler(
    IExpenseRepository expenseRepository,
    IUnitOfWorkCompany unitOfWorkCompany,
    IMapper mapper,
    ICacheService cacheService
) : IRequestHandler<CreateExpenseCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreateExpenseCommand request, CancellationToken cancellationToken)
    {
        bool isNameExists = await expenseRepository.AnyAsync(x => x.Name == request.Name, cancellationToken: cancellationToken);
            
        if (isNameExists)
        {
            return Result<string>.Failure("Bu gider ismi zaten mevcut");
        }
            
        Expense expense = mapper.Map<Expense>(request);
        await expenseRepository.AddAsync(expense, cancellationToken);
        await unitOfWorkCompany.SaveChangesAsync(cancellationToken);
        cacheService.Remove("expenses");
        return "Gider hesabı başarıyla oluşturuldu";
    }
}