using System.ComponentModel.DataAnnotations.Schema;
using Ardalis.SmartEnum;

namespace eMuhasebeServer.Domain.Enums;


public sealed class CheckType : SmartEnum<CheckType>
{
    public CheckType(string name, int value) : base(name, value) { }

    public static readonly CheckType Inward = new("Müşteri Çeki", 1);
    public static readonly CheckType Outward = new("Kendi Çekimiz", 2);

    public static CheckType FromNameOrDefault(string name)
    {
        return name switch
        {
            "Müşteri Çeki" => Inward,
            "Kendi Çekimiz" => Outward,
            _ => Inward // Varsayılan değer
        };
    }
}