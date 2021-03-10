using System.Threading.Tasks;

namespace EventsLister.CBP
{
    public interface IUIHandler
    {
        Task InitAsync();
    }
}