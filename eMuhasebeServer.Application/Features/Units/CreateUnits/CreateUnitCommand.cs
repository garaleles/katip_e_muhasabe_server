using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.Units.CreateUnits;

public sealed record CreateUnitCommand(
    string Name
    ) : IRequest<Result<string>>;