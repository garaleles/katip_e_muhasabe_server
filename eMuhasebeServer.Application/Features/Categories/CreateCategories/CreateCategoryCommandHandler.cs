using AutoMapper;
using eMuhasebeServer.Application.Services;
using eMuhasebeServer.Domain.Entities;
using eMuhasebeServer.Domain.Repositories;
using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.Categories.CreateCategories;

internal sealed class CreateCategoryCommandHandler(
    ICategoryRepository categoryRepository,
    IUnitOfWorkCompany unitOfWorkCompany,
    ICacheService cacheService,
    IMapper mapper
) : IRequestHandler<CreateCategoryCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        bool isNameExists = await categoryRepository.AnyAsync(x=>x.Name == request.Name, cancellationToken);
        if(isNameExists)
        {
            return Result<string>.Failure("Kategori daha önce kaydedilmiş.");
        }
            
        Category category = mapper.Map<Category>(request);
        await categoryRepository.AddAsync(category, cancellationToken);
        await unitOfWorkCompany.SaveChangesAsync(cancellationToken);
        cacheService.Remove("categories");
            
        return "Kategori başarıyla kaydedildi.";
    }
}