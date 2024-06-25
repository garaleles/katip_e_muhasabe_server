using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.DashboardQuerys.GetAllPurchases;

public sealed record GetAllPurchasesQuery():  IRequest<Result<decimal>>;