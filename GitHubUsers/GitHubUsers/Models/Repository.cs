using System;
using Newtonsoft.Json;

namespace GitHubUsers.Models
{
    public class Repository
    {
        [JsonProperty("id")]
        public uint Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("html_url")]
        public string HtmlUrl { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("stargazers_count")] 
        public int Stars { get; set; }

        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }
}
