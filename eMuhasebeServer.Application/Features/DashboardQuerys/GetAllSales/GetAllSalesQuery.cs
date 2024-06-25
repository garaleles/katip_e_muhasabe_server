using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.DashboardQuerys.GetAllSales;

public sealed record GetAllSalesQuery() :  IRequest<Result<decimal>>;