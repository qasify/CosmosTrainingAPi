using Newtonsoft.Json;

namespace CosmosTrainingAPi.Models
{
    public class User
    {
        [JsonProperty("id")]
        public string Username { get; set; }
        public string Password { get; set; }
        public string Gender { get; set; }
    }
}
