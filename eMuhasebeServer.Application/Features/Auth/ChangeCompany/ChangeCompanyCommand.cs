using System.Security.Claims;
using eMuhasebeServer.Application.Features.Auth.Login;
using eMuhasebeServer.Application.Services;
using eMuhasebeServer.Domain.Entities;
using eMuhasebeServer.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eMuhasebeServer.Application.Features.Auth.ChangeCompany;

public sealed record ChangeCompanyCommand(Guid CompanyId) : IRequest<Result<LoginCommandResponse>>;



internal sealed class ChangeCompanyCommandHandler(
    ICompanyUserRepository companyUserRepository,
    UserManager<AppUser> userManager,
    IHttpContextAccessor httpContextAccessor,
    IJwtProvider jwtProvider,
    ICacheService cacheService
    ) : IRequestHandler<ChangeCompanyCommand, Result<LoginCommandResponse>>
{


    public async Task<Result<LoginCommandResponse>> Handle(ChangeCompanyCommand request, CancellationToken cancellationToken)
    {
        if (httpContextAccessor.HttpContext is null)
        {
            return Result<LoginCommandResponse>.Failure("Bu işlemi yapmaya yetkiniz yok!");
        }

        string? userIdString = httpContextAccessor.HttpContext.User.FindFirstValue("Id");

        if (string.IsNullOrEmpty(userIdString))
        {
            return Result<LoginCommandResponse>.Failure("Bu işlemi yapmaya yetkiniz yok!");
        }

        AppUser? appuser = await userManager.FindByIdAsync(userIdString);

        if (appuser is null)
        {
            return Result<LoginCommandResponse>.Failure("Kullanıcı Bulunamadı!");
        }

        List<CompanyUser> companyUsers = await companyUserRepository.Where(x => x.AppUserId == appuser.Id)
            .Include(x => x.Company).ToListAsync(cancellationToken);

        List<Company> companies = companyUsers.Select(x => new Company
        {
            Id = x.CompanyId,
            Name = x.Company!.Name,
            TaxOffice = x.Company.TaxOffice,
            TaxNumber = x.Company.TaxNumber,
            FullAddress = x.Company.FullAddress,

        }).ToList();

        var response = await jwtProvider.CreateToken(appuser, request.CompanyId, companies);
        
        cacheService.RemoveAll();

        return response;

    }
}