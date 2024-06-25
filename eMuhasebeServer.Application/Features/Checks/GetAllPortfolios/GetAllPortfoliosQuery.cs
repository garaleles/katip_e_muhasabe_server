using eMuhasebeServer.Domain.Entities;
using eMuhasebeServer.Domain.Enums;
using eMuhasebeServer.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eMuhasebeServer.Application.Features.Checks.GetAllPortfolios;

public sealed record GetAllPortfoliosQuery(): IRequest<Result<List<Check>>>;


internal sealed class GetAllPortfoliosQueryHandler : IRequestHandler<GetAllPortfoliosQuery, Result<List<Check>>>
{
    private readonly ICheckRepository _checkRepository;

    public GetAllPortfoliosQueryHandler(ICheckRepository checkRepository)
    {
        _checkRepository = checkRepository;
    }

    public async Task<Result<List<Check>>> Handle(GetAllPortfoliosQuery request, CancellationToken cancellationToken)
    {
        try
        {
            List<Check> checks = await _checkRepository.GetAll().
                Where(x => x.Status == CheckStatus.InPortfolio).
                OrderBy(x => x.DueDate).
                ToListAsync(cancellationToken);

            // Check if the checks list is null
            if (checks == null)
            {
                return Result<List<Check>>.Failure("Portfoyde çek bulunamadı.");
            }

            return Result<List<Check>>.Succeed(checks);
        }
        catch (Exception ex)
        {
            // Return the exception as a failure result
            return Result<List<Check>>.Failure(ex.Message);
        }
    }
}