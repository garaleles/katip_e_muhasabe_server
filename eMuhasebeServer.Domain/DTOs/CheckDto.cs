namespace eMuhasebeServer.Domain.DTOs;

public class CheckDto
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public string? Status { get; set; }  // Enum değeri yerine string olarak tanımlandı
}