using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EventsLister.CBP
{
    public class NetworkCommunicationHandler : INetworkCommunicationHandler
    {
        private string _cachedContent = null;

        public async Task<string> GetHttpContentAsync(string url)
        {
            if (_cachedContent != null)
            {
                return await Task.FromResult(_cachedContent);
            }

            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                var pageContent = await response.Content.ReadAsStringAsync();
                return pageContent;
            }
        }
    }
}
