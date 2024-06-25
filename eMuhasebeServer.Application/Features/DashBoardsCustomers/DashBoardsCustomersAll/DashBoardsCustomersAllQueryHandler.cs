using eMuhasebeServer.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eMuhasebeServer.Application.Features.DashBoardsCustomers.DashBoardsCustomersAll;

internal sealed class DashBoardsCustomersAllQueryHandler(
    ICustomerRepository customerRepository
) : IRequestHandler<DashBoardsCustomersAllQuery, Result<decimal>>
{
    public async Task<Result<decimal>> Handle(DashBoardsCustomersAllQuery request, CancellationToken cancellationToken)
    {
        // Tüm Customer nesnelerini al ve detaylarını dahil et
        var customers = await customerRepository.GetAllWithTacking()
            .Include(x => x.Details)
            .ToListAsync(cancellationToken: cancellationToken);

        if (customers is null || !customers.Any())
        {
            return Result<decimal>.Failure("Müşteri bulunamadı.");
        }

        // DepositAmount toplamı, WithdrawalAmount toplamından fazla olan Customer nesnelerini seç
        var customersWithHigherDeposit = customers
            .Where(c => c.Details!.Sum(d => d.DepositAmount) > c.Details!.Sum(d => d.WithdrawalAmount));

        if (!customersWithHigherDeposit.Any())
        {
            return Result<decimal>.Failure("Borç toplamı, alacak toplamından fazla olan müşteri bulunamadı.");
        }

        // Seçilen Customer nesnelerinin DepositAmount bakiyelerinin toplamını hesapla
        decimal totalDepositAmount = customersWithHigherDeposit.Sum(c => c.Details!.Sum(d => d.DepositAmount));
        
        // Seçilen Customer nesnelerinin WithdrawalAmount bakiyelerinin toplamını hesapla
        decimal totalWithdrawalAmount = customersWithHigherDeposit.Sum(c => c.Details!.Sum(d => d.WithdrawalAmount));
        
        //totalDepositAmount değerinden totalWithdrawalAmount değerini çıkar ve borç bakiyyesi olarak döndür
        decimal debtBalance= totalDepositAmount - totalWithdrawalAmount;

        // Toplam DepositAmount bakiyesini döndür
        return Result<decimal>.Succeed(debtBalance);
    }
}