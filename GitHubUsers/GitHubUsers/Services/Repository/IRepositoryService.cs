using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GitHubUsers.Models;

namespace GitHubUsers.Services.Repository
{
    public interface IRepositoryService
    {
        Task<IReadOnlyList<Models.Repository>> GetUserRepositoriesAsync(string uri, int page = 0, int pageSize = 25);
    }
}
