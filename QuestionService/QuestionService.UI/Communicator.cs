using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace QuestionService.UI
{
    public class Communicator
    {
        public async Task<JsonElement> ReadQuestionContentFromService()
        {
            // curl -X GET "https://localhost:44318/api/Question" -H  "accept: text/plain"
            HttpClient client = new HttpClient();

            // var content = await client.GetStringAsync("https://localhost:44318/api/Question");
            var responseMessage = await client.GetAsync("https://localhost:44318/api/Question");
            var content = await responseMessage.Content.ReadAsStringAsync();

            var document = JsonDocument.Parse(content);
            return document.RootElement;
            
            // Alternative approach: System.Text.Json.JsonSerializer.Deserialize<Question>(content)
        }

        public async Task SendAnswer(string stringParameter)
        {
            int parameter = int.Parse(stringParameter);

            // curl -X POST "https://localhost:44318/api/Question/answers" -H  "accept: */*" -H  "Content-Type: application/json" -d "4"
            HttpClient client = new HttpClient();
            var content = JsonContent.Create(parameter, typeof(int));
            var response = await client.PostAsync("https://localhost:44318/api/Question/answers", content);
        }
    }
}
