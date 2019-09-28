

using DominicanWhoCodes.Profiles.Domain.Aggregates.Users;

namespace DominicanWhoCodes.Profiles.API.Application.DTO
{
    public class PhotoDto
    {
        public ImageSource ImageSource { get; set; }
        public string Url { get; set; }
        public byte[] ContentFile { get; set; }
        public string FileName { get; set; }
    }
}
