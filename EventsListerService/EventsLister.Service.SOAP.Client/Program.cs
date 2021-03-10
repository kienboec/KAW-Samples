using System;
using System.Threading.Tasks;
using FHTWService;

namespace EventsLister.Service.SOAP.Client
{
    class Program
    {
        async static Task Main(string[] args)
        {
            IFHTWService service = new FHTWServiceClient();
            Console.WriteLine((await service.HasDataAsync(new HasDataRequest())).HasDataResult);
            (await service.GetAllEventsAsync(new GetAllEventsRequest())).GetAllEventsResult.ForEach(e => Console.WriteLine($"{e.Date}: {e.Name}"));
            Console.WriteLine((await service.HasDataAsync(new HasDataRequest())).HasDataResult);
        }
    }
}
