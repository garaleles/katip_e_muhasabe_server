using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.ExpenseDetails.DeleteExpenseDetailById;

public sealed record DeleteExpenseDetailByIdCommand(Guid Id) : IRequest<Result<string>>;