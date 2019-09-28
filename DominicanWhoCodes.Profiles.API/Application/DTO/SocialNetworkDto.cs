

using DominicanWhoCodes.Profiles.Domain.Aggregates.Users;

namespace DominicanWhoCodes.Profiles.API.Application.DTO
{
    public class SocialNetworkDto
    {
        public Network Network { get; set; }
        public string Url { get; set; }
    }
}
