using System.Net;

namespace FirstCleanArchitecture.Application.Commons.Results;

public class GeneralResult<T>
{
    public HttpStatusCode StatusCode;

    public string Message;

    public T? ReturnObject;
    
    public GeneralResult()
    {
        StatusCode = HttpStatusCode.OK;
        Message = string.Empty;
    }

    public GeneralResult(T returnObject)
    {
        StatusCode = HttpStatusCode.OK;
        Message = string.Empty;
        ReturnObject = returnObject;
    }

    public GeneralResult(HttpStatusCode statusCode, string message, T returnObject)
    {
        StatusCode = statusCode;
        Message = message;
        ReturnObject = returnObject;
    }
    
    public GeneralResult(HttpStatusCode statusCode, T returnObject)
    {
        StatusCode = statusCode;
        Message = string.Empty;
        ReturnObject = returnObject;
    }
    
    public GeneralResult(HttpStatusCode statusCode)
    {
        StatusCode = statusCode;
        Message = string.Empty;
    }
    
    public static GeneralResult<T> GeneralResultSuccessful = new GeneralResult<T>(); 
    public static GeneralResult<T> GeneralResultNotFound = new GeneralResult<T>(HttpStatusCode.NotFound); 
    public static GeneralResult<T> GeneralResultBadRequest = new GeneralResult<T>(HttpStatusCode.BadRequest); 
}