namespace TodoApi.Dtos
{
    public interface IHttpError
    {
        string ExceptionMessage { get; set; }
        string ExceptionType { get; set; }
        int HResult { get; set; }
        string StackTrace { get; set; }
    }
}