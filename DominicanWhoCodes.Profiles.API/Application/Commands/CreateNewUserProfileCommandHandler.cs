
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DominicanWhoCodes.Profiles.API.Application.Exceptions;
using DominicanWhoCodes.Profiles.Domain.Aggregates.Users;
using DominicanWhoCodes.Shared.Application.DTO;
using DominicanWhoCodes.Shared.Domain.Users;
using MediatR;

namespace DominicanWhoCodes.Profiles.API.Application.Commands
{
    public class CreateNewUserProfileCommandHandler : IRequestHandler<CreateNewUserProfileCommand, bool>
    {
        private readonly IUserRepository _userRepository;
        private User _userAggregate;
        public CreateNewUserProfileCommandHandler(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }
        public async Task<bool> Handle(CreateNewUserProfileCommand request, CancellationToken cancellationToken)
        {
            var newUserDto = request.UserProfile ?? throw new ArgumentNullException(nameof(request.UserProfile));
            var userId = UserId.FromGuid(newUserDto.Id);

            _userAggregate = new User(userId, newUserDto.FirstName, newUserDto.LastName, 
                newUserDto.Email, newUserDto.Description);

            AddSocialNetworks(newUserDto);
            AddUserPhoto(newUserDto);

            _userRepository.Add(_userAggregate);
            await _userRepository.UnitOfWork.CommitChanges(cancellationToken);

            return true;
        }

        private void AddSocialNetworks(UserProfileDto newUserDto)
        {
            if (newUserDto.SocialNetworks != null && newUserDto.SocialNetworks.Any())
                foreach (var socialNetwork in newUserDto.SocialNetworks)
                    _userAggregate.AddSocialNetwork(socialNetwork.Network, socialNetwork.Url);
        }

        private void AddUserPhoto(UserProfileDto newUserDto)
        {
            if (newUserDto.Photo == null)
                throw new PhotoIsRequiredException("The profile photo is required.");

            if (newUserDto.Photo.ImageSource == ImageSource.Upload)
                _userAggregate.UploadPhoto(newUserDto.Photo.FileName, newUserDto.Photo.ContentFile);
            else
                _userAggregate.UploadPhoto(newUserDto.Photo.Url);
        }
    }
}
