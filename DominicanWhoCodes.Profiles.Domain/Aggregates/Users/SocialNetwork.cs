
using System;
using DominicanWhoCodes.Shared.Domain;

namespace DominicanWhoCodes.Profiles.Domain.Aggregates.Users
{
    public class SocialNetwork : Entity
    {
        public SocialNetwork(SocialNetworkId socialNetworkId, UserId userId, string url,
            Network network)
        {
            SocialNetworkId = socialNetworkId;
            UserId = userId;
            Network = network;
            Url = FieldChecker.NotEmpty(url, nameof(url));
        }

        public SocialNetworkId SocialNetworkId { get; }
        public UserId UserId { get; }
        public Network Network { get; }
        public string Url { get; private set; }
        public User User { get; private set; }
        internal SocialNetwork UpdateUrl(string url)
        {
            this.Url = FieldChecker.NotEmpty(url, nameof(url));
            return this;
        }
    }
}
