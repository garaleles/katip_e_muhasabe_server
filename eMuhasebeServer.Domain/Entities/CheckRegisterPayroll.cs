using eMuhasebeServer.Domain.Abstractions;
using eMuhasebeServer.Domain.Enums;

namespace eMuhasebeServer.Domain.Entities;

public sealed class CheckRegisterPayroll : Entity
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
    public DateOnly AverageMaturityDate { get; set; } // Changed to DateOnly for consistency
   
    public ICollection<CheckRegisterPayrollDetail> Details { get; set; } = new List<CheckRegisterPayrollDetail>();
}

