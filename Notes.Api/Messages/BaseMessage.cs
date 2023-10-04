using MediatR;
using Notes.Api.Model;
namespace Notes.Api.Messages
{
    public record BaseInProcessMessage() : IRequest<NotesInProcessResponse>, IBaseRequest, INotification;
    public record NotesInProcessResponse()
    {
        public List<ErrorModel> Errors { get; set; }

        public List<ErrorModel> Warnings { get; set; }

        public List<ErrorModel> Infos { get; set; }
    }
}
