using MediatR;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using Notes.Api.Messages;
using Notes.Api.Model;

namespace Notes.Api.Handlers
{
    public abstract class BaseMessageHandler<TMessage, TResponse> :
      INotificationHandler<TMessage>,
      IRequestHandler<TMessage, NotesInProcessResponse>
      where TMessage : IRequest<NotesInProcessResponse>, INotification
      where TResponse : NotesInProcessResponse
    {
        protected ILogger _logger;
        protected NotesInProcessResponse ValidationResult;

        public BaseMessageHandler(ILogger logger)
        {
            this._logger = logger;
        }

        Task<NotesInProcessResponse> IRequestHandler<TMessage, NotesInProcessResponse>.Handle(TMessage message, CancellationToken cancellationToken)
        {
            return this.HandleInternal(message, cancellationToken);
        }

        Task INotificationHandler<TMessage>.Handle(TMessage notification, CancellationToken cancellationToken)
        {
            return (Task)this.HandleInternal(notification, cancellationToken);
        }

        private async Task<NotesInProcessResponse> HandleInternal(
          TMessage message,
          CancellationToken cancellationToken)
        {
            try
            {
                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();
                try
                {
                    this.ValidationResult = await this.ValidateMessage(message);
                    return this.ValidationResult.Errors != null && this.ValidationResult.Errors.Any<ErrorModel>() ? this.ValidationResult : (NotesInProcessResponse)await this.HandleMessage(message);
                }
                finally
                {
                    stopWatch.Stop(); 
                    var interpolatedStringHandler = new StringBuilder( );
                    interpolatedStringHandler.Append(nameof(message));
                    interpolatedStringHandler.Append(": action executed in ");
                    interpolatedStringHandler.Append(stopWatch.ElapsedMilliseconds);
                    interpolatedStringHandler.Append(" milliseconds");
                    string stringAndClear = interpolatedStringHandler.ToString();
                    object[] objArray = Array.Empty<object>();
                    this._logger.LogInformation(stringAndClear, objArray);
                }
            }
            catch (Exception ex)
            {
                Exception exception = (Exception)(ex as AggregateException).Flatten() ?? ex;
                var interpolatedStringHandler = new StringBuilder(56, 3);
                interpolatedStringHandler.Append("Request(message) Type: ");
                interpolatedStringHandler.Append(typeof(TMessage).Name);
                interpolatedStringHandler.Append(", Response Type: ");
                interpolatedStringHandler.Append(typeof(TResponse).Name);
                interpolatedStringHandler.Append(", MessageObject:");
                interpolatedStringHandler.Append(message);
                string stringAndClear = interpolatedStringHandler.ToString();
                this._logger.LogError(exception, stringAndClear);
                List<ErrorModel> aemoErrorModelList = new List<ErrorModel>()
        {
          new ErrorModel()
          {
            Source = "BaseHandler",
            Title = "Internal exception occured. Refer to logs for details.",
            Detail = stringAndClear
          }
        };
                return new NotesInProcessResponse()
                {
                    Errors = aemoErrorModelList
                };
            }
        }

        public abstract Task<NotesInProcessResponse> ValidateMessage(TMessage message);

        public abstract Task<TResponse> HandleMessage(TMessage message);
    }
}
