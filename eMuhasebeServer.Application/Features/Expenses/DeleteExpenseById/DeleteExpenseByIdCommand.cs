using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.Expenses.DeleteExpenseById;

public sealed record DeleteExpenseByIdCommand(
    Guid Id
    ): IRequest<Result<string>>;