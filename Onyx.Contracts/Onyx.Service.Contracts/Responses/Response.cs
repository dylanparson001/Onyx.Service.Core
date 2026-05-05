namespace Onyx.Service.Contracts.Responses
{
    public abstract class Response
    {
        public bool IsSuccess { get; set; }
        public string? ErrorMessage { get; set; }
        public Response(string? errorMessage = "")
        {
            // If theres no error message, request was successful
            IsSuccess = string.IsNullOrEmpty(errorMessage);
            ErrorMessage = errorMessage;
        }
    }
}