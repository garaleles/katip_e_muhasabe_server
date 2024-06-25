using AutoMapper;
using eMuhasebeServer.Application.Services;
using eMuhasebeServer.Domain.Entities;
using eMuhasebeServer.Domain.Events;
using eMuhasebeServer.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eMuhasebeServer.Application.Features.Users.UpdateUser
{
    internal sealed class UpdateUserCommandHandler(IMediator mediator, UserManager<AppUser> userManager, IMapper mapper,
        ICompanyUserRepository companyUserRepository, IUnitOfWorkCompany unitOfWork, ICacheService cacheService
    ) :
        IRequestHandler<UpdateUserCommand, Result<string>>

    {
        public async Task<Result<string>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            AppUser? appUser =
                await userManager.Users
                    .Where(p => p.Id == request.Id)
                    .Include(p => p.CompanyUsers)
                    .FirstOrDefaultAsync(cancellationToken);


            bool isMailChanged = false;

            if (appUser is null)
            {
                return Result<string>.Failure("Kullanıcı bulunamadı.");
            }

            if (appUser.UserName != request.UserName)
            {
                bool isUserNameExist = await userManager.Users.AnyAsync(x => x.UserName == request.UserName, cancellationToken);
                if (isUserNameExist)
                {
                    return Result<string>.Failure("Kullanıcı adı zaten kullanımda.");
                }

                if (appUser.Email != request.Email)
                {
                    bool isEmailExist = await userManager.Users.AnyAsync(x => x.Email == request.Email, cancellationToken);
                    if (isEmailExist)
                    {
                        return Result<string>.Failure("E-posta adresi zaten kullanımda.");
                    }

                    isMailChanged = true;
                    appUser.EmailConfirmed = false;
                }

            }

            companyUserRepository.DeleteRange(appUser.CompanyUsers);

            List<CompanyUser> companyUsers = request.CompanyIds.Select(s => new CompanyUser
            {
                AppUserId = appUser.Id,
                CompanyId = s
            }).ToList();


            await companyUserRepository.AddRangeAsync(companyUsers, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            mapper.Map(request, appUser);
            IdentityResult result = await userManager.UpdateAsync(appUser);
            if (!result.Succeeded)
            {
                return Result<string>.Failure(result.Errors.Select(x => x.Description).ToList());

            }

            if (request.Password is not null)
            {
                string token = await userManager.GeneratePasswordResetTokenAsync(appUser);
                IdentityResult passwordResult = await userManager.ResetPasswordAsync(appUser, token, request.Password);
                if (!passwordResult.Succeeded)
                {
                    return Result<string>.Failure(passwordResult.Errors.Select(x => x.Description).ToList());
                }
            }


            cacheService.Remove("users");

            if (isMailChanged)
            {
                await mediator.Publish(new AppUserEvent(appUser.Id), cancellationToken);

            }



            return "Kullanıcı başarıyla güncellendi.";



        }
    }
}
