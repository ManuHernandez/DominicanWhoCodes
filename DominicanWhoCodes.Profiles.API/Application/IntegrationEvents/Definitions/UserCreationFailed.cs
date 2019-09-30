using DominicanWhoCodes.Profiles.API.Application.DTO;

namespace DominicanWhoCodes.Profiles.API.Application.IntegrationEvents.Definitions
{
    public interface UserCreationFailed
    {
        UserSagaInstance User { get; }
    }
}
