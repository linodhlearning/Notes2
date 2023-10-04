using Microsoft.AspNetCore.Mvc;

namespace Notes.Api.Model
{
    public sealed class ErrorResult : ObjectResult
    {
        public ErrorResult(int errorCode, object value) : base(value)
        {
            this.StatusCode = errorCode;
        }
    }
}
