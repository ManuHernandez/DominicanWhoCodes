

using DominicanWhoCodes.Profiles.Domain.Aggregates.Users;
using DominicanWhoCodes.Shared.Domain;
using System;
using Xunit;

namespace DominicanWhoCodes.Profiles.UnitTests.Domain
{
    public class EntityTest
    {
        [Fact]
        public void Entities_With_Same_Id_Are_Equals()
        {
            //arrange
            var userId = UserId.FromGuid(Guid.NewGuid());
            //act
            var user = new User(userId, "Manuel", "Hernández", "hernandezmanuel@lorenipsum.com");
            var userUpdated = new User(userId, "Angel", "Garcia", "agarcia@lorenipsum.com");
            //assert
            bool isTrue = user.Equals(userUpdated) && user == userUpdated
                && user.GetHashCode() == userUpdated.GetHashCode();
            Assert.True(isTrue);
        }

        [Fact]
        public void Entities_With_Different_Id_Are_Not_Equals()
        {
            //arrange
            var userId = UserId.FromGuid(Guid.NewGuid());
            var updateUserId = UserId.FromGuid(Guid.NewGuid());
            //act
            var user = new User(userId, "Manuel", "Hernández", "hernandezmanuel@lorenipsum.com");
            var userUpdated = new User(updateUserId, "Manuel", "Hernández",
                "hernandezmanuel@lorenipsum.com");
            //assert
            bool isTrue = !user.Equals(userUpdated) && user != userUpdated
                && user.GetHashCode() != userUpdated.GetHashCode();
            Assert.True(isTrue);
        }

        [Fact]
        public void Ids_Cannot_Be_Empty()
        {
            Assert.Throws<ArgumentException>(() => UserId.FromGuid(Guid.Empty));
        }
    }
}
