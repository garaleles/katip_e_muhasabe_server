using eMuhasebeServer.Domain.Abstractions;
using eMuhasebeServer.Domain.Enums;

namespace eMuhasebeServer.Domain.Entities;


public class Check : Entity
{
    public CheckType CheckType { get; set; } = CheckType.Inward;
    public CheckStatus Status { get; set; } = CheckStatus.InPortfolio;
    public DateOnly DueDate { get; set; }
    public string CheckNumber { get; set; } = string.Empty;
    public string BankName { get; set; } = string.Empty;
    public string BranchName { get; set; } = string.Empty;
    public string AccountNumber { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string? Debtor { get; set; }
    public string? Creditor { get; set; }
    public string? Endorser { get; set; }
    public Guid? CheckDetailId { get; set; } // Nullable

    public Guid? CheckRegisterPayrollDetailId { get; set; }
    public CheckRegisterPayrollDetail? CheckRegisterPayrollDetail { get; set; }
    public CheckDetail? CheckDetail { get; set; } // CheckDetail property

    public Guid? ChequeissuePayrollId { get; set; } // Nullable
    public ChequeissuePayroll? ChequeissuePayroll { get; set; } // Nullable
}

