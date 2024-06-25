using eMuhasebeServer.Application.Services;
using eMuhasebeServer.Domain.Entities;
using eMuhasebeServer.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eMuhasebeServer.Application.Features.CompanyCheckAccounts.GetAllCompanyCheckAccounts;

internal sealed class GetAllCompanyCheckAccountsQueryHandler(
    ICompanyCheckAccountRepository companyCheckAccountRepository,
    ICacheService cacheService
) : IRequestHandler<GetAllCompanyCheckAccountsQuery, Result<List<CompanyCheckAccount>>>
{
    public async Task<Result<List<CompanyCheckAccount>>> Handle(GetAllCompanyCheckAccountsQuery request, CancellationToken cancellationToken)
    {
        List<CompanyCheckAccount>? companyCheckAccounts;
        companyCheckAccounts = cacheService.Get<List<CompanyCheckAccount>>("companyCheckAccounts");



        if (companyCheckAccounts is null)
        {
            companyCheckAccounts =await companyCheckAccountRepository.GetAll()
                .OrderBy(x=>x.AccountName)
                .ToListAsync(cancellationToken);
            cacheService.Set("companyCheckAccounts", companyCheckAccounts);
        }
        return companyCheckAccounts;
    }
}