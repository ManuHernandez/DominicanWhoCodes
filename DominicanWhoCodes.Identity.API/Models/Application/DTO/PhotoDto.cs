

namespace DominicanWhoCodes.Identity.API.Models.Application.DTO
{
    public class PhotoDto
    {
        public ImageSource ImageSource { get; set; }
        public string Url { get; set; }
        public byte[] ContentFile { get; set; }
        public string FileName { get; set; }
    }
}
