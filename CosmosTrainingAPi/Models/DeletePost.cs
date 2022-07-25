using Newtonsoft.Json;

namespace CosmosTrainingAPi.Models
{
    public class DeletePost
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        public string Username { get; set; }
    }
}
