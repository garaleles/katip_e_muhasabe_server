using eMuhasebeServer.Domain.Entities;
using eMuhasebeServer.Domain.Events;
using MediatR;
using Microsoft.AspNetCore.Identity;
using TS.Result;

namespace eMuhasebeServer.Application.Features.Auth.SendConfirmEmail
{
    internal sealed class SendConfirmEmailCommandHandler(UserManager<AppUser> userManager, IMediator mediator)
        : IRequestHandler<SendConfirmEmailCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(SendConfirmEmailCommand request, CancellationToken cancellationToken)
        {
            AppUser? appUser = await userManager.FindByEmailAsync(request.Email);
            if (appUser is null)
            {
                return "E-Posta adresi bulunamadı.";
            }
            if (appUser.EmailConfirmed)
            {
                return "E-Posta adresi zaten onaylanmış.";
            }

            await mediator.Publish(new AppUserEvent(appUser.Id), cancellationToken);

            return "E-Posta adresine onaylama linki gönderildi.";


        }
    }
}
