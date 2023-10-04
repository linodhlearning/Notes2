using MediatR;
using Microsoft.AspNetCore.Mvc;
using Notes.Api.Messages;
using Notes.Api.Model;

namespace Notes.Api.Controllers
{
    public class BaseController<T> : ControllerBase where T : ControllerBase
    {
        protected ILogger<T> _logger;
        public BaseController(ILogger<T> logger)
        {
            _logger = logger;
        }

        [NonAction]
        public async Task<IActionResult> SendToMessageHandler(object request, CancellationToken cancellationToken = default(CancellationToken))
        {
            BaseController<T> ctrlBase = this;
            object obj = await this.HttpContext.RequestServices.GetService<IMediator>().Send(request, cancellationToken);
            if (obj != null && (object)(obj as NotesInProcessResponse) != null)
            {
                var inProcessResponse = obj as NotesInProcessResponse;
                return (inProcessResponse.Errors == null || !inProcessResponse.Errors.Any<ErrorModel>()) ?
                        (IActionResult)ctrlBase.Ok((object)inProcessResponse) : (IActionResult)ctrlBase.NotFound((object)inProcessResponse) ;
            } 
            return ctrlBase.Problem("Response is either not of type 'NotesInProcessResponse' or is null", ctrlBase.GetType().Name);
        }

    }
}
