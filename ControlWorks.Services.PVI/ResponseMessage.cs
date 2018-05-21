using System;

namespace ControlWorks.Services.PVI
{
    public class ResponseMessage
    {
        public Guid Id { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }

        public ErrorResponse[] Errors { get; set; }
    }

    public class ErrorResponse
    {
        public string Error { get; set; }
    }
}
