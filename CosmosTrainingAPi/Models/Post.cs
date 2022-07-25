using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace PracticeMVCApplication.Models
{
    public class Post
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        public string? Text { get; set; }
        public string Username { get; set; }
    }
}
