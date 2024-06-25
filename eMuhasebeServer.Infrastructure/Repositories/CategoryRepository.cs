using eMuhasebeServer.Domain.Entities;
using eMuhasebeServer.Domain.Repositories;
using eMuhasebeServer.Infrastructure.Context;
using GenericRepository;

namespace eMuhasebeServer.Infrastructure.Repositories;

public sealed class CategoryRepository: Repository<Category, CompanyDbContext>, ICategoryRepository
{
    public CategoryRepository(CompanyDbContext context) : base(context)
    {
    }
}