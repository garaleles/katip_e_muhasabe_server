using eMuhasebeServer.Application.Services;
using eMuhasebeServer.Domain.Entities;
using eMuhasebeServer.Domain.Repositories;
using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.Categories.DeleteCategoryById;

public sealed record DeleteCategoryByIdCommand(
    Guid Id
    ): IRequest<Result<string>>;
    
    
    internal sealed class DeleteCategoryByIdCommandHandler(
        ICategoryRepository categoryRepository,
        IUnitOfWorkCompany unitOfWorkCompany,
        ICacheService cacheService
        ) : IRequestHandler<DeleteCategoryByIdCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(DeleteCategoryByIdCommand request, CancellationToken cancellationToken)
        {
            Category? category=await categoryRepository.GetByExpressionWithTrackingAsync(x=>x.Id==request.Id,cancellationToken);
            if(category is null)
                return Result<string>.Failure("Kategori bulunamadı.");
            
            category.IsDeleted=true;
            await unitOfWorkCompany.SaveChangesAsync(cancellationToken);
            cacheService.Remove("categories");
            return "Kategori başarıyla silindi.";
        }
    }