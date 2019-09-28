

using DominicanWhoCodes.Identity.API.Models.Application.DTO;

namespace DominicanWhoCodes.Identity.API.Models.Application.InputModels
{
    public class NewUserInputModel
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public SocialNetworkDto[] SocialNetworks { get; set; }
        public string ImageUrl { get; set; }
        public UserProfileDto ConvertToUserProfile()
        {
            return new UserProfileDto()
            {
                FirstName = this.FirstName,
                LastName = this.LastName,
                Email = this.Email,
                Description = this.Description,
                Photo = ConvertInputPhotoToPhotoDto(),
                SocialNetworks = this.SocialNetworks
            };
        }

        PhotoDto ConvertInputPhotoToPhotoDto()
        {
            return new PhotoDto()
            {
                ImageSource = ImageSource.Url,
                Url = this.ImageUrl
            };
        }
    }
}
