namespace ControlWorks.Services.PVI
{
    public class ResponseMessage
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }

        public ErrorResponse[] Errors { get; set; }
    }

    public class ErrorResponse
    {
        public string Error { get; set; }
    }
}
