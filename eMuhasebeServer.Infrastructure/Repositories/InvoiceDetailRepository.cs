﻿using eMuhasebeServer.Domain.Entities;
using eMuhasebeServer.Domain.Repositories;
using eMuhasebeServer.Infrastructure.Context;
using GenericRepository;

namespace eMuhasebeServer.Infrastructure.Repositories;

public sealed class InvoiceDetailRepository: Repository<InvoiceDetail, CompanyDbContext>,IInvoiceDetailRepository
{
    public InvoiceDetailRepository(CompanyDbContext context) : base(context)
    {
    }
}