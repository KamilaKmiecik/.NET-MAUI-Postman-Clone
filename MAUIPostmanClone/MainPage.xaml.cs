using PostmanCloneLibrary;
using System.Text;

namespace MAUIPostmanClone
{
    public partial class MainPage : ContentPage
    {
        //overkill for now, but i am gonna add DI 
        readonly IAccessAPI _api = new AccessAPI();

        public MainPage()
        {
            InitializeComponent();

            HttpMethodPicker.ItemsSource = Enum.GetNames(typeof(HttpActionType)).ToList();
            HttpMethodPicker.SelectedItem = HttpActionType.GET.ToString();

            HttpMethodPicker.SelectedIndexChanged += (s, e) =>
            {
                var method = HttpMethodPicker.SelectedItem?.ToString();
                BodyEditor.IsVisible = method == nameof(HttpActionType.POST) || method == nameof(HttpActionType.PUT);
            };
        }

        private async void OnSendClicked(object sender, EventArgs e)
        {
            var url = UrlEntry.Text?.Trim();

            if (_api.IsValidUrl(url) == false)
            {
                ResponseEditor.Text = "Invalid Url!";
                return;
            }

            if(Enum.TryParse(HttpMethodPicker.SelectedItem.ToString(), out HttpActionType action) == false)
            {
                ResponseEditor.Text = "Invalid HTTP Action!";
                return;
            }

            string result = await _api.APICallAsync(url, action);

            ResponseEditor.Text = result;
        }
    }

}
