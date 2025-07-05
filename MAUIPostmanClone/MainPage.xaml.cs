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
            HttpMethodPicker.SelectedIndexChanged += (s, e) =>
            {
                var method = HttpMethodPicker.SelectedItem?.ToString();
                BodyEditor.IsVisible = method == "POST" || method == "PUT";
            };
        }

        //
        private async void OnSendClicked(object sender, EventArgs e)
        {
            var method = HttpMethodPicker.SelectedItem?.ToString();

            var url = UrlEntry.Text?.Trim();

            if (_api.IsValidUrl(url) == false)
                ResponseEditor.Text = "Invalid Url!";


            string result = await _api.ExecuteAPICallAsync(url, HttpActionType.GET);

            /*   if (method == "POST" || method == "PUT")
               {
                   string jsonBody = BodyEditor.Text ?? "";
                   request.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
               }*/

            //var response = await _client.SendAsync(request);
            //string responseText = await response.Content.ReadAsStringAsync();

            ResponseEditor.Text = result;
        }
    }

}
