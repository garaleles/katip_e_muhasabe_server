using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.DashBoardsCustomers.DashboardCustomersWithDrawalAll;

public record DashboardCustomersWithDrawalAllQuery() : IRequest<Result<decimal>>;