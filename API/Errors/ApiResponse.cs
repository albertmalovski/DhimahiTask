using System;

namespace API.Errors
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);
        }

        public int StatusCode { get; set; }
        public string Message { get; set; }

        private string GetDefaultMessageForStatusCode(int statusCode)
        {
            return statusCode switch
            {

                400 => "You have made a Bad Request",
                401 => "You are not Authorized",
                403 => "You are Forbidden to perform this operation",
                404 => "The requested resource was Not Found",
                500 => " Server Error 500. Check that the URL is entered correctly then click on the refresh button on your browser to try to open the page again, if you can not open the page Please contact your administrator",
                _ => null
            };
        }
    }
}