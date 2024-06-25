using System.Text.Json.Serialization;
using eMuhasebeServer.Application.Services;
using eMuhasebeServer.Domain.Entities;
using eMuhasebeServer.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eMuhasebeServer.Application.Features.CheckRegisterPayrolls.GetAllCheckRegisterPayroll;

using System.Text.Json;

internal sealed class GetAllCheckRegisterPayrollQueryHandler : IRequestHandler<GetAllCheckRegisterPayrollQuery, Result<List<CheckRegisterPayroll>>>
{
    private readonly ICheckRegisterPayrollRepository checkRegisterPayrollRepository;
    private readonly ICacheService cacheService;

    public GetAllCheckRegisterPayrollQueryHandler(ICheckRegisterPayrollRepository checkRegisterPayrollRepository, ICacheService cacheService)
    {
        this.checkRegisterPayrollRepository = checkRegisterPayrollRepository;
        this.cacheService = cacheService;
    }

    public async Task<Result<List<CheckRegisterPayroll>>> Handle(GetAllCheckRegisterPayrollQuery request, CancellationToken cancellationToken)
    {
        List<CheckRegisterPayroll>? checkRegisterPayrolls;
        string key = "checkRegisterPayrolls";
        checkRegisterPayrolls = cacheService.Get<List<CheckRegisterPayroll>>(key);

        if (checkRegisterPayrolls is null)
        {
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles // Ignore circular references
            };

            checkRegisterPayrolls =
                await checkRegisterPayrollRepository
                    .GetAll()
                    .Include(p => p.Customer)
                    .Include(p => p.Details)
                    .OrderBy(p => p.Date)
                    .ToListAsync(cancellationToken);

            var json = JsonSerializer.Serialize(checkRegisterPayrolls, options);
            cacheService.Set(key, checkRegisterPayrolls);
        }
        return Result<List<CheckRegisterPayroll>>.Succeed(checkRegisterPayrolls);
    }
}
