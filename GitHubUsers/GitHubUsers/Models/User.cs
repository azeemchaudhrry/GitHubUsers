using Newtonsoft.Json;

namespace GitHubUsers.Models
{
    public class User
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("login")] 
        public string Login { get; set; }

        [JsonProperty("avatar_url")]
        public string AvatarUrl { get; set; }

        [JsonProperty("html_url")] 
        public string HtmlUrl { get; set; }

        [JsonProperty("type")] 
        public string Type { get; set; }

        [JsonProperty("location")] 
        public string Location { get; set; }

        [JsonProperty("repos_url")]
        public string ReposUrl { get; set; }

        [JsonProperty("public_repos")]
        public int PublicRepositoryCount { get; set; }

        public User()
        {
            PublicRepositoryCount = 0;
        }
    }
}
