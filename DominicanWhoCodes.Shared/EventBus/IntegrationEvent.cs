
using Newtonsoft.Json;
using System;

namespace DominicanWhoCodes.Shared.EventBus
{
    public interface IIntegrationEvent
    {
        [JsonProperty]
        Guid Id { get; }
        [JsonProperty]
        DateTime CreationDate { get; }
    }
}
