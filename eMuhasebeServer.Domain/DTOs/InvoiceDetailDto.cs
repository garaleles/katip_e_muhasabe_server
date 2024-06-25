namespace eMuhasebeServer.Domain.DTOs;

public class InvoiceDetailDto
{
    public Guid ProductId { get; set; }
    public decimal Quantity { get; set; }
    public decimal Price { get; set; }
    public int DiscountRate { get; set; }
    public int TaxRate { get; set; }
    public decimal BrutTotal { get; set; }
    public decimal DiscountTotal { get; set; }
    public decimal NetTotal { get; set; }
    public decimal TaxTotal { get; set; }
    public decimal GrandTotal { get; set; }
}