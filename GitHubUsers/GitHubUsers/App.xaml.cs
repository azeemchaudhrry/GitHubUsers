using System;
using System.Threading.Tasks;
using GitHubUsers.View;
using GitHubUsers.ViewModels;
using Xamarin.Forms;

[assembly: ExportFont("FuturaPT-Book.ttf", Alias = "FuturaPTBook")]
[assembly: ExportFont("FuturaPT-Medium.ttf", Alias = "FuturaPTMedium")]
[assembly: ExportFont("fasolid900.ttf", Alias = "FontAwesome")]

namespace GitHubUsers
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            _ = InitializeAsync();
        }

        private async Task InitializeAsync()
        {
            if (Activator.CreateInstance(typeof(MainView)) is Page page)
            {
                var viewModel = Utilities.ViewModelLocator.Instance.Resolve<MainViewModel>();
                page.BindingContext = viewModel;

                MainPage = page;

                await (page.BindingContext as ViewModelBase)?.InitializeAsync();
            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
