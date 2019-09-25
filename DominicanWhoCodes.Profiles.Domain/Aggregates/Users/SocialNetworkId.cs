
using DominicanWhoCodes.Shared.Domain;
using System;

namespace DominicanWhoCodes.Profiles.Domain.Aggregates.Users
{
    public class SocialNetworkId : Id<SocialNetwork>
    {
        private SocialNetworkId() { }
        private SocialNetworkId(Guid value) : base(value) { }

        public static SocialNetworkId FromGuid(Guid socialNetworkId) => new SocialNetworkId(socialNetworkId);
    }
}
