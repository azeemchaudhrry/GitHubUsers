using Newtonsoft.Json;

namespace GitHubUsers.Models
{
    public class ErrorResponse
    {
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
