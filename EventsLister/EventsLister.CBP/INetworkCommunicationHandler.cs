using System.Threading.Tasks;

namespace EventsLister.CBP
{
    public interface INetworkCommunicationHandler
    {
        Task<string> GetHttpContentAsync(string url);
    }
}