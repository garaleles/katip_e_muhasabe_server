﻿using eMuhasebeServer.Domain.Entities;
using eMuhasebeServer.Domain.Repositories;
using eMuhasebeServer.Infrastructure.Context;
using GenericRepository;

namespace eMuhasebeServer.Infrastructure.Repositories;

public sealed class CompanyUserRepository : Repository<CompanyUser, ApplicationDbContext>, ICompanyUserRepository
{
    public CompanyUserRepository(ApplicationDbContext context) : base(context)
    {
    }
}