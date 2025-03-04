﻿
namespace DominicanWhoCodes.Profiles.Domain.Aggregates.Users
{
    using DominicanWhoCodes.Profiles.Domain.Aggregates.Users.Events;
    using DominicanWhoCodes.Shared.Domain;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class User : Entity, IAggregateRoot
    {
        private HashSet<SocialNetwork> _socialNetworks;
        private User()
        {
            _socialNetworks = new HashSet<SocialNetwork>();
        }
        public User(UserId userId, string firstName, string lastName, string email,
            string description)
        {
            Id = userId.Value;
            FirstName = FieldChecker.NotEmpty(firstName, nameof(firstName));
            LastName = FieldChecker.NotEmpty(lastName, nameof(lastName));
            Email = FieldChecker.NotEmpty(email, nameof(email));
            CurrentStatus = UserStatus.Pending;
            CreationDate = DateTime.UtcNow;
            UserId = userId;
            Description = description;
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public DateTime CreationDate { get; private set; }
        public UserStatus CurrentStatus { get; private set; }
        public IReadOnlyCollection<SocialNetwork> SocialNetworks => _socialNetworks;

        public UserId UserId { get; private set; }
        public string Description { get; private set; }
        public virtual Photo CurrentPhoto { get; private set; }
        public SocialNetwork AddSocialNetwork(Network contactNetwork, string url)
        {
            var socialNetworkId = SocialNetworkId.FromGuid(Guid.NewGuid());
            var userId = UserId.FromGuid(Id);
            var newSocialNetwork = new SocialNetwork(socialNetworkId, userId, url, contactNetwork);

            if (_socialNetworks == null) _socialNetworks = new HashSet<SocialNetwork>();
            var socialNetworkFound = _socialNetworks.FirstOrDefault(e => e.Network == contactNetwork);

            if (socialNetworkFound != null) newSocialNetwork = socialNetworkFound.UpdateUrl(url);
            else _socialNetworks.Add(newSocialNetwork);

            return newSocialNetwork;
        }

        public Photo UploadPhoto(string fileName, byte[] photoUpload)
        {
            var userId = UserId.FromGuid(Id);
            var photoId = Users.PhotoId.FromGuid(Guid.NewGuid());

            if (CurrentPhoto == null) CurrentPhoto = new Photo(photoId, userId, fileName, photoUpload);
            else CurrentPhoto.UpdatePhoto(fileName, photoUpload);

            AddDomainEvent(new UploadNewPhotoFromFileSystemDomainEvent(userId, fileName, photoUpload));

            return CurrentPhoto;
        }

        public Photo UploadPhoto(string photoUrl)
        {
            var userId = UserId.FromGuid(Id);
            var photoId = Users.PhotoId.FromGuid(Guid.NewGuid());

            if (CurrentPhoto == null) CurrentPhoto = new Photo(photoId, userId, photoUrl);
            else CurrentPhoto.UpdatePhoto(photoUrl);

            return CurrentPhoto;
        }
    }
}
