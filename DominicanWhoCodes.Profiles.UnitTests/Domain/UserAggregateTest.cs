
using DominicanWhoCodes.Profiles.Domain.Aggregates.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace DominicanWhoCodes.Profiles.UnitTests.Domain
{
    public class UserAggregateTest
    {
        private UserId _userId = UserId.FromGuid(Guid.NewGuid());
        private string _firstName = "Manuel";
        private string _lastName = "Hernández";
        private string _email = "hernandezmanuel@lorenipsum.com";
        private string _description = "Software Developer";

        [Fact]
        public void Create_User_Success()
        {
            //Act
            var newUser = new User(_userId, _firstName, _lastName, _email, _description);
            //Assert
            Assert.NotNull(newUser);
        }

        [Fact]
        public void User_Must_Be_Pending_In_Pending_Status_After_Successfull_Creation()
        {
            //Arrange
            var currentStatus = UserStatus.Pending;
            //Act
            var newUser = new User(_userId, _firstName, _lastName, _email, _description);
            //Assert
            Assert.True(newUser.CurrentStatus == currentStatus);
        }

        [Fact]
        public void User_Must_Have_Today_Creation_Date_After_Successfull_Creation()
        {
            //Arrange
            var currentDate = DateTime.UtcNow;
            //Act
            var newUser = new User(_userId, _firstName, _lastName, _email, _description);
            //Assert
            Assert.True(newUser.CreationDate.Date == currentDate.Date);
        }

        [Fact]
        public void User_Must_Have_a_FirstName()
        {
            //Arrange
            var firstName = string.Empty;
            //Act-Assert
            Assert.Throws<ArgumentNullException>(() => new User(_userId, firstName, _lastName, _email, _description));
        }

        [Fact]
        public void User_Must_Have_a_LastName()
        {
            //Arrange
            var lastName = string.Empty;
            //Act-Assert
            Assert.Throws<ArgumentNullException>(() => new User(_userId, _firstName, lastName, _email, _description));
        }

        [Fact]
        public void User_Must_Have_a_Email()
        {
            //Arrange
            var email = string.Empty;
            //Act-Assert
            Assert.Throws<ArgumentNullException>(() => new User(_userId, _firstName, _lastName, email, _description));
        }

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
    }
}
