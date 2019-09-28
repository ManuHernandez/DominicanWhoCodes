
using Newtonsoft.Json;
using System;

namespace DominicanWhoCodes.Shared.EventBus
{
    public abstract class IntegrationEvent
    {
        public IntegrationEvent()
        {
            CreationDate = DateTime.UtcNow;
            Id = Guid.NewGuid();
        }
        [JsonProperty]
        public Guid Id { get; private set; }
        [JsonProperty]
        public DateTime CreationDate { get; set; }
    }
}
