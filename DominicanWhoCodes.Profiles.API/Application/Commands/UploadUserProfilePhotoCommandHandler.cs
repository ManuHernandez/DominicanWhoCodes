using System.Threading;
using System.Threading.Tasks;
using DominicanWhoCodes.Profiles.API.Application.Exceptions;
using DominicanWhoCodes.Profiles.Domain.Aggregates.Users;
using MediatR;

namespace DominicanWhoCodes.Profiles.API.Application.Commands
{
    public class UploadUserProfilePhotoCommandHandler : IRequestHandler<UploadUserProfilePhotoCommand, bool>
    {
        private readonly IUserRepository _userRepository;

        public UploadUserProfilePhotoCommandHandler(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }
        public async Task<bool> Handle(UploadUserProfilePhotoCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAsync(UserId.FromGuid(request.UserId));
            if (user == null)
                throw new UserNotFoundException("User not found");

            user.UploadPhoto(request.FileName, request.Photo);

            await _userRepository.UnitOfWork.CommitChanges();
            return true;
        }
    }
}
