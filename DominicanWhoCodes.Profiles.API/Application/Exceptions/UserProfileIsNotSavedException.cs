using System;
using System.Runtime.Serialization;

namespace DominicanWhoCodes.Profiles.API.Application.Exceptions
{
    [Serializable]
    internal class UserProfileIsNotSavedException : Exception
    {
        public UserProfileIsNotSavedException()
        {
        }

        public UserProfileIsNotSavedException(string message) : base(message)
        {
        }

        public UserProfileIsNotSavedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UserProfileIsNotSavedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}