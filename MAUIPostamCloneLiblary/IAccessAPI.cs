


namespace PostmanCloneLibrary
{
    public interface IAccessAPI
    {
        Task<string> APICallAsync(string url, HttpActionType action, string body, bool formatJson = true);
        Task<string> APICallAsync(string url, HttpActionType action, HttpContent? body = null, bool formatJson = true);
        bool IsValidUrl(string url);
    }
}