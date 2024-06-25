using eMuhasebeServer.Domain.Abstractions;

namespace eMuhasebeServer.Domain.Entities;

public class CompanyCheckAccount:Entity
{
    public string AccountName { get; set; }=string.Empty;
    public string AccountNumber { get; set; }=string.Empty;
    public string BankName { get; set; }=string.Empty;
    public string BranchName { get; set; }=string.Empty;
    public string? Description { get; set; }
    public decimal Amount { get; set; }
}