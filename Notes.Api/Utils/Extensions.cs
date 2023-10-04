using Microsoft.AspNetCore.Mvc;
using Notes.Api.Model;

namespace Notes.Api.Utils
{
    public static class Extensions
    {
        public static ObjectResult HandleException(this Exception e, string? errorMessage = null)
        {
            var message = string.IsNullOrWhiteSpace(errorMessage) ? e.Message : errorMessage;

            return new ErrorResult(
                StatusCodes.Status500InternalServerError,
                new
                {
                    Status = StatusCodes.Status500InternalServerError,
                    message,
                    Data = new { UserData = e.Data, e.Message, e.StackTrace }
                });
        }
    }
}
