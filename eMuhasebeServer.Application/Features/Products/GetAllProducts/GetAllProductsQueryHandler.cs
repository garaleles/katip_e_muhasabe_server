using eMuhasebeServer.Application.Services;
using eMuhasebeServer.Domain.Entities;
using eMuhasebeServer.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eMuhasebeServer.Application.Features.Products.GetAllProducts;

internal sealed class GetAllProductsQueryHandler(
    IProductRepository productRepository,
    ICacheService cacheService
) : IRequestHandler<GetAllProductsQuery, Result<List<Product>>>
{
    public async Task<Result<List<Product>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        List<Product>? products = cacheService.Get<List<Product>>("products");
        if (products == null)
        {
            products = await productRepository.GetAll()
                .Include(p => p.Category) // Kategori bilgisini yükle
                .Include(p => p.Unit)    // Birim bilgisini yükle
                .OrderBy(x => x.Name)
                .ToListAsync(cancellationToken);
            cacheService.Set("products", products);
        }
        return products;
    }
}