using DominicanWhoCodes.Shared.Application.DTO;
using DominicanWhoCodes.Shared.EventBus;

namespace DominicanWhoCodes.Shared.IntegrationEvents
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
