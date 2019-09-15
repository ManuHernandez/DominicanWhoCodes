using System;
using DominicanWhoCodes.Identity.API.Models.Validators;
using DominicanWhoCodes.Shared.Users;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;

namespace DominicanWhoCodes.Identity.API.Models
{
    public class User : IdentityUser, IUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Guid UserId { get { return Guid.Parse(base.Id); } }

        public ValidationResult Validate() => new NewUserValidator().Validate(this);
    }
}
