
using Notes.Api.Model;

namespace Notes.Api.Messages
{
    public record GetNoteByIdRequest(int id) : BaseInProcessMessage;

    public record GetNoteByIdResponse(IEnumerable<NoteModel> Data) : NotesInProcessResponse;
     
    public record GetNoteListRequest(NoteTypes? NoteTypes) : BaseInProcessMessage;

    public record GetNoteListResponse(IEnumerable<NoteModel> Data) : NotesInProcessResponse;
}
