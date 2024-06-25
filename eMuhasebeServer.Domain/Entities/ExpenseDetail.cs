using eMuhasebeServer.Domain.Abstractions;

namespace eMuhasebeServer.Domain.Entities;

public class ExpenseDetail: Entity
{
    public Guid ExpenseId { get; set; }
    public string ProcessNumber { get; set; } = string.Empty;
    public DateOnly Date { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal WithdrawalAmount { get; set; } //Çıkış
    public Guid? ExpenseDetailId { get; set; }
    public Guid? BankDetailId { get; set; }
    public Guid? CashRegisterDetailId { get; set; }

}