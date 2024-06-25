using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.ExpenseDetails.CreateExpenseDetails;

public sealed record CreateExpenseDetailCommand(
    Guid ExpenseId,
    string ProcessNumber,
    DateOnly Date,
    decimal Amount,
    Guid? OppositeBankId,
    Guid? OppositeCashRegisterId,
    decimal OppositeAmount,
    string Description
    ): IRequest<Result<string>>;