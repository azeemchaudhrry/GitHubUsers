namespace GitHubUsers.Constants
{
    public static class EndPoints
    {
        public static string BaseUrl = "https://api.github.com";

        public static string User = "/users/{0}";
        public static string Repository = "/users/{0}/repos?page={1}&per_page={2}";
    }
}
