using eMuhasebeServer.Domain.Abstractions;
using eMuhasebeServer.Domain.Enums;

namespace eMuhasebeServer.Domain.Entities;

public class Expense: Entity
{
    public string Name { get; set; }=String.Empty;
    public string Description { get; set; }=String.Empty;
    public CurrencyTypeEnum CurrencyType { get; set; } = CurrencyTypeEnum.TL;
    public decimal WithdrawalAmount { get; set; } //Çıkış
    public List<ExpenseDetail>? Details { get; set; }
}