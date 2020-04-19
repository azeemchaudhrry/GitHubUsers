using System.Collections.Generic;
using System.Threading.Tasks;
using GitHubUsers.Services.Request;

namespace GitHubUsers.Services.Repository
{
    public class RepositoryService : IRepositoryService
    {
        private readonly IRequestService _requestService;

        public RepositoryService(IRequestService requestService)
        {
            _requestService = requestService;
        }

        public async Task<IReadOnlyList<Models.Repository>> GetUserRepositoriesAsync(string uri, int page = 0, int pageSize = 25)
        {
            var repositoriesUri = $"{uri}?page={page}&per_page={pageSize}";

            return await _requestService.GetAsync<IReadOnlyList<Models.Repository>>(repositoriesUri);
        }
    }
}
