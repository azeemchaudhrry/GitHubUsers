using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Input;
using GitHubUsers.Contracts;
using GitHubUsers.Exceptions;
using GitHubUsers.Models;
using GitHubUsers.Services.OpenUri;
using GitHubUsers.Services.Repository;
using GitHubUsers.Services.User;
using GitHubUsers.View;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace GitHubUsers.ViewModels
{
    public class MainViewModel : ViewModelBase,
        ISupportIncrementalLoading
    {
        #region Properties
        private readonly IUserService _userService;
        private readonly IRepositoryService _repositoryService;
        private readonly IOpenUrlService _openUrlService;
        public int PageSize { get; set; } = 20;
        public int PageIndex { get; set; }
        public bool HasMoreItems { get; set; }
        public bool IsLoadingIncrementally { get; set; }
        public ICommand LoadMoreItemsCommand { get; set; }
        public bool IsRefreshing { get; set; }
        public bool UserNotFound { get; set; }
        public string ErrorMessage { get; set; }
        public string TotalCountString
        {
            get
            {
                if (UserDetails != null)
                {
                    return $"{UserDetails.PublicRepositoryCount} {GetRepositoryString(UserDetails.PublicRepositoryCount)}";
                }
                return string.Empty;
            }
        }
        private string GetRepositoryString(int userDetailsPublicRepositoryCount)
        {
            return userDetailsPublicRepositoryCount <= 1 ? "Repository" : "Repositories";
        }

        public string UserName { get; set; }
        public User UserDetails { get; set; }
        public ObservableCollection<Repository> Repositories { get; set; }

        #endregion

        #region Commands

        public ICommand PerformSearch => new Command<string>(async (string query) =>
        {
            if (!string.IsNullOrEmpty(query))
            {
                UserNotFound = false;
                UserName = query;
                PageIndex = 1;
                Repositories?.Clear();
                UserDetails = null;
                await LoadPageData();
            }
        });

        public ICommand OpenUrlCommand => new Command<string>(async (string query) =>
        {
            if (!string.IsNullOrEmpty(query) && Uri.TryCreate(query, UriKind.RelativeOrAbsolute, out Uri result))
            {
                _openUrlService.OpenUrl(result);
            }
        });

        public Command<object> ItemSelectedCommand => new Command<object>(async (item) =>
        {
            if (item is Repository repository && Uri.TryCreate(repository.HtmlUrl, UriKind.RelativeOrAbsolute, out Uri result))
            {
                _openUrlService.OpenUrl(result);
            }
        });

        public Command RefreshCommand => new Command(async () =>
        {
            if (IsLoadingIncrementally)
            {
                IsRefreshing = false;
                return;
            }
            //reload the data
            PageIndex = 1;
            IsRefreshing = true;
            await LoadPageData();
            IsRefreshing = false;
        });
        #endregion

        #region Constructor

        public MainViewModel(
            IUserService userService,
            IRepositoryService repositoryService,
            IOpenUrlService openUrlService)
        {
            _userService = userService;
            _repositoryService = repositoryService;
            _openUrlService = openUrlService;
            UserName = "azeemchaudhrry";
            PageIndex = 1;
            LoadMoreItemsCommand = new Command(async () => await LoadPageData());
        }
        #endregion

        public override Task InitializeAsync()
        {
            Task.Factory.StartNew(LoadPageData);
            return Task.FromResult(false);
        }

        private async Task LoadPageData()
        {
            Exception exception = null;
            try
            {
                IsLoadingIncrementally = true;

                //Load User Details
                var userDetails = await _userService.GetUserByNameAsync(UserName);
                if (string.IsNullOrEmpty(userDetails.Name))
                {
                    userDetails.Name = userDetails.Login;
                }
                UserDetails = userDetails;

                //Load User Repositories
                var repositories =
                    await _repositoryService.GetUserRepositoriesAsync(UserDetails.ReposUrl, PageIndex, PageSize);
                if (repositories != null)
                {
                    if (PageIndex == 1)
                    {
                        Repositories = new ObservableCollection<Repository>(repositories);
                    }
                    else
                    {
                        foreach (var item in repositories)
                        {
                            if (Repositories != null && Repositories.Any(x => x.Id != item.Id))
                            {
                                Repositories.Add(item);
                            }
                        }
                    }
                }

                HasMoreItems = UserDetails.PublicRepositoryCount != Repositories?.Count;
            }
            catch (UserNotFoundException userNotFoundException)
            {
                UserNotFound = true;
                ErrorMessage = $"'{UserName}' {userNotFoundException.Message}";
            }
            catch (Exception exp)
            {
                exception = exp;
            }
            finally
            {
                IsLoadingIncrementally = false;
                Console.WriteLine(exception);
            }
        }
    }
}
