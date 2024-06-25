using eMuhasebeServer.Domain.Abstractions;

namespace eMuhasebeServer.Domain.Entities;

public class ChequeissuePayrollDetail : Entity
{
    public Guid ChequeissuePayrollId { get; set; }
    public string CheckNumber { get; set; } = string.Empty;
    public string BankName { get; set; } = string.Empty;
    public string BranchName { get; set; } = string.Empty;
    public string AccountNumber { get; set; } = string.Empty;
    public DateOnly DueDate { get; set; }
    public decimal Amount { get; set; }
    public string? Description { get; set; }
    public string? Debtor { get; set; }
    public string? Creditor { get; set; }
    public string? Endorser { get; set; }
    public int Status { get; set; } = 7;

    public Guid? BankDetailId { get; set; }
    public BankDetail? BankDetail { get; set; }
    // Check ile ilişkilendirilecek yeni CheckId alanı
    public Guid? CheckId { get; set; }


    public Guid? CashRegisterDetailId { get; set; }
    public CashRegisterDetail? CashRegisterDetail { get; set; }

    public ICollection<Check> Checks { get; set; } = new List<Check>();
    public ICollection<CheckDetail> CheckDetails { get; set; } = new List<CheckDetail>();
}
