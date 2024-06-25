﻿using eMuhasebeServer.Domain.Entities;
using eMuhasebeServer.Domain.Repositories;
using eMuhasebeServer.Infrastructure.Context;
using GenericRepository;

namespace eMuhasebeServer.Infrastructure.Repositories;

public sealed class CustomerRepository:Repository<Customer, CompanyDbContext>, ICustomerRepository
{
    public CustomerRepository(CompanyDbContext context) : base(context)
    {
    }
}