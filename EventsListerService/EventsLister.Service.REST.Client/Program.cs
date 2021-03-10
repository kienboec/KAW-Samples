using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace EventsLister.Service.REST.Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Hinzufügen / Dienstverweis / swagger.json - URL imported

            const string endPoint = "https://localhost:44332/";
            swaggerClient client = new swaggerClient(endPoint, new HttpClient());
            (await client.EventsAsync()).ToList().ForEach(x => Console.WriteLine(x));
            
        }
    }
}
