using System.Threading.Tasks;

namespace GitHubUsers.Services.Request
{
    public interface IRequestService
    {
        Task<TResult> GetAsync<TResult>(string uri);
    }
}