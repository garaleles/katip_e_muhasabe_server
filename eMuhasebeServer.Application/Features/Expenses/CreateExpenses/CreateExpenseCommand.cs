using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.Expenses.CreateExpenses;

public sealed record CreateExpenseCommand(
    string Name,
    string Description,
    decimal WithdrawalAmount,
    int CurrencyTypeValue
    ): IRequest<Result<string>>;