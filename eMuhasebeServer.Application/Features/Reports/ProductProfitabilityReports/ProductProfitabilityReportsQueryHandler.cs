using eMuhasebeServer.Domain.Entities;
using eMuhasebeServer.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eMuhasebeServer.Application.Features.Reports.ProductProfitabilityReports;

internal sealed class ProductProfitabilityReportsQueryHandler(
    IProductRepository productRepository
) : IRequestHandler<ProductProfitabilityReportsQuery, 
    Result<List<ProductProfitabilityReportsQueryResponse>>>
{
    public async Task<Result<List<ProductProfitabilityReportsQueryResponse>>> 
        Handle(ProductProfitabilityReportsQuery request, CancellationToken cancellationToken)
    {
        List<Product> products = await productRepository
            .Where(x=>x.Withdrawal > 0)
            .Include(x=>x.Details)
            .OrderBy(x=>x.Name)
            .ToListAsync(cancellationToken);
    
        List<ProductProfitabilityReportsQueryResponse> response = new();
        foreach (var product in products)
        {
            if (product.Details == null) continue;

            decimal depositPrice = product.Details.Where(x=>x.Deposit >0).Sum(x=>x.NetTotal);
            decimal withdrawalPrice = product.Details.Where(x=>x.Withdrawal >0).Sum(x=>x.NetTotal);
            decimal deposit = product.Details.Where(x=>x.Deposit >0).Sum(x=>x.Deposit);
            decimal withdrawal = product.Details.Where(x=>x.Withdrawal >0).Sum(x=>x.Withdrawal);

            if (deposit == 0 || withdrawal == 0) continue;

            decimal depositPricePerUnit = depositPrice / deposit;
            decimal withdrawalPricePerUnit = withdrawalPrice / withdrawal;
            decimal profitPerUnit = withdrawalPricePerUnit - depositPricePerUnit;
            decimal profitPercentage = profitPerUnit / depositPricePerUnit * 100;
        
            response.Add(new ProductProfitabilityReportsQueryResponse
            {
                Id = product.Id,
                Name = product.Name,
                DepositPrice = depositPricePerUnit,
                WithdrawalPrice = withdrawalPricePerUnit,
                Profit = profitPerUnit,
                ProfitPercentage = profitPercentage
            });
        }
        return Result<List<ProductProfitabilityReportsQueryResponse>>.Succeed(response);
    }
}