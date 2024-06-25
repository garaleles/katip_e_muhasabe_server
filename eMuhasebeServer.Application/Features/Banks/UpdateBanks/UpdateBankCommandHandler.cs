using AutoMapper;
using eMuhasebeServer.Application.Services;
using eMuhasebeServer.Domain.Entities;
using eMuhasebeServer.Domain.Repositories;
using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.Banks.UpdateBanks;

internal sealed class UpdateBankCommandHandler(
    IBankRepository bankRepository,
    IMapper mapper,
    IUnitOfWorkCompany unitOfWorkCompany,
    ICacheService cacheService
) : IRequestHandler<UpdateBankCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateBankCommand request, CancellationToken cancellationToken)
    {
        Bank? bank= await bankRepository.GetByExpressionWithTrackingAsync(x => x.Id == request.Id, cancellationToken);
        if (bank is null)
        {
            return Result<string>.Failure("Banka bulunamadı.");
        }

        if (bank.IBAN != request.IBAN)
        {
            if (await bankRepository.AnyAsync(x => x.IBAN == request.IBAN, cancellationToken))
            {
                return Result<string>.Failure("Bu IBAN numarası zaten kullanılmaktadır.");
            }
        }
        
        mapper.Map(request, bank);
        await unitOfWorkCompany.SaveChangesAsync(cancellationToken);
        cacheService.Remove("banks");
        return "Banka bilgileri başarıyla güncellendi.";
       
    }
}