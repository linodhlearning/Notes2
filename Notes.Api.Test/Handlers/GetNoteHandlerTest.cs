using Microsoft.Extensions.Logging;
using Moq;
using Notes.Api.Handlers;
using Notes.Api.Messages;
using Xunit.Abstractions;

namespace Notes.Api.Test.Handlers;
public class GetNoteHandlerTest : ApiTestBase
{
    public GetNoteHandlerTest(ITestOutputHelper output, OnetimeSetup setup) : base(output, setup)
    {

    }

    [Fact]
    public void AcknowledgeDispatchInstructionHandler_HandleMessage_ReturnsResult()
    {
        //Arange
        var mockILogger = new Mock<ILogger<GetNoteHandler>>();

        GetNoteByIdRequest req = new GetNoteByIdRequest(11);

        GetNoteHandler handler =  new GetNoteHandler(mockILogger.Object); 
       var result = handler.HandleMessage(req);

        //Asert
        Assert.NotNull(result);
    }
}
