using eMuhasebeServer.Domain.Abstractions;

namespace eMuhasebeServer.Domain.Entities;

public sealed class Category: Entity
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    
  //  public List<Product> Products { get; set; } = new List<Product>(); // Yönlendirme Özelliği
    
}