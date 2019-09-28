

using DominicanWhoCodes.Profiles.Domain.Aggregates.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace DominicanWhoCodes.Profiles.UnitTests.Domain
{
    public partial class UserAggregateTest
    {
        [Fact]
        public void Add_Social_Network_Success()
        {
            //Act
            var newUser = new User(_userId, _firstName, _lastName, _email, _description);
            //Arrange
            SocialNetwork socialNetworkAdded = newUser.AddSocialNetwork(contactNetwork: Network.Twitter,
                url: "https://twitter.com/ManuHdez_");
            //Assert
            Assert.NotNull(socialNetworkAdded);
        }

        [Fact]
        public void SocialNetwork_If_Exist_Should_Not_Be_Duplicate_Instead_Must_Be_Updated()
        {
            //Act
            var newUser = new User(_userId, _firstName, _lastName, _email, _description);
            string updatedUrl = "https://twitter.com/ManuHdez01_";
            //Arrange
            SocialNetwork socialNetworkAdded = newUser.AddSocialNetwork(contactNetwork: Network.Twitter,
                url: "https://twitter.com/ManuHdez_");
            SocialNetwork socialNetworkUpdated = newUser.AddSocialNetwork(contactNetwork: Network.Twitter,
                url: updatedUrl);
            //Assert
            IReadOnlyCollection<SocialNetwork> socialNetworks = newUser.SocialNetworks;
            bool isTrue = socialNetworks.Count() == 1 && socialNetworks.Any(e => e.Url == updatedUrl
                && e.Id == socialNetworkAdded.Id);
            Assert.True(isTrue);
        }

        [Fact]
        public void SocialNetwork_Url_Should_Not_Be_Empty()
        {
            //Act
            var newUser = new User(_userId, _firstName, _lastName, _email, _description);
            //Arrange-Assert
            Assert.Throws<ArgumentNullException>(() => newUser
                 .AddSocialNetwork(contactNetwork: Network.Twitter, url: ""));
        }
    }
}
