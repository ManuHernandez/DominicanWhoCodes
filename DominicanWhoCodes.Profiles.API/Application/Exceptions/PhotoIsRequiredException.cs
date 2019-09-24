using System;
using System.Runtime.Serialization;

namespace DominicanWhoCodes.Profiles.API.Application.Exceptions
{
    [Serializable]
    internal class PhotoIsRequiredException : Exception
    {
        public PhotoIsRequiredException()
        {
        }

        public PhotoIsRequiredException(string message) : base(message)
        {
        }

        public PhotoIsRequiredException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected PhotoIsRequiredException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}