using eMuhasebeServer.Domain.Enums;
using eMuhasebeServer.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eMuhasebeServer.Application.Features.Reports.CheckReports;

internal sealed class ChecksInPortfolioQueryHandler(
    ICheckRepository checkRepository
) : IRequestHandler<ChecksInPortfolioQuery, Result<List<ChecksInPortfolioQueryResponse>>>
{
    public async Task<Result<List<ChecksInPortfolioQueryResponse>>> Handle(ChecksInPortfolioQuery request, CancellationToken cancellationToken)
    {
        var checksInPortfolio = await checkRepository.GetAll()
            .Where(check => check.Status == CheckStatusEnum.InPortfolio)
            .Select(check => new ChecksInPortfolioQueryResponse
            {
                Id = check.Id,
                Status = check.Status,
                DueDate = check.DueDate,
                CheckNumber = check.CheckNumber,
                BankName = check.BankName,
                BranchName = check.BranchName,
                AccountNumber = check.AccountNumber,
                Amount = check.Amount,
                Debtor = check.Debtor,
                Creditor = check.Creditor,
                Endorser = check.Endorser
            })
            .ToListAsync(cancellationToken);

        return Result<List<ChecksInPortfolioQueryResponse>>.Succeed(checksInPortfolio);
    }
}