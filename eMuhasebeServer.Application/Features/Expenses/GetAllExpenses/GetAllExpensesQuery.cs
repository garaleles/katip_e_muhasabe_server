using eMuhasebeServer.Domain.Entities;
using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.Expenses.GetAllExpenses;

public sealed record GetAllExpensesQuery(): IRequest<Result<List<Expense>>>;