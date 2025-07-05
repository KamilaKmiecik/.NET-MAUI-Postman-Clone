using System.Text;

namespace MAUIPostmanClone
{
    public partial class MainPage : ContentPage
    {
        readonly HttpClient _client = new();

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
            try
            {
                var method = HttpMethodPicker.SelectedItem?.ToString();
                var url = UrlEntry.Text?.Trim();
                if (string.IsNullOrWhiteSpace(url)) return;

                HttpRequestMessage request = new(new HttpMethod(method), url);

                if (method == "POST" || method == "PUT")
                {
                    string jsonBody = BodyEditor.Text ?? "";
                    request.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
                }

                var response = await _client.SendAsync(request);
                string responseText = await response.Content.ReadAsStringAsync();

                ResponseEditor.Text = $"Status: {(int)response.StatusCode} {response.ReasonPhrase}\n\n{responseText}";
            }
            catch (Exception ex)
            {
                ResponseEditor.Text = $"Error: {ex.Message}";
            }
        }
    }

}
