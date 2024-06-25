using Ardalis.SmartEnum;

namespace eMuhasebeServer.Domain.Enums;

public sealed class CheckRegisterPayrollType: SmartEnum<CheckRegisterPayrollType>
{
    public static readonly CheckRegisterPayrollType Inward = new("Giriş Bordrosu", 1);
    public static readonly CheckRegisterPayrollType Outward = new("Çıkış Bordrosu", 2);
    
    public CheckRegisterPayrollType(string name, int value) : base(name, value)
    {
    }
}