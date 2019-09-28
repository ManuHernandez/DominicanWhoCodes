

using DominicanWhoCodes.Shared.Domain;

namespace DominicanWhoCodes.Profiles.Domain.Aggregates.Users
{
    public class SocialNetwork : Entity
    {
        private SocialNetwork() { }
        public SocialNetwork(SocialNetworkId socialNetworkId, UserId userId, string url,
            Network network)
        {
            SocialNetworkId = socialNetworkId;
            UserId = userId;
            Network = network;
            Url = FieldChecker.NotEmpty(url, nameof(url));
        }

        public SocialNetworkId SocialNetworkId { get; private set; }
        public UserId UserId { get; private set; }
        public Network Network { get; private set; }
        public string Url { get; private set; }
        internal SocialNetwork UpdateUrl(string url)
        {
            this.Url = FieldChecker.NotEmpty(url, nameof(url));
            return this;
        }
    }
}
