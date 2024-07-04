using eMuhasebeServer.Domain.Repositories;
using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.CompanyCheckissuePayrolls.GetByIdCompanyCheckissuePayroll;

internal sealed class GetByIdCompanyCheckissuePayrollQueryHandler(
    ICompanyCheckissuePayrollRepository companyCheckissuePayrollRepository
) : IRequestHandler<GetByIdCompanyCheckissuePayrollQuery, Result<string>>
{
    

    public async Task<Result<string>> Handle(GetByIdCompanyCheckissuePayrollQuery request, CancellationToken cancellationToken)
    {
        var companyCheckissuePayroll = await companyCheckissuePayrollRepository.GetCompanyCheckissuePayrollByIdAsync(request.Id);
        if (companyCheckissuePayroll == null)
        {
            return Result<string>.Failure("Çıkış bordrosu bulunamadı");
        }

        return Result<string>.Succeed("Çıkış bordrosu bulundu");
        
    }
}