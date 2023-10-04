using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Notes.Api.Test
{
    public static class JsonSettingsUtil
    {
        /// <summary>
        ///     Modify
        ///     <param name="serializerSettings"></param>
        ///     according to project default
        /// </summary>
        public static JsonSerializerSettings ApplyDefault(JsonSerializerSettings? serializerSettings = null)
        {
            serializerSettings ??= new JsonSerializerSettings();
            serializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            serializerSettings.Converters.Add(new StringEnumConverter());

            JsonConvert.DefaultSettings = () => serializerSettings;
            return serializerSettings;
        }
    }
    public class TestSettings
    {
        public const string ApiTestCollectionName = "Api Test Collection";
        public bool UseInMemoryTestServer { get; set; }
        public string? ApiBaseUrl { get; set; }
    }

    [Collection(TestSettings.ApiTestCollectionName)]
    public class ApiTestBase : IDisposable
    {
        private Stopwatch? _sw;

        private string? _testName;

        public ApiTestBase(ITestOutputHelper output, OnetimeSetup setup)
        {
            JsonSettingsUtil.ApplyDefault();
            Output = output;
            Setup = setup;
            SetTestContext(output);
        }

        public ITestOutputHelper Output { get; }
        public OnetimeSetup Setup { get; }

        public void Dispose()
        {
            if (_sw != null)
            {
                Output.WriteLine("========== {0} ==========> ({1} seconds)", _testName, _sw.Elapsed.TotalSeconds);
            }
        }

        private void SetTestContext(ITestOutputHelper output)
        {
            //https://github.com/xunit/xunit/issues/416#issuecomment-378512739
            Type type = output.GetType();
            FieldInfo? testMember = type.GetField("test", BindingFlags.Instance | BindingFlags.NonPublic);
            ITest test = (ITest)testMember?.GetValue(output)!;
            _testName = test.DisplayName;
            output.WriteLine("========== {0} ==========> START", _testName);
            _sw = Stopwatch.StartNew();
        }
    }
}
