

using DominicanWhoCodes.Shared.Domain;
using System.Threading.Tasks;

namespace DominicanWhoCodes.Profiles.Domain.Aggregates.Users
{
    public interface IUserRepository : IRepository<User>
    {
        User Add(User user);
        Task<User> GetAsync(UserId userId);
    }
}
