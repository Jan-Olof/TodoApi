using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights;
using TodoApi.Exceptions;
using TodoApi.Factories;

namespace TodoApi.Middleware
{
    /// <summary>
    /// See http://stackoverflow.com/questions/38630076/asp-net-core-web-api-exception-handling
    /// </summary>
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task Invoke(HttpContext context /* other scoped dependencies */)
        {
            try
            {
                // must be awaited
                await _next(context);
            }
            catch (Exception ex)
            {
                var telemetry = new TelemetryClient();
                telemetry.TrackException(ex);

                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var statusCode = SetStatusCode(exception);

            return WriteExceptionAsync(context, exception, statusCode);
        }

        private static HttpStatusCode SetStatusCode(Exception exception)
        {
            // if it's not one of the expected exception, set it to 500
            var code = HttpStatusCode.InternalServerError;

            // Here you can set status code depending on exception thrown.
            if (exception is MyNotFoundException) code = HttpStatusCode.NotFound;
            else if (exception is MyUnauthorizedException) code = HttpStatusCode.Unauthorized;
            else if (exception is MyException) code = HttpStatusCode.BadRequest;

            return code;
        }

        private static Task WriteExceptionAsync(HttpContext context, Exception exception, HttpStatusCode statusCode)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = (int)statusCode;

            return response.WriteAsync(JsonConvert.SerializeObject(HttpErrorFactory.CreateHttpError(exception)));
        }
    }
}