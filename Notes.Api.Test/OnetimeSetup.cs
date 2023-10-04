using Flurl.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace Notes.Api.Test;

[CollectionDefinition(TestSettings.ApiTestCollectionName)]
public sealed class OnetimeSetup : ICollectionFixture<OnetimeSetup>, IDisposable
{
    private readonly List<IDisposable> _autoDisposes = new();

    public OnetimeSetup()
    {
        TestSettings = LoadSettings();
        if (TestSettings.UseInMemoryTestServer)
        {
             TestServer = CreateInMemoryTestServer();
        }
    }

    public TestSettings TestSettings { get; }
   public WebApplicationFactory<Program>? TestServer { get; }

    public void Dispose()
    {
        foreach (IDisposable x in _autoDisposes)
        {
            x.Dispose();
        }
    }

    public static TestSettings LoadSettings()
    {
        ConfigurationBuilder config = new ConfigurationBuilder();
        config.AddJsonFile("appsettings.json");
        config.AddUserSecrets<OnetimeSetup>(true);

        TestSettings settings = new TestSettings();
        config.Build().Bind(settings);
        return settings;
    }

    public IFlurlClient CreateApiTestClient()
    {
        if (TestSettings.UseInMemoryTestServer)
        {
            return new FlurlClient(TestServer?.CreateClient());
        }

        FlurlClient client = new FlurlClient();
        client.BaseUrl = TestSettings.ApiBaseUrl;
        return client;
    }

    // https://docs.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-6.0
    private WebApplicationFactory<Program> CreateInMemoryTestServer()
    {
        WebApplicationFactory<Program> testServer = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
        {
            // ReSharper disable once UnusedParameter.Local
            builder.ConfigureTestServices(services =>
            {
                // services.UseInMemoryDb()
                // services.AddMocks() for unit tests...
            });
        });
        _autoDisposes.Add(testServer);
        return testServer;
    }
}
