using Notes.Api.Messages;
using Notes.Api.Model;

namespace Notes.Api.Handlers
{
    public class GetNoteHandler : BaseMessageHandler<GetNoteByIdRequest, GetNoteByIdResponse>
    {
        private readonly ILogger<GetNoteHandler> _logger;

        public GetNoteHandler(ILogger<GetNoteHandler> logger) : base(logger)
        {
            _logger = logger;
        }

        public override async Task<NotesInProcessResponse> ValidateMessage(GetNoteByIdRequest message)
        {

            return await Task.FromResult(new NotesInProcessResponse { Errors = new List<ErrorModel>() });
        }

        public override async Task<GetNoteByIdResponse> HandleMessage(GetNoteByIdRequest message)
        {
            _logger.LogInformation("GetNoteHandler HandleMessage Begin");
            var data = new List<NoteModel> {new(){Id = message.id, Title = "test1",Description = "test 123", NoteType = NoteTypes.None } };//todo
            return await Task.FromResult(new GetNoteByIdResponse(data));
        }
    }
}
