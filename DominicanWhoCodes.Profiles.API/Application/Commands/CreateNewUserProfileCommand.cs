

using DominicanWhoCodes.Shared.Application.DTO;
using MediatR;

namespace DominicanWhoCodes.Profiles.API.Application.Commands
{
    public class CreateNewUserProfileCommand : IRequest<bool>
    {
        public CreateNewUserProfileCommand(UserProfileDto userProfile)
        {
            UserProfile = userProfile;
        }
        public UserProfileDto UserProfile { get; private set; }
    }
}
