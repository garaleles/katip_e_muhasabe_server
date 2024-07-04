using eMuhasebeServer.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eMuhasebeServer.Application.Features.DashBoardsCustomers.DashboardCompanyChecks;

internal sealed class DashboardCompanyChecksQueryHandler : IRequestHandler<DashboardCompanyChecksQuery, Result<decimal>>
{
    private readonly ICompanyCheckAccountRepository _companyCheckAccountRepository;

    public DashboardCompanyChecksQueryHandler(ICompanyCheckAccountRepository companyCheckAccountRepository)
    {
        _companyCheckAccountRepository = companyCheckAccountRepository;
    }

    public async Task<Result<decimal>> Handle(DashboardCompanyChecksQuery request, CancellationToken cancellationToken)
    {
        var today = DateOnly.FromDateTime(DateTime.Now);
        var totalAmount = await _companyCheckAccountRepository.GetAll()
            .Where(c => c.DueDate >= today)
            .SumAsync(c => c.Amount, cancellationToken);

        return Result<decimal>.Succeed(totalAmount);
    }
}