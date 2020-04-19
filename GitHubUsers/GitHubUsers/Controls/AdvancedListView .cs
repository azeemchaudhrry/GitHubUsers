using System;
using System.Collections;
using System.Windows.Input;
using GitHubUsers.Contracts;
using Xamarin.Forms;

namespace GitHubUsers.Controls
{
    public class AdvancedListView : ListView
    {
		private int _lastPosition;
        private IList _itemsSource;
		private ISupportIncrementalLoading _incrementalLoading;

        public new Action<Point> Scrolled { get; set; }

        public static BindableProperty ItemSelectedCommandProperty = BindableProperty.Create(
			nameof(ItemSelectedCommand),
			typeof(ICommand),
			typeof(AdvancedListView),
			null);

        public static readonly BindableProperty PreLoadCountProperty =
            BindableProperty.Create(nameof(PreLoadCount), typeof(int), typeof(AdvancedListView), 0);
        public int PreLoadCount
		{
			get => (int)GetValue(PreLoadCountProperty);
            set => SetValue(PreLoadCountProperty, value);
        }

		public AdvancedListView() : base(ListViewCachingStrategy.RecycleElement)
		{
			ItemAppearing += OnItemAppearing;
			ItemTapped += OnItemTapped;
		}

		public ICommand ItemSelectedCommand
		{
			get => (ICommand)GetValue(ItemSelectedCommandProperty);
            set => SetValue(ItemSelectedCommandProperty, value);
        }

		private void OnItemTapped(object sender, ItemTappedEventArgs e)
		{
			if (e.Item != null && this.ItemSelectedCommand != null && this.ItemSelectedCommand.CanExecute(e))
			{
				ItemSelectedCommand.Execute(e.Item);
				SelectedItem = null;
			}
		}

		void LoadMoreItems()
		{
			var command = _incrementalLoading.LoadMoreItemsCommand;
			if (command != null && command.CanExecute(null))
				command.Execute(null);
		}

		private void OnItemAppearing(object sender, ItemVisibilityEventArgs e)
		{
			if (ItemsSource != null )
            {
				if (_incrementalLoading == null) return;

                var position = e.ItemIndex;

				if (PreLoadCount <= 0)
					PreLoadCount = 1;

				int preLoadIndex = Math.Max(_itemsSource.Count - PreLoadCount, 0);

				if ((position > _lastPosition || (position == _itemsSource.Count - 1)) && (position >= preLoadIndex))
				{
					_lastPosition = position;

					if (!_incrementalLoading.IsLoadingIncrementally && !IsRefreshing && _incrementalLoading.HasMoreItems)
					{
                        _incrementalLoading.PageIndex += 1;
						LoadMoreItems();
					}
				}
			}
		}

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == ItemsSourceProperty.PropertyName)
            {
                _itemsSource = ItemsSource as IList;

                if (_itemsSource == null)
                {
                    throw new Exception($"{nameof(AdvancedListView)} requires that {nameof(ItemsSource)} be of type IList");
                }
            }
        }

		protected override void OnBindingContextChanged()
		{
			base.OnBindingContextChanged();

			if (BindingContext != null)
			{
				_incrementalLoading = BindingContext as ISupportIncrementalLoading;

				if (_incrementalLoading == null)
				{
					System.Diagnostics.Debug.WriteLine($"{nameof(AdvancedListView)} BindingContext does not implement {nameof(ISupportIncrementalLoading)}. This is required for incremental loading to work.");
				}
			}
		}
	}
}
