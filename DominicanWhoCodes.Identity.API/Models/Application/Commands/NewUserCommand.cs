

using DominicanWhoCodes.Identity.API.Models.Application.InputModels;
using DominicanWhoCodes.Shared.Application.DTO;
using MediatR;

namespace DominicanWhoCodes.Identity.API.Models.Application.Commands
{
    public class NewUserCommand : IRequest<bool>
    {
        public UserProfileDto NewUser { get; private set; }
        public string Password { get; private set; }
        public NewUserCommand(UserProfileDto newUser, string password)
        {
            NewUser = newUser;
            Password = password;
        }
    }
}
