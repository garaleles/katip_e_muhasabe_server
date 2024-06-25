using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.Expenses.UpdateExpenses;

public sealed record UpdateExpenseCommand(
    Guid Id,
    string Name,
    string Description,
    int CurrencyTypeValue
    ): IRequest<Result<string>>;