using eMuhasebeServer.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eMuhasebeServer.Application.Features.DashBoardsCustomers.DashboardCustomersWithDrawalAll;

internal sealed class DashboardCustomersWithDrawalAllQueryHandler(
    ICustomerRepository customerRepository
) : IRequestHandler<DashboardCustomersWithDrawalAllQuery, Result<decimal>>
{
    public async Task<Result<decimal>> Handle(DashboardCustomersWithDrawalAllQuery request, CancellationToken cancellationToken)
    {
        // Tüm Customer nesnelerini al ve detaylarını dahil et
        var customers = await customerRepository.GetAllWithTacking()
            .Include(x => x.Details)
            .ToListAsync(cancellationToken: cancellationToken);

        if (customers is null || !customers.Any())
        {
            return Result<decimal>.Failure("Müşteri bulunamadı.");
        }

        // WithdrawalAmount toplamı, DepositAmount toplamından fazla olan Customer nesnelerini seç
        var customersWithHigherWithDrawal = customers
            .Where(c => c.Details!.Sum(d => d.WithdrawalAmount) > c.Details!.Sum(d => d.DepositAmount));

        if (!customersWithHigherWithDrawal.Any())
        {
            return Result<decimal>.Failure("Alacak toplamı, borç toplamından fazla olan müşteri bulunamadı.");
        }

        // Seçilen Customer nesnelerinin WithdrawalAmount bakiyelerinin toplamını hesapla
        decimal totalWithdrawalAmount = customersWithHigherWithDrawal.Sum(c => c.Details!.Sum(d => d.WithdrawalAmount));
        
        // Seçilen Customer nesnelerinin WithdrawalAmount bakiyelerinin toplamını hesapla
        decimal totalDebtAmount = customersWithHigherWithDrawal.Sum(c => c.Details!.Sum(d => d.DepositAmount));
        
        //totalDepositAmount değerinden totalWithdrawalAmount değerini çıkar ve borç bakiyyesi olarak döndür
        decimal withBalance= totalWithdrawalAmount - totalDebtAmount;

        // Toplam DepositAmount bakiyesini döndür
        return Result<decimal>.Succeed(withBalance);
    }
}