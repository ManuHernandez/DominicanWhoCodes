

using System;

namespace DominicanWhoCodes.Shared.Users
{
    public interface IUser
    {
        Guid UserId { get; }
        string FirstName { get; set; }
        string LastName { get; set;}
    }
}
