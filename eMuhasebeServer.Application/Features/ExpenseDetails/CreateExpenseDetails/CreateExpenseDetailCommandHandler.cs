using eMuhasebeServer.Application.Services;
using eMuhasebeServer.Domain.Entities;
using eMuhasebeServer.Domain.Repositories;
using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.ExpenseDetails.CreateExpenseDetails;

internal sealed class CreateExpenseDetailCommandHandler(
    IExpenseRepository expenseRepository,
    IExpenseDetailRepository expenseDetailRepository,
    IUnitOfWorkCompany unitOfWorkCompany,
    ICacheService cacheService,
    ICashRegisterRepository cashRegisterRepository,
    ICashRegisterDetailRepository cashRegisterDetailRepository,
    IBankDetailRepository bankDetailRepository,
    IBankRepository bankRepository

) : IRequestHandler<CreateExpenseDetailCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreateExpenseDetailCommand request, CancellationToken cancellationToken)
    {
        Expense expense =
            await expenseRepository.GetByExpressionWithTrackingAsync(p => p.Id == request.ExpenseId, cancellationToken);

        expense.WithdrawalAmount += request.Amount;


        ExpenseDetail expenseDetail = new()
        {
            Date = request.Date,
            ProcessNumber = request.ProcessNumber,
            WithdrawalAmount = request.Amount,
            Description = request.Description,
            ExpenseId = request.ExpenseId
        };

        await expenseDetailRepository.AddAsync(expenseDetail, cancellationToken);
        cacheService.Remove("expenses");

        if (request.OppositeBankId is not null)
        {
            Bank bank = await bankRepository.GetByExpressionWithTrackingAsync(p => p.Id == request.OppositeBankId,
                cancellationToken);

            bank.WithdrawalAmount += request.OppositeAmount;

            BankDetail bankDetail = new()
            {
                Date = request.Date,
                ProcessNumber = request.ProcessNumber,
                WithdrawalAmount = request.OppositeAmount,
                ExpenseDetailId = expenseDetail.Id,
                Description = request.Description,
                BankId = (Guid)request.OppositeBankId
            };

            expenseDetail.BankDetailId = bankDetail.Id;

            await bankDetailRepository.AddAsync(bankDetail, cancellationToken);
            cacheService.Remove("banks");
        }

        if (request.OppositeCashRegisterId is not null)
        {
            CashRegister cashRegister =
                await cashRegisterRepository.GetByExpressionWithTrackingAsync(
                    p => p.Id == request.OppositeCashRegisterId, cancellationToken);

            cashRegister.WithdrawalAmount += request.OppositeAmount;

            CashRegisterDetail cashRegisterDetail = new()
            {
                Date = request.Date,
                ProcessNumber = request.ProcessNumber,
                WithdrawalAmount = request.OppositeAmount,
                ExpenseDetailId = expenseDetail.Id,
                Description = request.Description,
                CashRegisterId = (Guid)request.OppositeCashRegisterId
            };

            expenseDetail.CashRegisterDetailId = cashRegisterDetail.Id;
            await cashRegisterDetailRepository.AddAsync(cashRegisterDetail, cancellationToken);

        }

        await unitOfWorkCompany.SaveChangesAsync(cancellationToken);
       
        cacheService.Remove("cashRegisters");
        

        return "Giderhareketi başarıyla oluşturuldu";
    }

}