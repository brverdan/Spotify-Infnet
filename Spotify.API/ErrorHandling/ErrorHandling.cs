namespace Spotify.API.ErrorHandling;

public class ErrorHandling
{
    public List<ErrorMessage> Messages { get; set; } = new List<ErrorMessage>();
    public String ErrorDescription = "Errors occurred while processing your request";

    public class ErrorMessage
    {
        public string Message { get; set; }
        public string ErrorName { get; set; }
    }
}
