using System.Text.Json;
using System.Text.Json.Serialization;
using eMuhasebeServer.Application.Services;
using eMuhasebeServer.Domain.Entities;
using eMuhasebeServer.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eMuhasebeServer.Application.Features.CompanyCheckissuePayrolls.GetAllCompanyCheckissuePayroll;

internal sealed class GetAllCompanyCheckissuePayrollQueryHandler : IRequestHandler<GetAllCompanyCheckissuePayrollQuery, Result<List<CompanyCheckissuePayroll>>>
{
    private readonly ICompanyCheckissuePayrollRepository companyCheckissuePayrollRepository;
    private readonly ICacheService cacheService;

    public GetAllCompanyCheckissuePayrollQueryHandler(ICompanyCheckissuePayrollRepository companyCheckissuePayrollRepository, ICacheService cacheService)
    {
        this.companyCheckissuePayrollRepository = companyCheckissuePayrollRepository;
        this.cacheService = cacheService;
    }
    
    public async Task<Result<List<CompanyCheckissuePayroll>>> Handle(GetAllCompanyCheckissuePayrollQuery request, CancellationToken cancellationToken)
    {
        List<CompanyCheckissuePayroll>? companyCheckissuePayrolls;
        string key = "companyCheckissuePayrolls";
        companyCheckissuePayrolls = cacheService.Get<List<CompanyCheckissuePayroll>>(key);

        if (companyCheckissuePayrolls is null)
        {
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles // Ignore circular references
            };

            companyCheckissuePayrolls =
                await companyCheckissuePayrollRepository
                    .GetAll()
                    .Include(p => p.Customer)
                    .Include(p => p.Details)
                    .OrderBy(p => p.Date)
                    .ToListAsync(cancellationToken);

            var json = JsonSerializer.Serialize(companyCheckissuePayrolls, options);
            cacheService.Set(key, companyCheckissuePayrolls);
        }
        return Result<List<CompanyCheckissuePayroll>>.Succeed(companyCheckissuePayrolls);
    }
}