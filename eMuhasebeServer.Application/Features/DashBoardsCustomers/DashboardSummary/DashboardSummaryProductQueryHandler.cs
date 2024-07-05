using eMuhasebeServer.Domain.Repositories;
using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.DashBoardsCustomers.DashboardSummary;

internal sealed class DashboardSummaryProductQueryHandler(
    IProductRepository productRepository
) : IRequestHandler<DashboardSummaryProductQuery, Result<int>>
{
    public async Task<Result<int>> Handle(DashboardSummaryProductQuery request, CancellationToken cancellationToken)
    {
        var productCount = await productRepository.GetProductCount();
        return Result<int>.Succeed(productCount);
    }
}