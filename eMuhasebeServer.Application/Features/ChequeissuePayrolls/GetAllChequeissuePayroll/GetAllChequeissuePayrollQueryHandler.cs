using System.Text.Json;
using System.Text.Json.Serialization;
using eMuhasebeServer.Application.Services;
using eMuhasebeServer.Domain.Entities;
using eMuhasebeServer.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eMuhasebeServer.Application.Features.ChequeissuePayrolls.GetAllChequeissuePayroll;

internal sealed class GetAllChequeissuePayrollQueryHandler : IRequestHandler<GetAllChequeissuePayrollQuery, Result<List<ChequeissuePayroll>>>
{
    private readonly IChequeissuePayrollRepository chequeissuePayrollRepository;
    private readonly ICacheService cacheService;

    public GetAllChequeissuePayrollQueryHandler(IChequeissuePayrollRepository chequeissuePayrollRepository, ICacheService cacheService)
    {
        this.chequeissuePayrollRepository = chequeissuePayrollRepository;
        this.cacheService = cacheService;
    }
    
    public async Task<Result<List<ChequeissuePayroll>>> Handle(GetAllChequeissuePayrollQuery request, CancellationToken cancellationToken)
    {
        List<ChequeissuePayroll>? chequeissuePayrolls;
        string key = "chequeissuePayrolls";
        chequeissuePayrolls = cacheService.Get<List<ChequeissuePayroll>>(key);

        if (chequeissuePayrolls is null)
        {
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles // Ignore circular references
            };

            chequeissuePayrolls =
                await chequeissuePayrollRepository
                    .GetAll()
                    .Include(p => p.Customer)
                    .Include(p => p.Details)
                    .OrderBy(p => p.Date)
                    .ToListAsync(cancellationToken);

            var json = JsonSerializer.Serialize(chequeissuePayrolls, options);
            cacheService.Set(key, chequeissuePayrolls);
        }
        return Result<List<ChequeissuePayroll>>.Succeed(chequeissuePayrolls);
    }
}