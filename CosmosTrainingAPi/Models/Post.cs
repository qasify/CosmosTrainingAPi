using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace CosmosTrainingAPi.Models
{
    public class Post
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        public string? Text { get; set; }
        [JsonProperty("username")]
        public string Username { get; set; }
    }
}
