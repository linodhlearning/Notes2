
using Microsoft.FeatureManagement;
using Notes.Api.Messages;
using Notes.Api.Model;

namespace Notes.Api.Handlers
{
    public class GetNoteListHandler : BaseMessageHandler<GetNoteListRequest, GetNoteListResponse>
    {
        private readonly ILogger<GetNoteListHandler> _logger;
        private readonly IConfiguration _configuration;
        private readonly IFeatureManager _featureManager;
        public GetNoteListHandler(ILogger<GetNoteListHandler> logger, IConfiguration configuration, IFeatureManager featureManager) : base(logger)
        {
            _logger = logger;
            _configuration = configuration;
            _featureManager = featureManager;
        }

        public override async Task<NotesInProcessResponse> ValidateMessage(GetNoteListRequest message)
        {
            return await Task.FromResult(new NotesInProcessResponse { Errors = new List<ErrorModel>() });
        }

        public override async Task<GetNoteListResponse> HandleMessage(GetNoteListRequest message)
        {
            _logger.LogInformation("GetNoteListHandler HandleMessage Begin");
            bool isAngryNoteEnabled = await _featureManager.IsEnabledAsync("AngryNote");
            var data = Enumerable.Range(1, 10).Select(x => new NoteModel()
            {
                Id = x,
                Title = "test1" + x,
                Description = _configuration["Note2:Setting:Details"],//azure app configuration
                NoteType = message.NoteTypes ?? NoteTypes.None,
                AdditionalNotes = isAngryNoteEnabled ? "Angry notetype is configured in feature " : ""
            });
            return await Task.FromResult(new GetNoteListResponse(data));
        }
    }
}
