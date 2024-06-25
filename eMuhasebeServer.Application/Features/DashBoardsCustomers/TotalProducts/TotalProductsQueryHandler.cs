using eMuhasebeServer.Domain.Entities;
using eMuhasebeServer.Domain.Enums;
using eMuhasebeServer.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eMuhasebeServer.Application.Features.DashBoardsCustomers.TotalProducts;

internal sealed class TotalProductsQueryHandler(
    IProductRepository productRepository
) : IRequestHandler<TotalProductsQuery, Result<decimal>>
{
    public async Task<Result<decimal>> Handle(TotalProductsQuery request, CancellationToken cancellationToken)
    {
        // Alış faturalarıyla giren Product nesnelerini al
        var purchaseProducts = await productRepository.GetAllWithTacking()
            .Include(x => x.Details!)
            .ThenInclude(d => d.Invoice)
            .Where(p => p.Details!.Any(d =>
                d.InvoiceId.HasValue && d.Invoice != null && d.Invoice.Type == InvoiceTypeEnum.Purchase))
            .ToListAsync(cancellationToken: cancellationToken);

        // Satış faturalarıyla çıkan Product nesnelerini al
        var sellingProducts = await productRepository.GetAllWithTacking()
            .Include(x => x.Details!)
            .ThenInclude(d => d.Invoice)
            .Where(p => p.Details!.Any(d =>
                d.InvoiceId.HasValue && d.Invoice != null && d.Invoice.Type == InvoiceTypeEnum.Selling))
            .ToListAsync(cancellationToken: cancellationToken);

        if (purchaseProducts is null || !purchaseProducts.Any())
        {
            return Result<decimal>.Failure("Alış Ürünü bulunamadı.");
        }

        if (sellingProducts is null || !sellingProducts.Any())
        {
            return Result<decimal>.Failure("Satış Ürünü bulunamadı.");
        }

        decimal totalStockValue = 0;

        foreach (var product in purchaseProducts)
        {
            // Her bir ürünün Toplam deposit(giriş) miktarını bul
            decimal totalDeposit = product.Details!
                .Where(d => d.InvoiceId.HasValue && d.Invoice != null && d.Invoice.Type == InvoiceTypeEnum.Purchase)
                .Sum(d => d.Deposit);

            // Her bir ürünün Toplam Withdrawal(çıkış) miktarını bul
            decimal totalWithdrawal = product.Details!
                .Where(d => d.InvoiceId.HasValue && d.Invoice != null && d.Invoice.Type == InvoiceTypeEnum.Selling)
                .Sum(d => d.Withdrawal);

            // Her bir ürünün stok miktarını bul
            decimal stockAmount = totalDeposit - totalWithdrawal;

            // Her bir ürünün ortalama alış değerini bul
            decimal averagePurchaseValue = totalDeposit != 0
                ? product.Details!
                    .Where(d => d.InvoiceId.HasValue && d.Invoice != null && d.Invoice.Type == InvoiceTypeEnum.Purchase)
                    .Sum(d => d.NetTotal) / totalDeposit
                : 0;

            // Her bir ürünün stok değerini bul ve genel stok değerine ekle
            totalStockValue += stockAmount * averagePurchaseValue;
        }

        return Result<decimal>.Succeed(totalStockValue);
    }
}