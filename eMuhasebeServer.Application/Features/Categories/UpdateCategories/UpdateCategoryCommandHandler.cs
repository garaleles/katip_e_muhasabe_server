using AutoMapper;
using eMuhasebeServer.Application.Services;
using eMuhasebeServer.Domain.Entities;
using eMuhasebeServer.Domain.Repositories;
using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.Categories.UpdateCategories;

internal sealed class UpdateCategoryCommandHandler(
    ICategoryRepository categoryRepository,
    IUnitOfWorkCompany unitOfWorkCompany,
    IMapper mapper,
    ICacheService cacheService
) : IRequestHandler<UpdateCategoryCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        Category? category=await categoryRepository.GetByExpressionWithTrackingAsync(x=>x.Id==request.Id,cancellationToken);
        if(category is null)
            return Result<string>.Failure("Kategori bulunamadı.");
    
        if (category.Name != request.Name)
        {
            bool isNameExists = await categoryRepository.AnyAsync(x => x.Name == request.Name, cancellationToken);
            if (isNameExists)
            {
                return Result<string>.Failure("Kategori daha önce kaydedilmiş.");
            }
        }
                
        mapper.Map(request, category);
        await unitOfWorkCompany.SaveChangesAsync(cancellationToken);
        cacheService.Remove("categories");
        return "Kategori başarıyla güncellendi.";
    }
}