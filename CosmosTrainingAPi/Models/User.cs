using Newtonsoft.Json;

namespace CosmosTrainingAPi.Models
{
    public class User
    {
        [JsonProperty("id")]
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Gender { get; set; }
    }
}
