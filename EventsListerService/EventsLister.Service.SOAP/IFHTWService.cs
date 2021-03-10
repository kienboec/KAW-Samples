using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace EventsLister.Service.SOAP
{
    [ServiceContract]
    public interface IFHTWService
    {
        [OperationContract]
        bool HasData();

        [OperationContract]
        Task<IEnumerable<FHTWEvent>> GetAllEvents();
    }
}
