using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.DashBoardsCustomers.DashboardSafes;

public record DashboardSafesQuery() : IRequest<Result<decimal>>;