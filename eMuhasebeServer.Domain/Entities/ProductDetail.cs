using eMuhasebeServer.Domain.Abstractions;

namespace eMuhasebeServer.Domain.Entities;

public sealed class ProductDetail: Entity
{
    public Guid ProductId { get; set; }
    public DateOnly Date { get; set; }
    public string InvoiceNumber { get; set; }=string.Empty;
    public string Description { get; set; }=string.Empty;
    public decimal Price { get; set; }
    public decimal Deposit { get; set; }//Ürünün giriş miktarı
    public decimal Withdrawal { get; set; }//Ürünün çıkış miktarı
    public int DiscountRate { get; set; }
    public int TaxRate { get; set; }
    public decimal BrutTotal { get; set; }
    public decimal DiscountTotal { get; set; }
    public decimal NetTotal { get; set; }
    public decimal TaxTotal { get; set; }
    public decimal GrandTotal { get; set; }
    public Guid? InvoiceDetailId { get; set; }
    public Guid? InvoiceId { get; set; }
    public Invoice? Invoice { get; set; }
   
    public List<ProductDetail>? Details { get; set; }
    
    
    
    
}