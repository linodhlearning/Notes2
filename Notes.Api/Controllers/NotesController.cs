using Microsoft.AspNetCore.Mvc;
using Notes.Api.Messages;
using Notes.Api.Model;
using Notes.Api.Utils;

namespace Notes.Api.Controllers
{
    [Route("notes")]
    [ApiController]
    public class NotesController : BaseController<NotesController>
    { 
        public NotesController(ILogger<NotesController> logger) : base(logger)
        {
        }

        [HttpGet]
        [Route("notes/{noteId:int}")]
        public async Task<IActionResult> GetNote(int noteId)
        {
            try
            {
                return await SendToMessageHandler(new GetNoteByIdRequest(noteId));
            }
            catch (Exception ex)
            {
                string error = $"Error occurred while retrieving note for note id:{noteId}";
                _logger.LogError(ex, error);
                return ex.HandleException(error);
            }
        }

        [HttpGet]
        [Route("notes")]
        public async Task<IActionResult> GetNotes()
        {
            try
            {
                //
                var noteType = NoteTypes.None;
                return await SendToMessageHandler(new GetNoteListRequest(noteType));
            }
            catch (Exception ex)
            {
                string error = $"Error occurred while retrieving notes";
                _logger.LogError(ex, error);
                return ex.HandleException(error);
            }
        }
    }
}
