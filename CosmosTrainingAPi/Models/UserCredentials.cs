using Newtonsoft.Json;

namespace PracticeMVCApplication.Models
{
    public class UserCredentials
    {
        [JsonProperty("id")]
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
