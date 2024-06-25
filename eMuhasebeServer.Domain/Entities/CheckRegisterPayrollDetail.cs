using eMuhasebeServer.Domain.Abstractions;
using eMuhasebeServer.Domain.Enums;

namespace eMuhasebeServer.Domain.Entities;


public class CheckRegisterPayrollDetail : Entity
{
    public Guid CheckRegisterPayrollId { get; set; }
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
    
    public ICollection<Check> Checks { get; set; } = new List<Check>();
    public ICollection<CheckDetail> CheckDetails { get; set; } = new List<CheckDetail>();
}

