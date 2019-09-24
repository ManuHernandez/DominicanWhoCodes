using DominicanWhoCodes.Shared.Domain.Users;
using System;

namespace DominicanWhoCodes.Shared.Application.DTO
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
        public class SocialNetworkDto
        {
            public Network Network { get; set; }
            public string Url { get; set; }
        }
        public class PhotoDto
        {
            public ImageSource ImageSource { get; set; }
            public string Url { get; set; }
            public byte[] ContentFile { get; set; }
            public string FileName { get; set; }
        }
    }
}
