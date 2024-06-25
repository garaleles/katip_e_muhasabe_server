using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.ExpenseDetails.UpdateExpenses;

public sealed record UpdateExpenseDetailCommand(
    Guid Id,
    Guid ExpenseId,
    DateOnly Date,
    decimal Amount,
    string Description
    ): IRequest<Result<string>>;