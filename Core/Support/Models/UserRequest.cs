using Newtonsoft.Json;

namespace Yordan.GoRestSpecflow.Core.Support.Models
{
    public class UserRequest
    {
        [JsonIgnore]
        public string FirstName { get; set; }

        [JsonIgnore]
        public string LastName { get; set; }

        [JsonProperty("name")]
        public string Name => FirstName + LastName;

        [JsonProperty("email")]

        public string Email { get; set; }

        [JsonProperty("gender")]

        public string Gender { get; set; }

        [JsonProperty("status")]

        public string Status { get; set; }
    }
}
