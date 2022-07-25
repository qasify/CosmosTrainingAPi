﻿using Newtonsoft.Json;

namespace CosmosTrainingAPi.Models
{ 
    public class UserCredentials
    {
        [JsonProperty("id")]
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
