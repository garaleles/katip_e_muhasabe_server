using AutoMapper;
using eMuhasebeServer.Application.Services;
using eMuhasebeServer.Domain.Entities;
using eMuhasebeServer.Domain.Events;
using eMuhasebeServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eMuhasebeServer.Application.Features.Users.CreateUser
{
    internal sealed class CreateUserCommandHandler(
        IMediator mediator,
        UserManager<AppUser> userManager, IMapper mapper, ICompanyUserRepository userRepository, IUnitOfWork unitOfWork, ICacheService cacheService) :
         IRequestHandler<CreateUserCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            bool isUserNameExists = await userManager.Users.AnyAsync(x => x.UserName == request.UserName, cancellationToken);
            if (isUserNameExists)
            {
                return Result<string>.Failure("Bu kullanıcı adı daha önce kullanılmış.");
            }

            bool isEmailExists = await userManager.Users.AnyAsync(x => x.Email == request.Email, cancellationToken);
            if (isEmailExists)
            {
                return Result<string>.Failure("Bu e-posta adresi daha önce kullanılmış.");
            }



            AppUser appUser = mapper.Map<AppUser>(request);


            IdentityResult identityResult = await userManager.CreateAsync(appUser, request.Password);
            if (!identityResult.Succeeded)
            {
                return Result<string>.Failure(identityResult.Errors.Select(x => x.Description).ToList());
            }

            List<CompanyUser> companyUsers = request.CompanyIds.Select(companyId => new CompanyUser
            {
                AppUserId = appUser.Id,
                CompanyId = companyId
            }).ToList();

            await userRepository.AddRangeAsync(companyUsers, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);



            cacheService.Remove("users");
            await mediator.Publish(new AppUserEvent(appUser.Id), cancellationToken);


            return "Kullanıcı başarıyla oluşturuldu.";

        }
    }

}
