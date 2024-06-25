using eMuhasebeServer.Domain.Entities;
using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.CompanyCheckAccounts.GetAllCompanyCheckAccounts;

public record GetAllCompanyCheckAccountsQuery() : IRequest<Result<List<CompanyCheckAccount>>>;