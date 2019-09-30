
namespace DominicanWhoCodes.Identity.API.Models.Application.EventContracts
{
    using Identity.API.Models.Application.DTO;
    public interface UserSubmitted
    {
        UserProfileDto User { get; }
    }
}
