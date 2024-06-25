using eMuhasebeServer.Domain.Abstractions;

namespace eMuhasebeServer.Domain.Entities;

public sealed class Product: Entity
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal SellingPrice { get; set; }
    public decimal PurchasePrice { get; set; }
    public decimal Deposit { get; set; }//Ürünün giriş miktarı
    public decimal Withdrawal { get; set; }//Ürünün çıkış miktarı
    public int DiscountRate { get; set; }
    public int PurchaseDiscountRate { get; set; }
    public int TaxRate { get; set; }
    public Guid CategoryId { get; set; } // Foreign Key (Yabancı Anahtar)
    public Guid UnitId { get; set; } // Foreign Key (Yabancı Anahtar)
   
    
    public Category? Category { get; set; } // Navigation Property (Yönlendirme Özelliği)
    public Unit? Unit { get; set; } // Navigation Property (Yönlendirme Özelliği)
    
    public List<ProductDetail>? Details { get; set; } // Navigation Property (Yönlendirme Özelliği)
   
}