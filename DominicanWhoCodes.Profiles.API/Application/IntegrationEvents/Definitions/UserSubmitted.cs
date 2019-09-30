

using DominicanWhoCodes.Profiles.API.Application.DTO;

namespace DominicanWhoCodes.Profiles.API.Application.IntegrationEvents.Definitions
{
    public interface UserSubmitted
    {
        UserSagaInstance User { get; }
    }
}
