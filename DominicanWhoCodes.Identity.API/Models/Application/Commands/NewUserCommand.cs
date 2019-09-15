

using DominicanWhoCodes.Identity.API.Models.Application.DTO;
using MediatR;

namespace DominicanWhoCodes.Identity.API.Models.Application.Commands
{
    public class NewUserCommand : IRequest<bool>
    {
        public NewUserDto NewUser { get; private set; }

        public NewUserCommand(NewUserDto newUser)
        {
            NewUser = newUser;
        }
    }
}
