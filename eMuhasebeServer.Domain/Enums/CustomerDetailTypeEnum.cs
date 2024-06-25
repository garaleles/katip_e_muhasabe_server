using Ardalis.SmartEnum;

namespace eMuhasebeServer.Domain.Enums;

public sealed class CustomerDetailTypeEnum: SmartEnum<CustomerDetailTypeEnum>
{
    public CustomerDetailTypeEnum(string name, int value) : base(name, value)
    {
    }
    
    public static readonly CustomerDetailTypeEnum Bank = new("Banka", 1);
    public static readonly CustomerDetailTypeEnum CashRegister = new("Kasa", 2);
    public static readonly CustomerDetailTypeEnum PurchaseInvoice = new("Alış Faturası", 3);
    public static readonly CustomerDetailTypeEnum SalesInvoice = new("Satış Faturası", 4);
    public static readonly CustomerDetailTypeEnum Check = new("Çek", 5);
  
}