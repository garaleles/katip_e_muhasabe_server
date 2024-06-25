using MediatR;
using TS.Result;
using Unit = eMuhasebeServer.Domain.Entities.Unit;

namespace eMuhasebeServer.Application.Features.Units.GetAllUnits;

public sealed record GetAllUnitsQuery(): IRequest<Result<List<Unit>>>;