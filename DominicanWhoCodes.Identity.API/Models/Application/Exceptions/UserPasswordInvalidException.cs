using System;
using System.Runtime.Serialization;

namespace DominicanWhoCodes.Identity.API.Models.Application.Exceptions
{
    [Serializable]
    internal class UserPasswordInvalidException : UserException
    {
        public UserPasswordInvalidException()
        {
        }

        public UserPasswordInvalidException(string message) : base(message)
        {
        }

        public UserPasswordInvalidException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UserPasswordInvalidException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}