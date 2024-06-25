﻿using eMuhasebeServer.Domain.Entities;
using eMuhasebeServer.Domain.Repositories;
using eMuhasebeServer.Infrastructure.Context;
using GenericRepository;

namespace eMuhasebeServer.Infrastructure.Repositories;

public sealed class ProductDetailRepository: Repository<ProductDetail, CompanyDbContext>, IProductDetailRepository
{
    public ProductDetailRepository(CompanyDbContext context) : base(context)
    {
    }
}
