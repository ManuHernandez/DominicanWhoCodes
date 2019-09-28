using DominicanWhoCodes.Shared.Domain;
using System.IO;

namespace DominicanWhoCodes.Profiles.Domain.Aggregates.Users
{
    public class Photo: Entity
    {
        private Photo() { }
        public Photo(PhotoId photoId, UserId userId, string url)
        {
            PhotoId = photoId;
            UserId = userId;
            Url = FieldChecker.NotEmpty(url);
            ImageSource = ImageSource.Url;
            FileName = Path.GetFileName(url);
        }

        public Photo(PhotoId photoId, UserId userId, string fileName, byte[] fileUpload)
        {
            PhotoId = photoId;
            UserId = userId;
            FileName = FieldChecker.NotEmpty(fileName);
            ImageSource = ImageSource.Upload;
        }

        public PhotoId PhotoId { get; private set; }
        public UserId UserId { get; private set; }
        public string Url { get; private set; }
        public ImageSource ImageSource { get; private set; }
        public string FileName { get; private set; }
        public virtual User User { get; private set; }
        internal void UpdatePhoto(string fileName, byte[] photoUpload)
        {
            this.FileName = FieldChecker.NotEmpty(fileName);
        }

        internal void UpdatePhoto(string photoUrl)
        {
            this.Url = FieldChecker.NotEmpty(photoUrl);
            this.FileName = Path.GetFileName(photoUrl);
        }
    }
}