using eMuhasebeServer.Application.Services;
using eMuhasebeServer.Domain.Entities;
using eMuhasebeServer.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eMuhasebeServer.Application.Features.Categories.GetAllCategories;

internal sealed class GetAllCategoriesQueryHandler(
    ICategoryRepository categoryRepository,
    ICacheService cacheService
) : IRequestHandler<GetAllCategoriesQuery, Result<List<Category>>>
{
    public async Task<Result<List<Category>>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        List<Category>? categories= cacheService.Get<List<Category>>("categories");
        if (categories == null)
        {
            categories = await categoryRepository.GetAll().OrderBy(x=>x.Name).ToListAsync(cancellationToken);
            cacheService.Set("categories", categories);
        }
        return categories;
        
    }
}