using eMuhasebeServer.Domain.Enums;
using eMuhasebeServer.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eMuhasebeServer.Application.Features.DashboardQuerys.GetAllSales;

internal sealed class GetAllSalesQueryCommandHandler(
    IInvoiceRepository invoiceRepository
) : IRequestHandler<GetAllSalesQuery, Result<decimal>>
{
    public async Task<Result<decimal>> Handle(GetAllSalesQuery request, CancellationToken cancellationToken)
    {
        var salesInvoices = await invoiceRepository
            .Where(invoice => invoice.Type == InvoiceTypeEnum.Selling)
            .Include(invoice => invoice.Details)
            .ToListAsync(cancellationToken);

        if (salesInvoices is null || !salesInvoices.Any())
        {
            return Result<decimal>.Failure("Satış bulunamadı.");
        }

        var totalNetAmount = salesInvoices
            .SelectMany(invoice => invoice.Details!)
            .Sum(detail => detail.NetTotal);

        return Result<decimal>.Succeed(totalNetAmount);
    }
}