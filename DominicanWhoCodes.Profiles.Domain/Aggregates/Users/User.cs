
namespace DominicanWhoCodes.Profiles.Domain.Aggregates.Users
{
    using DominicanWhoCodes.Shared.Domain;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class User : Entity<User>, IAggregateRoot
    {
        private List<SocialNetwork> _socialNetworks;
     
        public User(UserId userId, string firstName, string lastName, string email,
            string description)
        {
            Id = userId.Value;
            FirstName = FieldChecker.NotEmpty(firstName, nameof(firstName));
            LastName = FieldChecker.NotEmpty(lastName, nameof(lastName));
            Email = FieldChecker.NotEmpty(email, nameof(email));
            CurrentStatus = UserStatus.Pending;
            CreationDate = DateTime.UtcNow;
            Description = description;
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public DateTime CreationDate { get; private set; }
        public UserStatus CurrentStatus { get; private set; }
        public IReadOnlyCollection<SocialNetwork> SocialNetworks => _socialNetworks;
        public string Description { get; private set; }

        public SocialNetwork AddSocialNetwork(Network contactNetwork, string url)
        {
            var socialNetworkId = SocialNetworkId.FromGuid(Guid.NewGuid());
            var userId = UserId.FromGuid(Id);
            var newSocialNetwork = new SocialNetwork(socialNetworkId, userId, url, contactNetwork);

            if (_socialNetworks == null) _socialNetworks = new List<SocialNetwork>();
            var socialNetworkFound = _socialNetworks.FirstOrDefault(e => e.Network == contactNetwork);

            if (socialNetworkFound != null) newSocialNetwork = socialNetworkFound.UpdateUrl(url);
            else _socialNetworks.Add(newSocialNetwork);

            return newSocialNetwork;
        }
    }
}
