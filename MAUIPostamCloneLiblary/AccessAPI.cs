using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PostmanCloneLibrary
{
    public class AccessAPI : IAccessAPI
    {
        private readonly HttpClient _client = new();

        public async Task<string> ExecuteAPICallAsync(string url, HttpActionType action, bool formatJson = true)
        {
            HttpRequestMessage request = new(new HttpMethod(action.ToString()), url);

            //if (method == "POST" || method == "PUT")
            //{
            //    string jsonBody = BodyEditor.Text ?? "";
            //    request.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            //}

            var response = await _client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();

                if (formatJson)
                {
                    var jsonDeserialized = JsonSerializer.Deserialize<JsonElement>(json);
                    json = JsonSerializer.Serialize(jsonDeserialized, new JsonSerializerOptions() { WriteIndented = true, IncludeFields = true });
                }

                return json;
            }

            return $"Error status code: {response.StatusCode}";

        }



        public bool IsValidUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                return false;

            return Uri.TryCreate(url, UriKind.Absolute, out Uri uri) && uri.Scheme == Uri.UriSchemeHttps;
        }
    }
}
