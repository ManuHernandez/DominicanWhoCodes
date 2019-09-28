
using DominicanWhoCodes.Profiles.API.Application.DTO;
using DominicanWhoCodes.Shared.EventBus;

namespace DominicanWhoCodes.Profiles.API.Application.IntegrationEvents
{
    public class CreateNewUserProfileIntegrationEvent : IntegrationEvent
    {
        public CreateNewUserProfileIntegrationEvent(UserProfileDto userProfile)
        {
            UserProfile = userProfile;
        }

        public UserProfileDto UserProfile { get; private set; }
    }
}
