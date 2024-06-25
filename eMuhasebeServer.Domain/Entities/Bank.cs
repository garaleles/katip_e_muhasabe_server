using eMuhasebeServer.Domain.Abstractions;
using eMuhasebeServer.Domain.Enums;

namespace eMuhasebeServer.Domain.Entities;

public sealed class Bank: Entity
{
    public string Name { get; set; }=String.Empty;
    public string IBAN { get; set; }=String.Empty;
    public CurrencyTypeEnum CurrencyType { get; set; } = CurrencyTypeEnum.TL;
    public decimal DepositAmount { get; set; } //Giriş
    public decimal WithdrawalAmount { get; set; } //Çıkış
    
    public List<BankDetail>? Details { get; set; }
    public List<ChequeissuePayroll>? ChequeissuePayrolls { get; set; }
  
}