using Newtonsoft.Json;

namespace CosmosTrainingAPi.Models
{
    public class UpdatePassword
    {
        [JsonProperty("id")]
        public string Username { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
