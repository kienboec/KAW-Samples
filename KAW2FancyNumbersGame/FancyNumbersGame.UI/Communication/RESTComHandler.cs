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
        public void CallGenerateNewNumber()
        {
            Task.Run(async () =>
            {
                HttpClient client = new HttpClient();
                var content = new StringContent(string.Empty);
                await client.PostAsync("https://localhost:44376/api/Numbers", content);
            });
        }

        public void CallSendGuess(string username, int guess)
        {
            Task.Run(async () =>
            {
                HttpClient client = new HttpClient();
                var content = JsonContent.Create(guess, typeof(int));
                await client.PostAsync("https://localhost:44376/api/Numbers/" + username, content);
            });
        }

        public string CallCalculateWinner()
        {
            WebClient client = new WebClient();
            return client.DownloadString("https://localhost:44376/api/Numbers/");
        }
    }
}
