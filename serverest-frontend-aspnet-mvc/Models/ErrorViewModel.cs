namespace serverest_frontend_aspnet_mvc.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }

    public class ResponseMessage
    {
        public string Message { get; set; }
    }
}
