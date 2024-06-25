using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.Units.UpdateUnits;

public sealed record UpdateUnitCommand(
    Guid Id,
    string Name
    ): IRequest<Result<string>>;