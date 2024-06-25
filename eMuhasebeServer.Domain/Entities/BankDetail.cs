using eMuhasebeServer.Domain.Abstractions;

namespace eMuhasebeServer.Domain.Entities;

public sealed class BankDetail : Entity
{
    public Guid BankId { get; set; }
    public string ProcessNumber { get; set; } = string.Empty;
    public Guid? CollectionId { get; set; }
    public Guid? PaymentId { get; set; }
    public DateOnly Date { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal DepositAmount { get; set; } //Giriş
    public decimal WithdrawalAmount { get; set; } //Çıkış

    public Guid? BankDetailId { get; set; }
    
    public Guid? CashRegisterDetailId { get; set; }
    public Guid? CustomerDetailId { get; set; }
    public Guid? ExpenseDetailId { get; set; }
    public Guid? ChequeissuePayrollId { get; set; }
    public List<ChequeissuePayrollDetail>? ChequeissuePayrollDetails { get; set; }
    

}
