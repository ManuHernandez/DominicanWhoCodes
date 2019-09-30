

using System;

namespace DominicanWhoCodes.Identity.API.Models.Application.EventContracts
{
    public interface UserCreationFailed
    {
        Guid UserId { get; }
    }
}
