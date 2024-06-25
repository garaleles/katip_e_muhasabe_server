using eMuhasebeServer.Domain.Entities;
using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.ExpenseDetails.GetAllExpenseDetails;

public sealed record GetAllExpenseDetailsQuery(
    Guid ExpenseId,
    DateOnly StartDate,
    DateOnly EndDate
    ): IRequest<Result<Expense>>;