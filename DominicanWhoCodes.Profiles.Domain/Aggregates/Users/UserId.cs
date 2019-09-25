

using DominicanWhoCodes.Shared.Domain;
using System;

namespace DominicanWhoCodes.Profiles.Domain.Aggregates.Users
{
    public class UserId : Id<User>
    {
        private UserId() { }
        private UserId(Guid userId) : base(userId) { }

        public static UserId FromGuid(Guid userId) => new UserId(userId);
    }
}
