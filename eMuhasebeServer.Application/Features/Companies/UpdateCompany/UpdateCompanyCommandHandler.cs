using AutoMapper;
using eMuhasebeServer.Application.Services;
using eMuhasebeServer.Domain.Entities;
using eMuhasebeServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.Companies.UpdateCompany
{
    internal sealed class UpdateCompanyCommandHandler(
        ICompanyRepository _companyRepository,
        IUnitOfWork _unitOfWork,
        ICacheService cacheService,
        IMapper _mapper
        ) : IRequestHandler<UpdateCompanyCommand, Result<string>>
    {

        public async Task<Result<string>> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
        {
            Company company = await _companyRepository.GetByExpressionWithTrackingAsync(x => x.Id == request.Id, cancellationToken);
            if (company is null)
            {
                return Result<string>.Failure("Şirket bulunamadı.");
            }

            bool isTaxNumberExists = await _companyRepository.AnyAsync(x => x.TaxNumber == request.TaxNumber && x.Id != request.Id, cancellationToken);
            if (isTaxNumberExists)
            {
                return Result<string>.Failure("Bu vergi numarası ile kayıtlı bir şirket bulunmaktadır.");
            }

            _mapper.Map(request, company);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            cacheService.Remove("companies");

            return "Şirket başarıyla güncellendi.";

        }
    }

}
