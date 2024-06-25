using eMuhasebeServer.Application.Services;
using eMuhasebeServer.Domain.Entities;
using eMuhasebeServer.Domain.Repositories;
using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.ExpenseDetails.DeleteExpenseDetailById;

internal sealed class DeleteExpenseDetailByIdCommandHandler(
    IExpenseRepository expenseRepository,
    IExpenseDetailRepository expenseDetailRepository,
    IUnitOfWorkCompany unitOfWorkCompany,
    ICacheService cacheService,
    ICashRegisterRepository cashRegisterRepository,
    ICashRegisterDetailRepository cashRegisterDetailRepository,
    IBankDetailRepository bankDetailRepository,
    IBankRepository bankRepository
) : IRequestHandler<DeleteExpenseDetailByIdCommand, Result<string>>
{
    public async Task<Result<string>> Handle(DeleteExpenseDetailByIdCommand request, CancellationToken cancellationToken)
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
        expenseDetailRepository.Delete(expenseDetail);

        if (expenseDetail.ExpenseDetailId is not null)
        {
            ExpenseDetail? oppositeExpenseDetail =
                await expenseDetailRepository
                    .GetByExpressionWithTrackingAsync(p => p.Id == expenseDetail.ExpenseDetailId, cancellationToken);

            if (oppositeExpenseDetail is null)
            {
                return Result<string>.Failure("Gider hareketi bulunamadı");
            }
            Expense? oppositeExpense =
                await expenseRepository
                    .GetByExpressionWithTrackingAsync(p => p.Id == oppositeExpenseDetail.ExpenseId, cancellationToken);
            if (oppositeExpense is null)
            {
                return Result<string>.Failure("Gider bulunamadı");
            }
            oppositeExpense.WithdrawalAmount -= oppositeExpenseDetail.WithdrawalAmount;
            expenseDetailRepository.Delete(oppositeExpenseDetail);
            
        }
        
        if (expenseDetail.CashRegisterDetailId is not null)
        {
            CashRegisterDetail? oppositeCashRegisterDetail =
                await cashRegisterDetailRepository
                    .GetByExpressionWithTrackingAsync(p => p.Id == expenseDetail.CashRegisterDetailId, cancellationToken);

            if (oppositeCashRegisterDetail is null)
            {
                return Result<string>.Failure("Kasa hareketi bulunamadı");
            }
            CashRegister? oppositeCashRegister =
                await cashRegisterRepository
                    .GetByExpressionWithTrackingAsync(p => p.Id == oppositeCashRegisterDetail.CashRegisterId, cancellationToken);
            if (oppositeCashRegister is null)
            {
                return Result<string>.Failure("Kasa bulunamadı");
            }
            oppositeCashRegister.DepositAmount -= oppositeCashRegisterDetail.DepositAmount;
            oppositeCashRegister.WithdrawalAmount -= oppositeCashRegisterDetail.WithdrawalAmount;
            cashRegisterDetailRepository.Delete(oppositeCashRegisterDetail);
            
            cacheService.Remove("cashRegisters");
        }
        
      
        if (expenseDetail.BankDetailId is not null)
        {
            BankDetail? oppositeBankDetail =
                await bankDetailRepository
                    .GetByExpressionWithTrackingAsync(p => p.Id == expenseDetail.BankDetailId, cancellationToken);

            if (oppositeBankDetail is null)
            {
                return Result<string>.Failure("Banka hareketi bulunamadı");
            }
            Bank? oppositeBank =
                await bankRepository
                    .GetByExpressionWithTrackingAsync(p => p.Id == oppositeBankDetail.BankId, cancellationToken);
            if (oppositeBank is null)
            {
                return Result<string>.Failure("Banka bulunamadı");
            }
            oppositeBank.DepositAmount -= oppositeBankDetail.DepositAmount;
            oppositeBank.WithdrawalAmount -= oppositeBankDetail.WithdrawalAmount;
            bankDetailRepository.Delete(oppositeBankDetail);
            
        }
        await unitOfWorkCompany.SaveChangesAsync(cancellationToken);
        cacheService.Remove("expenses");
        return "Gider hareketi silindi";
    }
}