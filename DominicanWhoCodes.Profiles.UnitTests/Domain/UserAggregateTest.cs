
using DominicanWhoCodes.Profiles.Domain.Aggregates.Users;
using System;
using Xunit;

namespace DominicanWhoCodes.Profiles.UnitTests.Domain
{
    public class UserAggregateTest
    {
        private UserId _userId = UserId.FromGuid(Guid.NewGuid());
        private string _firstName = "Manuel";
        private string _lastName = "Hernández";
        private string _email = "hernandezmanuel@lorenipsum.com";
  
        [Fact]
        public void Create_User_Success()
        {
            //Act
            var newUser = new User(_userId, _firstName, _lastName, _email);
            //Assert
            Assert.NotNull(newUser);
        }


        [Fact]
        public void User_Must_Be_Pending_In_Pending_Status_After_Successfull_Creation()
        {
            //Arrange
            var currentStatus = UserStatus.Pending;
            //Act
            var newUser = new User(_userId, _firstName, _lastName, _email);
            //Assert
            Assert.True(newUser.CurrentStatus == currentStatus);
        }

        [Fact]
        public void User_Must_Have_Today_Creation_Date_After_Successfull_Creation()
        {
            //Arrange
            var currentDate = DateTime.UtcNow;
            //Act
            var newUser = new User(_userId, _firstName, _lastName, _email);
            //Assert
            Assert.True(newUser.CreationDate.Date == currentDate.Date);
        }

        [Fact]
        public void User_Must_Have_a_FirstName()
        {
            //Arrange
            var firstName = string.Empty;
            //Act-Assert
            Assert.Throws<ArgumentNullException>(() => new User(_userId, firstName, _lastName, _email));
        }

        [Fact]
        public void User_Must_Have_a_LastName()
        {
            //Arrange
            var lastName = string.Empty;
            //Act-Assert
            Assert.Throws<ArgumentNullException>(() => new User(_userId, _firstName, lastName, _email));
        }

        [Fact]
        public void User_Must_Have_a_Email()
        {
            //Arrange
            var email = string.Empty;
            //Act-Assert
            Assert.Throws<ArgumentNullException>(() => new User(_userId, _firstName, _lastName, email));
        }
    }
}
