using eMuhasebeServer.Domain.Abstractions;
using eMuhasebeServer.Domain.Enums;

namespace eMuhasebeServer.Domain.Entities;

public sealed class Customer: Entity
{
    public string Name { get; set; }= String.Empty;
    public CustomerTypeEnum Type { get; set; }= CustomerTypeEnum.Alicilar;
    public string District { get; set; }= String.Empty;
    public string City { get; set; }= String.Empty;
    public string FullAddress { get; set; }= String.Empty;
    public string Phone { get; set; }= String.Empty;
    public string TaxOffice { get; set; }= String.Empty;
    public string TaxNumber { get; set; }= String.Empty;
    public decimal DepositAmount { get; set; }
    public decimal WithdrawalAmount { get; set; }
    
    public List<CustomerDetail>? Details { get; set; }
    
}