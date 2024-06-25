using eMuhasebeServer.Application.Services;
using eMuhasebeServer.Domain.Repositories;
using MediatR;
using TS.Result;
using Unit = eMuhasebeServer.Domain.Entities.Unit;

namespace eMuhasebeServer.Application.Features.Units.DeleteUnitById;

public sealed record DeleteUnitByIdCommand(Guid Id): IRequest<Result<string>>;


internal sealed class DeleteUnitByIdCommandHandler(
    IUnitRepository unitRepository,
    IUnitOfWorkCompany unitOfWorkCompany,
    ICacheService cacheService
    ) : IRequestHandler<DeleteUnitByIdCommand, Result<string>>
{
    public async Task<Result<string>> Handle(DeleteUnitByIdCommand request, CancellationToken cancellationToken)
    {
        Unit? unit = await unitRepository.GetByExpressionWithTrackingAsync(x => x.Id == request.Id, cancellationToken);
        if (unit is null)
            return Result<string>.Failure("Birim bulunamadı.");

        unit.IsDeleted = true;
        await unitOfWorkCompany.SaveChangesAsync(cancellationToken);
        cacheService.Remove("units");
        return "Birim başarıyla silindi.";
    }
}