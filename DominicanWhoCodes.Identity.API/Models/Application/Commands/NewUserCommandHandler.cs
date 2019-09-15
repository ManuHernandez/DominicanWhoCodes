
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using DominicanWhoCodes.Identity.API.Models.Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace DominicanWhoCodes.Identity.API.Models.Application.Commands
{
    public class NewUserCommandHandler : IRequestHandler<NewUserCommand, bool>
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public NewUserCommandHandler(UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<bool> Handle(NewUserCommand request, CancellationToken cancellationToken)
        {
            var userEntity = TransformToUserEntity(request);

            ValidateUserData(userEntity);

            var result = await _userManager.CreateAsync(userEntity, request.NewUser.Password);

            if (result.Succeeded)
            {
                var roleName = await AddUserToDefaultRole();
                await AddClaims(userEntity, roleName);
            }
            else
                throw new UserPasswordInvalidException(result.Errors?
                    .FirstOrDefault()?.Description);

            return true;
        }

        private User TransformToUserEntity(NewUserCommand request)
        {
            return new User()
            {
                FirstName = request.NewUser.FirstName,
                LastName = request.NewUser.LastName,
                Email = request.NewUser.Email,
                UserName = request.NewUser.Email
            };
        }

        private void ValidateUserData(User user)
        {
            var validationResult = user.Validate();

            if (!validationResult.IsValid)
                throw new UserInvalidException(validationResult.Errors[0]?.ErrorMessage);
        }
        
        private async Task<string> AddUserToDefaultRole()
        {
            string defaultRole = "Basic";

            bool existsRole = await _roleManager.FindByNameAsync(defaultRole) != null;

            if (!existsRole) await _roleManager.CreateAsync(new IdentityRole(defaultRole));

            return defaultRole;
        }

        private async Task AddClaims(User userEntity, string roleName)
        {
            await _userManager.AddClaimAsync(userEntity, new Claim("userName", 
                userEntity.UserName));
            await _userManager.AddClaimAsync(userEntity, new Claim("firstName", 
                userEntity.FirstName));
            await _userManager.AddClaimAsync(userEntity, new Claim("lastName", 
                userEntity.LastName));
            await _userManager.AddClaimAsync(userEntity, new Claim("email", 
                userEntity.Email));
            await _userManager.AddClaimAsync(userEntity, new Claim("role", 
                roleName));
        }

    }
}
