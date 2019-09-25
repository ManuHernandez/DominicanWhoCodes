using DominicanWhoCodes.Shared.Domain;
using System;

namespace DominicanWhoCodes.Profiles.Domain.Aggregates.Users
{
    public class PhotoId : Id<Photo>
    {
        private PhotoId() { }
        private PhotoId(Guid value) : base(value) { }
        public static PhotoId FromGuid(Guid photoId) => new PhotoId(photoId);
    }
}