using System.Threading.Tasks;
using GitHubUsers.Services.Request;

namespace GitHubUsers.Services.User
{
    public class UserService : IUserService
    {
        private readonly IRequestService _requestService;
        public UserService(IRequestService requestService)
        {
            _requestService = requestService;
        }

        public async Task<Models.User> GetUserByNameAsync(string name)
        {
            var uri = $"{AppSettings.ApiBaseUrl}/users/{name}";

            return await _requestService.GetAsync<Models.User>(uri);
        }
    }
}
