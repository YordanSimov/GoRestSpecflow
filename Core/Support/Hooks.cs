using Yordan.GoRestSpecflow.Core.Config;
using Yordan.GoRestSpecflow.Core.ContextContainers;

namespace Yordan.GoRestSpecflow.Core.Support
{
    [Binding]
    public sealed class Hooks
    {
        private TestContextContainer _testContext;
        private BaseConfig _baseConfig;

        public Hooks(TestContextContainer testContext, BaseConfig baseConfig)
        {
            _testContext = testContext;
            _baseConfig = baseConfig;
        } 

        [BeforeScenario]
        public void TearUp()
        {
            _testContext.HttpClient = new HttpClient();
        }

        [BeforeScenario("Authenticate")]
        public void Authenticate()
        {
            _testContext.HttpClient.DefaultRequestHeaders.Add("Authorization", _baseConfig.HttpClientConfig.Token);
        }
    }
}
