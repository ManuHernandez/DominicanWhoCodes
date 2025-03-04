﻿
using System;
using DominicanWhoCodes.Profiles.API.Application.IntegrationEvents.Definitions;
using DominicanWhoCodes.Shared.EventBus;

namespace DominicanWhoCodes.Profiles.API.Application.IntegrationEvents
{
    public class CreateNewUserProfileIntegrationEvent : IIntegrationEvent, UserSubmitted
    {
        public CreateNewUserProfileIntegrationEvent(UserSagaInstance user)
        {
            User = user;
        }

        public UserSagaInstance User { get; private set; }

        public Guid Id => User.Id;

        public DateTime CreationDate => DateTime.Now;
    }
}
