
using System;
using DominicanWhoCodes.Profiles.API.Application.IntegrationEvents.Definitions;
using DominicanWhoCodes.Shared.EventBus;

namespace DominicanWhoCodes.Profiles.API.Application.IntegrationEvents
{
    public class CreationUserFailedIntegrationEvent : IIntegrationEvent, UserCreationFailed
    {
        public CreationUserFailedIntegrationEvent(Guid userId)
        {
            Id = userId;
        }
        public Guid Id { get; private set; }

        public DateTime CreationDate => DateTime.Now;

        public UserSagaInstance User { get; private set; }
    }
}
