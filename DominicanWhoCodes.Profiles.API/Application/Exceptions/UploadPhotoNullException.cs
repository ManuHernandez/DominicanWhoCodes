using System;
using System.Runtime.Serialization;

namespace DominicanWhoCodes.Profiles.API.Application.Exceptions
{
    [Serializable]
    internal class UploadPhotoNullException : Exception
    {
        public UploadPhotoNullException()
        {
        }

        public UploadPhotoNullException(string message) : base(message)
        {
        }

        public UploadPhotoNullException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UploadPhotoNullException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}