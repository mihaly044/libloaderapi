using System;

namespace libloaderapi.Domain.Exceptions
{
    public class InvalidPayloadException : Exception
    {
        public InvalidPayloadException(string message) : base(message)
        {

        }

        public InvalidPayloadException() : base()
        {

        }
    }
}
