using eMuhasebeServer.Domain.Abstractions;

namespace eMuhasebeServer.Domain.Entities;

public sealed class Unit: Entity
{
    public string Name { get; set; } = string.Empty;
    
  //  public ICollection<Product> Products { get; set; } = new List<Product>(); // Yönlendirme Özelliği
    
}