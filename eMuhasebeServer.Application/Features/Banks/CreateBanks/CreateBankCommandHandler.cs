using AutoMapper;
using eMuhasebeServer.Application.Services;
using eMuhasebeServer.Domain.Entities;
using eMuhasebeServer.Domain.Repositories;
using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.Banks.CreateBanks;

internal sealed class CreateBankCommandHandler(
    IBankRepository bankRepository,
    IUnitOfWorkCompany unitOfWorkCompany,
    IMapper mapper,
    ICacheService cacheService
) : IRequestHandler<CreateBankCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreateBankCommand request, CancellationToken cancellationToken)
    {
        bool isIbanExists = await bankRepository.AnyAsync(x => x.IBAN == request.IBAN, cancellationToken: cancellationToken);
        
        if (isIbanExists)
        {
            return Result<string>.Failure("Bu IBAN numarası zaten mevcut");
        }
        
        Bank bank= mapper.Map<Bank>(request);
        await bankRepository.AddAsync(bank, cancellationToken);
        await unitOfWorkCompany.SaveChangesAsync(cancellationToken);
        cacheService.Remove("banks");
        return "Banka başarıyla oluşturuldu";
    }
}