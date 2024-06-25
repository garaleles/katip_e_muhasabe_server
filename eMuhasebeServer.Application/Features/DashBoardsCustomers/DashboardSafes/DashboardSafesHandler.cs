using eMuhasebeServer.Domain.Enums;
using eMuhasebeServer.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eMuhasebeServer.Application.Features.DashBoardsCustomers.DashboardSafes;

internal sealed class DashboardSafesHandler : IRequestHandler<DashboardSafesQuery, Result<decimal>>
{
    private readonly ICashRegisterRepository _cashRegisterRepository;

    public DashboardSafesHandler(ICashRegisterRepository cashRegisterRepository)
    {
        _cashRegisterRepository = cashRegisterRepository;
    }

    public async Task<Result<decimal>> Handle(DashboardSafesQuery request, CancellationToken cancellationToken)
    {
        // Bütün kasaları(CashRegister) bul
        var cashRegisters = await _cashRegisterRepository.GetAll().ToListAsync(cancellationToken);

        // Bütün kasaların null kontrollerini yap
        if (cashRegisters is null || !cashRegisters.Any())
        {
            return Result<decimal>.Failure("Kasa bulunamadı.");
        }

        // Bütün kasaların CurrencyType = CurrencyTypeEnum.TL olanların DepositAmount toplamlarını al (Kasaya toplam giriş değeri)
        decimal totalDeposit = cashRegisters
            .Where(cr => cr.CurrencyType == CurrencyTypeEnum.TL)
            .Sum(cr => cr.DepositAmount);

        // Bütün kasaların CurrencyTypeCurrencyType = CurrencyTypeEnum.TL olanların WithdrawalAmount toplamlarını al(Kasadan toplam çıkış değeri)
        decimal totalWithdrawal = cashRegisters
            .Where(cr => cr.CurrencyType == CurrencyTypeEnum.TL)
            .Sum(cr => cr.WithdrawalAmount);

        // Bütün kasaların CurrencyTypeCurrencyType = CurrencyTypeEnum.TL olanların DepositAmount toplamından WithdrawalAmount toplamını çıkar ve bu rakamı decimalolarak döndür.(Kasa Bakiyesi)
        decimal balance = totalDeposit - totalWithdrawal;

        return Result<decimal>.Succeed(balance);
    }
}