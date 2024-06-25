using eMuhasebeServer.Domain.Enums;
using eMuhasebeServer.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eMuhasebeServer.Application.Features.DashboardQuerys.GetAllPurchases;

internal sealed class GetAllPurchasesQueryHandler(
    IInvoiceRepository invoiceRepository
) : IRequestHandler<GetAllPurchasesQuery, Result<decimal>>
{
    public async Task<Result<decimal>> Handle(GetAllPurchasesQuery request, CancellationToken cancellationToken)
    {
        var purchasesInvoices = await invoiceRepository
            .Where(invoice => invoice.Type == InvoiceTypeEnum.Purchase)
            .Include(invoice => invoice.Details)
            .ToListAsync(cancellationToken);

        if (purchasesInvoices is null || !purchasesInvoices.Any())
        {
            return Result<decimal>.Failure("Alış bulunamadı.");
        }

        var totalNetAmount = purchasesInvoices
            .SelectMany(invoice => invoice.Details!)
            .Sum(detail => detail.NetTotal);

        return Result<decimal>.Succeed(totalNetAmount);
    }
}