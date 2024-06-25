﻿using eMuhasebeServer.Domain.Entities;
using eMuhasebeServer.Domain.Repositories;
using eMuhasebeServer.Infrastructure.Context;
using GenericRepository;

namespace eMuhasebeServer.Infrastructure.Repositories;

public sealed class CashRegisterRepository: Repository<CashRegister, CompanyDbContext>, ICashRegisterRepository
{
    public CashRegisterRepository(CompanyDbContext context) : base(context)
    {
    }
}