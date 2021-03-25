using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace FancyNumbersGame.UI.Communication
{
    public class RESTComHandler : IComHandler
    {
        HttpClient _client = new HttpClient();

        public async Task CallGenerateNewNumber()
        {
            await _client.PostAsync("https://localhost:44376/api/Numbers", new StringContent(string.Empty));
        }

        public async Task CallSendGuess(string username, int guess)
        {
            await _client.PostAsync(
                "https://localhost:44376/api/Numbers/" + username, 
                JsonContent.Create(guess, typeof(int)));
        }

        public async Task<string> CallCalculateWinner()
        {
            WebClient client = new WebClient();
            Task<string> task = Task.Run(()=>client.DownloadString(new Uri("https://localhost:44376/api/Numbers/")));
            task.Wait();
            return task.Result;
        }
    }
}
