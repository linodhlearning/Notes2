
using Notes.Api.Messages;
using Notes.Api.Model;

namespace Notes.Api.Handlers
{
    public class GetNoteListHandler : BaseMessageHandler<GetNoteListRequest, GetNoteListResponse>
    {
        private readonly ILogger<GetNoteListHandler> _logger;

        public GetNoteListHandler(ILogger<GetNoteListHandler> logger) : base(logger)
        {
            _logger = logger;
        }

        public override async Task<NotesInProcessResponse> ValidateMessage(GetNoteListRequest message)
        {

            return await Task.FromResult(new NotesInProcessResponse { Errors = new List<ErrorModel>() });
        }

        public override async Task<GetNoteListResponse> HandleMessage(GetNoteListRequest message)
        {
            _logger.LogInformation("GetNoteListHandler HandleMessage Begin");
            var data = new List<NoteModel> { new()
            {
                Id = 100, Title = "test1", Description = "test 123",NoteType= message.NoteTypes??NoteTypes.None
            }
            };
            return await Task.FromResult(new GetNoteListResponse(data));
        }
    }
}
