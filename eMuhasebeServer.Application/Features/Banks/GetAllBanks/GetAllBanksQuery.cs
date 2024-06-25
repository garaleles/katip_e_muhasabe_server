using AutoMapper;
using eMuhasebeServer.Application.Services;
using eMuhasebeServer.Domain.Entities;
using MediatR;
using TS.Result;

namespace eMuhasebeServer.Application.Features.GetAllBanks;

public sealed record GetAllBanksQuery() : IRequest<Result<List<Bank>>>;