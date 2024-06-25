using eMuhasebeServer.Domain.Enums;
using eMuhasebeServer.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eMuhasebeServer.Application.Features.DashBoardsCustomers.DashboardBanks;

public record DashboardBanksQuery(): IRequest<Result<decimal>>;

internal sealed class DashboardBanksHandler : IRequestHandler<DashboardBanksQuery, Result<decimal>>
{
    private readonly IBankRepository _bankRepository;

    public DashboardBanksHandler(IBankRepository bankRepository)
    {
        _bankRepository = bankRepository;
    }

    public async Task<Result<decimal>> Handle(DashboardBanksQuery request, CancellationToken cancellationToken)
    {
        // Bütün bankaları(Bank) bul
        var banks = await _bankRepository.GetAll().ToListAsync(cancellationToken);

        // Bütün bankaların null kontrollerini yap
        if (banks is null || !banks.Any())
        {
            return Result<decimal>.Failure("Banka bulunamadı.");
        }

        // Bütün bankaların CurrencyType = CurrencyTypeEnum.TL olanların DepositAmount toplamlarını al (Bankaya toplam giriş değeri)
        decimal totalDeposit = banks
            .Where(b => b.CurrencyType == CurrencyTypeEnum.TL)
            .Sum(b => b.DepositAmount);

        // Bütün bankaların CurrencyTypeCurrencyType = CurrencyTypeEnum.TL olanların WithdrawalAmount toplamlarını al(Bankadan toplam çıkış değeri)
        decimal totalWithdrawal = banks
            .Where(b => b.CurrencyType == CurrencyTypeEnum.TL)
            .Sum(b => b.WithdrawalAmount);

        // Bütün bankaların CurrencyTypeCurrencyType = CurrencyTypeEnum.TL olanların DepositAmount toplamından WithdrawalAmount toplamını çıkar ve bu rakamı decimalolarak döndür.(Banka Bakiyesi)
        decimal balance = totalDeposit - totalWithdrawal;

        return Result<decimal>.Succeed(balance);
    }
}