

using DominicanWhoCodes.Profiles.Domain.Aggregates.Users;
using DominicanWhoCodes.Profiles.Domain.Aggregates.Users.Events;
using System;
using System.IO;
using System.Linq;
using Xunit;

namespace DominicanWhoCodes.Profiles.UnitTests.Domain
{
    public partial class UserAggregateTest
    {

        [Fact]
        public void Upload_Profile_Photo_Url_Success()
        {
            //Act
            var newUser = new User(_userId, _firstName, _lastName, _email, _description);
            string photoUrl = "http://myimage.com/developer23.png";
            //Arrange
            object photoAdded = newUser.UploadPhoto(photoUrl);
            //Assert
            Assert.NotNull(photoAdded);
        }

        [Fact]
        public void Profile_Photo_Url_Should_Not_Be_Empty()
        {
            //Act
            var newUser = new User(_userId, _firstName, _lastName, _email, _description);
            string photoUrl = string.Empty;
            //Arrange-Assert
            Assert.Throws<ArgumentNullException>(() => newUser.UploadPhoto(photoUrl));
        }

        [Fact]
        public void Upload_Profile_Photo_From_FileSystem_Success()
        {
            //Act
            var newUser = new User(_userId, _firstName, _lastName, _email, _description);
            byte[] photoUpload = new byte[30 ^ 2];
            string fileName = "fake.jpg";
            //Arrange
            Photo photoAdded = newUser.UploadPhoto(fileName, photoUpload);
            //Assert
            Assert.NotNull(photoAdded);
        }

        [Fact]
        public void Upload_Photo_If_Another_Exists_Should_Be_Updated()
        {
            //Act
            var newUser = new User(_userId, _firstName, _lastName, _email, _description);
            byte[] photoUpload = new byte[30 ^ 2];
            string fileName = "fake.jpg";
            string photoUrl = "http://myimage.com/developer23.png";
            //Arrange
            Photo photoAdded = newUser.UploadPhoto(fileName, photoUpload);
            Photo photoUpdated = newUser.UploadPhoto(photoUrl);
            //Assert
            Photo currentPhoto = newUser.CurrentPhoto;
            bool isTrue = (photoAdded == photoUpdated) && (photoUpdated == currentPhoto);
            Assert.True(isTrue);
        }

        [Fact]
        public void Upload_Photo_From_FileSystem_Should_Fire_Upload_New_Photo_Domain_Event()
        {
            //Act
            var newUser = new User(_userId, _firstName, _lastName, _email, _description);
            byte[] photoUpload = new byte[30 ^ 2];
            string fileName = "fake.jpg";
            //Arrange
            Photo photoAdded = newUser.UploadPhoto(fileName, photoUpload);
            //Assert
            var domainEventExists = newUser.DomainEvents
                .Any(e => e.GetType() == typeof(UploadNewPhotoFromFileSystemDomainEvent));
            Assert.True(domainEventExists);
        }
    }
}
