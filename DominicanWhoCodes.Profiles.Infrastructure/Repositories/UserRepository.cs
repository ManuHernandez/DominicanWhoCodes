

using System.Threading.Tasks;
using DominicanWhoCodes.Profiles.Domain.Aggregates.Users;
using DominicanWhoCodes.Shared.Domain;
using Microsoft.EntityFrameworkCore;

namespace DominicanWhoCodes.Profiles.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserProfileContext _context;
        public UserRepository(UserProfileContext context)
        {
            _context = context;
        }
        public IUnitOfWork UnitOfWork => _context;

        public User Add(User user)
        {
            return _context.Users.Add(user).Entity;
        }

        public async Task<User> GetAsync(UserId userId)
        {
            var user = await _context.Users
                .Include(e => e.SocialNetworks)
                .Include(e => e.CurrentPhoto)
                .FirstOrDefaultAsync(e => e.Id == userId.Value);

            if (user == null) return null;

            return user;
        }
    }
}
