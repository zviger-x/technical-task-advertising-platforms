using Microsoft.AspNetCore.Http;
using Serilog;

namespace Shared.Common
{
    internal class ErrorHandlingHelper
    {
        public int GetResponseStatusCode(Exception ex)
        {
            switch (ex)
            {
                case Exception:
                default:
                    return StatusCodes.Status500InternalServerError;
            }
        }

        public void LogError(Exception ex, int statusCode)
        {
            if (statusCode < StatusCodes.Status500InternalServerError)
            {
                Log.Error("An error occurred: {errorMessage}", ex.Message);
            }
            else
            {
                Log.Error(ex, "An unexpected error occurred");
            }
        }

        public object GetErrorResponse(Exception ex)
        {
            return GetErrorObject("unexpectedError", $"An unexpected error occurred. {ex.Message}");
        }

        private object GetErrorObject(string code, string message)
        {
            return new
            {
                errors = new
                {
                    code,
                    message,
                }
            };
        }
    }
}
