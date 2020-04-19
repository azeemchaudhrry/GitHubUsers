using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PropertyChanged;
using Xamarin.Forms;

namespace GitHubUsers.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class ViewModelBase : BindableObject
    {
        public virtual Task InitializeAsync()
        {
            return Task.FromResult(false);
        }
    }
}
