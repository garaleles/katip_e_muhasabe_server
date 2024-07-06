using eMuhasebeServer.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eMuhasebeServer.Application.Features.Reports.CompanyCheckReports;

internal sealed class CompanyCheckReportsQueryHandler(
    ICompanyCheckAccountRepository companyCheckAccountRepository
) : IRequestHandler<CompanyCheckReportsQuery, Result<List<CompanyCheckReportsQueryResponse>>>
{
    public async Task<Result<List<CompanyCheckReportsQueryResponse>>> Handle(CompanyCheckReportsQuery request, CancellationToken cancellationToken)
    {
        var today = DateOnly.FromDateTime(DateTime.Now);
        var companyChecks = await companyCheckAccountRepository.GetAll()
            .Where(c => c.DueDate >= today)
            .Select(c => new CompanyCheckReportsQueryResponse
            {
                Id = c.Id,
                DueDate = c.DueDate,
                CheckNumber = c.CheckNumber,
                BankName = c.BankName,
                BranchName = c.BranchName,
                AccountNumber = c.AccountNumber,
                Amount = c.Amount
            })
            .ToListAsync(cancellationToken);

        return Result<List<CompanyCheckReportsQueryResponse>>.Succeed(companyChecks);
    }
}