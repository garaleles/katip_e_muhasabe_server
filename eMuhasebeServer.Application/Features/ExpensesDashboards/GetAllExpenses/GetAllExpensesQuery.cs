using eMuhasebeServer.Domain.Entities;
using eMuhasebeServer.Domain.Enums;
using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.ExpensesDashboards.GetAllExpenses;

public sealed record GetAllExpensesQuery():  IRequest<Result<GetAllExpensesQuery>>, IRequest<Result<List<ExpenseDetail>>>;