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

        public async Task<string> APICallAsync(string url, HttpActionType action, string body, bool formatJson = true)
        {
            StringContent contet = new StringContent(body, Encoding.UTF8, "application/json");
            return await APICallAsync(url, action, contet, formatJson);
        }

        public async Task<string> APICallAsync(string url, HttpActionType action, HttpContent? body = null, bool formatJson = true)
        {
            HttpResponseMessage? response = null; 
            switch (action)
            {
                case HttpActionType.GET:
                     response = await _client.GetAsync(url);
                    break;
               
                case HttpActionType.POST:
                    response = await _client.PostAsync(url, body);
                    break;
                case HttpActionType.PUT:
                    response = await _client.PutAsync(url, body);
                    break;
                case HttpActionType.DELETE:
                    response = await _client.DeleteAsync(url);
                    break;
            }

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
