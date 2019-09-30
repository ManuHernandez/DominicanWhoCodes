
using System;
using DominicanWhoCodes.Profiles.API.Application.DTO;
using DominicanWhoCodes.Profiles.API.Application.IntegrationEvents.Definitions;
using DominicanWhoCodes.Shared.EventBus;

namespace DominicanWhoCodes.Profiles.API.Application.IntegrationEvents
{
    public class CreationUserFailedIntegrationEvent : IIntegrationEvent, UserCreationFailed
    {
        public CreationUserFailedIntegrationEvent(UserSagaInstance user)
        {
            this.User = user;
        }
        public Guid Id => User.Id;

        public DateTime CreationDate => DateTime.Now;

        public UserSagaInstance User { get; private set; }
    }
}
