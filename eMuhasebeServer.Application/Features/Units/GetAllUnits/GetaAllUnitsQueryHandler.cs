using eMuhasebeServer.Application.Services;
using eMuhasebeServer.Domain.Entities;
using eMuhasebeServer.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eMuhasebeServer.Application.Features.Units.GetAllUnits;

internal sealed class GetaAllUnitsQueryHandler(
    IUnitRepository UnitRepository,
    ICacheService cacheService
) : MediatR.IRequestHandler<GetAllUnitsQuery, Result<List<Unit>>>
{
    public async Task<Result<List<Unit>>> Handle(GetAllUnitsQuery request, CancellationToken cancellationToken)
    {
        List<Unit>? units;
        units= cacheService.Get<List<Unit>>("units");

        if (units is null)
        {
            units= await UnitRepository.GetAll().OrderBy(x=>x.Name).ToListAsync(cancellationToken);
            cacheService.Set("units", units);
        }
       
        return units;
           
       
    }
}