using Newtonsoft.Json;

namespace PracticeMVCApplication.Models
{
    public class User {
        [JsonProperty("id")]
        public string Username { get; set; }
        public string Password { get; set; }
        [JsonProperty("gender")]
        public string Gender { get; set; }
    }
}
