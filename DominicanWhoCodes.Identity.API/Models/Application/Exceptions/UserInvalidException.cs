using System;
using System.Runtime.Serialization;

namespace DominicanWhoCodes.Identity.API.Models.Application.Exceptions
{
    [Serializable]
    internal class UserInvalidException : Exception
    {
        public UserInvalidException()
        {
        }

        public UserInvalidException(string message) : base(message)
        {
        }

        public UserInvalidException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UserInvalidException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}