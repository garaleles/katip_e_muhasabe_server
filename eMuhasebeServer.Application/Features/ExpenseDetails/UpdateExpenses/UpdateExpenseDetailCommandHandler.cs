using eMuhasebeServer.Application.Services;
using eMuhasebeServer.Domain.Entities;
using eMuhasebeServer.Domain.Repositories;
using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.ExpenseDetails.UpdateExpenses;

public sealed class UpdateExpenseDetailCommandHandler(
    IExpenseDetailRepository expenseDetailRepository,
    IExpenseRepository expenseRepository,
    ICashRegisterDetailRepository cashRegisterDetailRepository,
    ICashRegisterRepository cashRegisterRepository,
    IBankDetailRepository bankDetailRepository,
    IBankRepository bankRepository,
    IUnitOfWorkCompany unitOfWorkCompany,
    ICacheService cacheService
) : IRequestHandler<UpdateExpenseDetailCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateExpenseDetailCommand request, CancellationToken cancellationToken)
    {
        ExpenseDetail? expenseDetail =
            await expenseDetailRepository
                .GetByExpressionWithTrackingAsync(p => p.Id == request.Id, cancellationToken);

        if (expenseDetail is null)
        {
            return Result<string>.Failure("Gider hareketi bulunamadı");
        }

        Expense? expense =
            await expenseRepository
                .GetByExpressionWithTrackingAsync(p => p.Id == expenseDetail.ExpenseId, cancellationToken);

        if (expense is null)
        {
            return Result<string>.Failure("Gider bulunamadı");
        }

        expense.WithdrawalAmount -= expenseDetail.WithdrawalAmount;
        expense.WithdrawalAmount += request.Amount;
        expenseDetail.WithdrawalAmount = request.Amount;

       
        if (expenseDetail.CashRegisterDetailId is not null)
        {
            CashRegisterDetail? cashRegisterDetail =
                await cashRegisterDetailRepository
                    .GetByExpressionWithTrackingAsync(p => p.Id == expenseDetail.CashRegisterDetailId, cancellationToken);

            if (cashRegisterDetail is not null)
            {
                CashRegister? cashRegister =
                    await cashRegisterRepository
                        .GetByExpressionWithTrackingAsync(p => p.Id == cashRegisterDetail.CashRegisterId, cancellationToken);

                if (cashRegister is not null)
                {
                    cashRegister.WithdrawalAmount -= cashRegisterDetail.WithdrawalAmount;
                    cashRegister.WithdrawalAmount += request.Amount;
                    cashRegisterDetail.WithdrawalAmount = request.Amount;
                    cashRegisterDetail.Description = request.Description; // Update the description
                }
            }
        }

        if (expenseDetail.BankDetailId is not null)
        {
            BankDetail? bankDetail =
                await bankDetailRepository
                    .GetByExpressionWithTrackingAsync(p => p.Id == expenseDetail.BankDetailId, cancellationToken);

            if (bankDetail is not null)
            {
                Bank? bank =
                    await bankRepository
                        .GetByExpressionWithTrackingAsync(p => p.Id == bankDetail.BankId, cancellationToken);

                if (bank is not null)
                {
                    bank.WithdrawalAmount -= bankDetail.WithdrawalAmount;
                    bank.WithdrawalAmount += request.Amount;
                    bankDetail.WithdrawalAmount = request.Amount;
                    bankDetail.Description = request.Description; // Update the description
                }
            }
        }

        expenseDetail.Description = request.Description;
        expenseDetail.Date = request.Date;

        await unitOfWorkCompany.SaveChangesAsync(cancellationToken);

        cacheService.Remove("expenses");

        return "Gider hareketi başarıyla güncellendi";
    }
}