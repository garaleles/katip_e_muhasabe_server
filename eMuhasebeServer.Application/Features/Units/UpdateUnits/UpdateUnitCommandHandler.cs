using AutoMapper;
using eMuhasebeServer.Application.Services;
using eMuhasebeServer.Domain.Repositories;
using MediatR;
using TS.Result;
using Unit = eMuhasebeServer.Domain.Entities.Unit;

namespace eMuhasebeServer.Application.Features.Units.UpdateUnits;

internal sealed class UpdateUnitCommandHandler(
    IUnitRepository unitRepository,
    IMapper mapper,
    IUnitOfWorkCompany unitOfWorkCompany,
    ICacheService cacheService
) : IRequestHandler<UpdateUnitCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateUnitCommand request, CancellationToken cancellationToken)
    {
        Unit? unit = await unitRepository.GetByExpressionWithTrackingAsync(x => x.Id == request.Id, cancellationToken);
        if (unit is null)
        {
            return Result<string>.Failure("Birim bulunamadı.");
        }

        if (unit.Name != request.Name)
        {
            bool isNameExists = await unitRepository.AnyAsync(x => x.Name == request.Name, cancellationToken);
            if (isNameExists)
            {
                return Result<string>.Failure("Birim daha önce kaydedilmiş.");
            }
        }
           
        mapper.Map(request, unit);
        await unitOfWorkCompany.SaveChangesAsync(cancellationToken);
        cacheService.Remove("units");

        return "Birim başarıyla güncellendi.";

    }
}