using System.Windows.Input;

namespace GitHubUsers.Contracts
{
    public interface ISupportIncrementalLoading
    {
        int PageSize { get; set; }
        int PageIndex { get; set; }
        bool HasMoreItems { get; set; }
        bool IsLoadingIncrementally { get; set; }
        ICommand LoadMoreItemsCommand { get; set; }
    }
}
