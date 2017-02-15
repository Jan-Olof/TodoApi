namespace TodoApi.Dtos
{
    public class HttpError : IHttpError
    {
        public HttpError()
        {
            ExceptionMessage = string.Empty;
            ExceptionType = string.Empty;
            StackTrace = string.Empty;
        }

        public string ExceptionMessage { get; set; }
        public string ExceptionType { get; set; }
        public int HResult { get; set; }
        public string StackTrace { get; set; }
    }
}