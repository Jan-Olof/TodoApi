using System;
using TodoApi.Dtos;

namespace TodoApi.Factories
{
    public static class HttpErrorFactory
    {
        public static IHttpError CreateHttpError(Exception exception)
        {
            return new HttpError
            {
                ExceptionMessage = exception.Message,
                ExceptionType = exception.GetType().FullName,
                StackTrace = exception.StackTrace,
                HResult = exception.HResult
            };
        }
    }
}