using System;
using DominicanWhoCodes.Shared.Users;
using Microsoft.AspNetCore.Identity;

namespace DominicanWhoCodes.Identity.API.Models
{
    public class User : IdentityUser, IUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Guid UserId { get { return Guid.Parse(base.Id); } }
    }
}
