using eMuhasebeServer.Domain.Abstractions;

namespace eMuhasebeServer.Domain.Entities;

public sealed class InvoiceDetail: Entity
{
    public Guid InvoiceId { get; set; }
    public Guid ProductId { get; set; }
    public Product? Product { get; set; }
    public decimal Quantity { get; set; }
    public decimal Price { get; set; }
    public int DiscountRate { get; set; }
    public int TaxRate { get; set; }
    public decimal BrutTotal { get; set; }
    public decimal DiscountTotal { get; set; }
    public decimal NetTotal { get; set; }
    public decimal TaxTotal { get; set; }
    public decimal GrandTotal { get; set; }
    
   // public Guid? ProductDetailId { get; set; }
    public ProductDetail? ProductDetail { get; set; }
    
}