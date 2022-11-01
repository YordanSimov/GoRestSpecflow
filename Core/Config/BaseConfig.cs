using Microsoft.Extensions.Configuration;

namespace Yordan.GoRestSpecflow.Core.Config
{
    public class BaseConfig
    {
        public BaseConfig()
        {
            var config = new ConfigurationBuilder().AddJsonFile("testConfig.json").Build();

            HttpClientConfig = config.GetSection("HttpClient").Get<HttpClientConfig>();
        }

        public HttpClientConfig HttpClientConfig { get; set; }
    }
}
