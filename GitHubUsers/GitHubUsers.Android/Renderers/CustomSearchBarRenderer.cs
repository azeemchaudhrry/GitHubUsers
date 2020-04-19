using Android.Content;
using GitHubUsers.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: Xamarin.Forms.ExportRenderer(typeof(SearchBar), typeof(CustomSearchBarRenderer))]
namespace GitHubUsers.Droid.Renderers
{
    public class CustomSearchBarRenderer : SearchBarRenderer
    {
        private Context _context;
        public CustomSearchBarRenderer(Context context) : base(context)
        {
            _context = context;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<SearchBar> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                var plateId = Resources.GetIdentifier("android:id/search_plate", null, null);
                var plate = Control.FindViewById(plateId);
                plate.SetBackgroundColor(Android.Graphics.Color.Transparent);
            }
        }
    }
}