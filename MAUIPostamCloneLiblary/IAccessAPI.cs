
namespace PostmanCloneLibrary
{
    public interface IAccessAPI
    {
        Task<string> ExecuteAPICallAsync(string url, HttpActionType action, bool formatJson = true);
        bool IsValidUrl(string url);
    }
}