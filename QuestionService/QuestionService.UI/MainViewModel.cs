using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace QuestionService.UI
{
    public class MainViewModel : ViewModelBase
    {
        public string QuestionText { get; set; }
        public string Answer1 { get; set; }
        public string Answer2 { get; set; }
        public string Answer3 { get; set; }
        public string Answer4 { get; set; }

        public RelayCommand<string> SendAnswerCommand { get; set; }

        public bool ForceDesignMode { get; set; } = false;

        public MainViewModel() : this(false)
        {
        }

        public MainViewModel(bool forceDesignMode)
        {
            ForceDesignMode = forceDesignMode;
            SendAnswerCommand = new RelayCommand<string>(SendAnswerAction);
            InitData();
        }

        private void InitData()
        {
            if (IsInDesignMode || ForceDesignMode)
            {
                SetQuestionContentForDesign();
                return;
            }
         
            ReadQuestionContentFromService();
        }

        private void SetQuestionContentForDesign()
        {
            QuestionText = "How much is the fish?";
            Answer1 = "5,40 €";
            Answer2 = "a lot";
            Answer3 = "the price of 2 fish";
            Answer4 = "no one knows";
        }

        private async void ReadQuestionContentFromService()
        {
            // curl -X GET "https://localhost:44318/api/Question" -H  "accept: text/plain"
            HttpClient client = new HttpClient();
            
            // var content = await client.GetStringAsync("https://localhost:44318/api/Question");
            var responseMessage = await client.GetAsync("https://localhost:44318/api/Question");
            var content = await responseMessage.Content.ReadAsStringAsync();
            
            var document = JsonDocument.Parse(content);
            var root = document.RootElement;
            QuestionText = root.GetProperty("text").ToString();
            RaisePropertyChanged(nameof(QuestionText));
            
            Answer1 = root.GetProperty("answer1").ToString();
            RaisePropertyChanged(nameof(Answer1));
            Answer2 = root.GetProperty("answer2").ToString();
            RaisePropertyChanged(nameof(Answer2));
            Answer3 = root.GetProperty("answer3").ToString();
            RaisePropertyChanged(nameof(Answer3));
            Answer4 = root.GetProperty("answer4").ToString();
            RaisePropertyChanged(nameof(Answer4));

            // Alternative approach: System.Text.Json.JsonSerializer.Deserialize<Question>(content)
        }

        private async void SendAnswerAction(string stringParameter)
        {
            int parameter = int.Parse(stringParameter);

            // curl -X POST "https://localhost:44318/api/Question/answers" -H  "accept: */*" -H  "Content-Type: application/json" -d "4"
            HttpClient client = new HttpClient();
            var content = JsonContent.Create(parameter, typeof(int));
            var response = await client.PostAsync("https://localhost:44318/api/Question/answers", content);

        }
    }
}
