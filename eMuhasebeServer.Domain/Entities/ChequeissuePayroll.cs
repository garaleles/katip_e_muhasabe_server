using System.ComponentModel.DataAnnotations;
using eMuhasebeServer.Domain.Abstractions;
using eMuhasebeServer.Domain.Enums;

namespace eMuhasebeServer.Domain.Entities;

public class ChequeissuePayroll : Entity
{
    public DateOnly Date { get; set; }
    public string PayrollNumber { get; set; } = string.Empty;
    public Guid? CustomerId { get; set; }
    public Customer? Customer { get; set; }
    public Guid? CheckId { get; set; }
    public Check? Check { get; set; }
    public decimal PayrollAmount { get; set; }
    public string? Description { get; set; }
    public int CheckCount { get; set; }
    public DateOnly AverageMaturityDate { get; set; }

    public Guid? BankId { get; set; }
    public Bank? Bank { get; set; }

    public Guid? CashRegisterId { get; set; }
    public CashRegister? CashRegister { get; set; }

    // Diğer özellikler
    [Timestamp]
    public byte[] RowVersion { get; set; } = null!;
    public ICollection<ChequeissuePayrollDetail> Details { get; set; } = new List<ChequeissuePayrollDetail>();
}
