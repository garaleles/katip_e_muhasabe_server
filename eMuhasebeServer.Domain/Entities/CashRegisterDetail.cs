using eMuhasebeServer.Domain.Abstractions;

namespace eMuhasebeServer.Domain.Entities;

public sealed class CashRegisterDetail : Entity
{
    public Guid CashRegisterId { get; set; }
    public string ProcessNumber { get; set; } = string.Empty;
    public DateOnly Date { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal DepositAmount { get; set; } //Giriş
    public decimal WithdrawalAmount { get; set; } //Çıkış

    public Guid? CashRegisterDetailId { get; set; }
    public Guid? BankDetailId { get; set; }
    public Guid? CustomerDetailId { get; set; }
    public Guid? ExpenseDetailId { get; set; }
    public Guid? CollectionId { get; set; }
    public Guid? PaymentId { get; set; }
    public List<ChequeissuePayroll>? ChequeissuePayrolls { get; set; }
    public Guid? ChequeissuePayrollId { get; set; }
    public List<ChequeissuePayrollDetail>? ChequeissuePayrollDetails { get; set; }
}