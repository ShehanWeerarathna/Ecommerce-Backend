namespace Ecommerce_Backend.Models.OutputModels;

public class ErrorResultJson
{
    public string ErrorCode { get; set; }

    public string ExceptionName { get; set; }

    public string Message { get; set; }
}