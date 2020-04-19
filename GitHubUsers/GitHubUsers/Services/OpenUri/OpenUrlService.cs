using System;
using Xamarin.Essentials;

namespace GitHubUsers.Services.OpenUri
{
    public class OpenUrlService : IOpenUrlService
    {
        public void OpenUrl(Uri url)
        {
            Launcher.OpenAsync(url);
        }
    }
}
