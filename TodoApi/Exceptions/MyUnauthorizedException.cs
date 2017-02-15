using System;

namespace TodoApi.Exceptions
{
    public class MyUnauthorizedException : Exception
    {
        public MyUnauthorizedException()
        {
        }

        public MyUnauthorizedException(string message) : base(message)
        {
        }

        public MyUnauthorizedException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}