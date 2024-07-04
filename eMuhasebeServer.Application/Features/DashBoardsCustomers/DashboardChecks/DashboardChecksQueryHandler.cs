using eMuhasebeServer.Domain.Enums;
using eMuhasebeServer.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eMuhasebeServer.Application.Features.DashBoardsCustomers.DashboardChecks;

internal sealed class DashboardChecksQueryHandler : IRequestHandler<DashboardChecksQuery, Result<decimal>>
{
    private readonly ICheckRepository _checkRepository;

    public DashboardChecksQueryHandler(ICheckRepository checkRepository)
    {
        _checkRepository = checkRepository;
    }

    public async Task<Result<decimal>> Handle(DashboardChecksQuery request, CancellationToken cancellationToken)
    {
        // Çeklerin toplam değerini hesapla
        var totalAmount = await _checkRepository.GetAll()
            .Where(c => c.Status == CheckStatusEnum.InPortfolio)
            .SumAsync(c => c.Amount, cancellationToken);

        return Result<decimal>.Succeed(totalAmount);
    }
}