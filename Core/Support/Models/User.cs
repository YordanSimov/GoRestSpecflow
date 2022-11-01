using Newtonsoft.Json;

namespace Yordan.GoRestSpecflow.Core.Support.Models
{
    public class User
    {
        [JsonProperty]

        public int Id { get; set; }

        [JsonProperty]
        public string Name { get; set; }

        [JsonProperty]

        public string Email { get; set; }

        [JsonProperty]

        public string Gender { get; set; }

        [JsonProperty]

        public string Status { get; set; }
    }
}
