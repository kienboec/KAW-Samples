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
        private bool _readOnlyMode = true;
        public string QuestionText { get; set; }
        public string Answer1 { get; set; }
        public string Answer2 { get; set; }
        public string Answer3 { get; set; }
        public string Answer4 { get; set; }

        public bool ReadOnlyMode
        {
            get
            {
                return _readOnlyMode;
            }
            set
            {
                _readOnlyMode = value;
                SendAnswerCommand.RaiseCanExecuteChanged();
            }
        }

        public RelayCommand<string> SendAnswerCommand { get; set; }

        public bool ForceDesignMode { get; set; } = false;

        private Communicator _com = new Communicator();

        public MainViewModel() : this(false)
        {
        }

        public MainViewModel(bool forceDesignMode)
        {
            ForceDesignMode = forceDesignMode;
            SendAnswerCommand = new RelayCommand<string>(
                async (p) =>
                {
                    await SendAnswerAction(p);
                }, 
                (parameter) => { return !ReadOnlyMode; });
            InitData();
        }

        private async Task InitData()
        {
            if (IsInDesignMode || ForceDesignMode)
            {
                SetQuestionContentForDesign();
                return;
            }
         
            await ReadQuestionContentFromService();
        }

        private void SetQuestionContentForDesign()
        {
            QuestionText = "How much is the fish?";
            Answer1 = "5,40 €";
            Answer2 = "a lot";
            Answer3 = "the price of 2 fish";
            Answer4 = "no one knows";
        }

        private async Task ReadQuestionContentFromService()
        {
            
            var root = await _com.ReadQuestionContentFromService();
            
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

        private async Task SendAnswerAction(string stringParameter)
        {
            await _com.SendAnswer(stringParameter);
        }
    }
}
