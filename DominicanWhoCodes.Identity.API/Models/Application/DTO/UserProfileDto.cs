﻿
using System;

namespace DominicanWhoCodes.Identity.API.Models.Application.DTO
{
    public class UserProfileDto
    {
        public Guid Id { get; set; } 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public SocialNetworkDto[] SocialNetworks { get; set; }
        public PhotoDto Photo { get; set; }
    }

}
