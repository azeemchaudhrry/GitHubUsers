using System.Threading.Tasks;

namespace GitHubUsers.Services.User
{
    public interface IUserService
    {
        Task<Models.User> GetUserByNameAsync(string name);
    }
}
