namespace MassRoverAPI.QuickStartSample.Errors
{
    public abstract class ErrorMessage
    {
        public ErrorCode Code { get;set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public string Detail { get; set; }
        public string Instance { get; set; }
        public string Info { get; set; }
    }

    public class RequestContentErrorMessage : ErrorMessage
    {
        public RequestContentErrorMessage()
        {
            Code = ErrorCode.RequestContentMismatch;
            Type = $"https://massrover.com/doc/errors/#{ErrorCode.RequestContentMismatch.ToString()}";
        }
    }

    public class EntityNotFoundErrorMessage : ErrorMessage
    {
        public EntityNotFoundErrorMessage()
        {
            Code = ErrorCode.RequestContentMismatch;
            Type = $"https://massrover.com/doc/errors/#{ErrorCode.EntityNotFound.ToString()}";
        }
    }
}
