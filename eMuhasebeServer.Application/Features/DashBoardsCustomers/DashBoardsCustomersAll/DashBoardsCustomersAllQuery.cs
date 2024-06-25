using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.DashBoardsCustomers.DashBoardsCustomersAll;

public record DashBoardsCustomersAllQuery() : IRequest<Result<decimal>>;