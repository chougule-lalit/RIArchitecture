using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RIArchitecture.Api.Errors
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
                400 => "Bad Request to RIArchitecture",
                401 => "Unauthorized Request to RIArchitecture",
                404 => "Resource not found in RIArchitecture API",
                500 => "Internal Server Error in RIArchitecture API",
                _ => null
            };
        }
    }
}
