using eMuhasebeServer.Application.Services;
using eMuhasebeServer.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using TS.Result;

namespace eMuhasebeServer.Application.Features.Users.DeleteUserById
{
    internal sealed class DeleteUserByIdCommandHandler(UserManager<AppUser> userManager, ICacheService cacheService) :
        IRequestHandler<DeleteUserByIdCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(DeleteUserByIdCommand request, CancellationToken cancellationToken)
        {
            AppUser? appUser = await userManager.FindByIdAsync(request.Id.ToString());
            if (appUser is null)
            {
                return Result<string>.Failure("Kullanıcı bulunamadı.");
            }

            appUser.IsDeleted = true;

            IdentityResult result = await userManager.UpdateAsync(appUser);
            if (!result.Succeeded)
            {
                return Result<string>.Failure(result.Errors.Select(x => x.Description).ToList());
            }

            cacheService.Remove("users");
            return "Kullanıcı başarıyla silindi.";

        }
    }

}
