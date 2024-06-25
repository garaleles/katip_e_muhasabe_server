using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.DashBoardsCustomers.TotalProducts;

public sealed record TotalProductsQuery() : IRequest<Result<decimal>>;