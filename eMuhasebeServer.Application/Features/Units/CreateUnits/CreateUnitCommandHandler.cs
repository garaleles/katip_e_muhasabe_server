using AutoMapper;
using eMuhasebeServer.Application.Services;
using eMuhasebeServer.Domain.Repositories;
using MediatR;
using TS.Result;
using Unit = eMuhasebeServer.Domain.Entities.Unit;

namespace eMuhasebeServer.Application.Features.Units.CreateUnits;

internal sealed class CreateUnitCommandHandler(
    IUnitRepository unitRepository,
    IUnitOfWorkCompany unitOfWorkCompany,
    IMapper mapper,
    ICacheService cacheService
) : IRequestHandler<CreateUnitCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreateUnitCommand request, CancellationToken cancellationToken)
    {
        bool isNameExists = await unitRepository.AnyAsync(x => x.Name == request.Name, cancellationToken: cancellationToken);
            
        if (isNameExists)
        {
            return Result<string>.Failure("Bu birim adı zaten mevcut");
        }
            
        Unit unit = mapper.Map<Unit>(request);
        await unitRepository.AddAsync(unit, cancellationToken);
        await unitOfWorkCompany.SaveChangesAsync(cancellationToken);
        cacheService.Remove("units");
        return "Birim başarıyla oluşturuldu";
    }
}