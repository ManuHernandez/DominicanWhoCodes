﻿using System;
using System.Runtime.Serialization;

namespace DominicanWhoCodes.Identity.API.Models.Application.Exceptions
{
    public abstract class UserException : Exception
    {
        public UserException()
        {
        }
        public UserException(string message) : base(message)
        {
        }

        public UserException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UserException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
