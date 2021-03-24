using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FancyNumbersGame.UI
{
    class MainViewModel : INotifyPropertyChanged
    {
        private RelayCommand _generateNewNumberCommand;
        private RelayCommand _sendGuessCommand;
        private RelayCommand _calculateWinnerCommand;
        private string _winner;

        public RelayCommand GenerateNewNumberCommand =>
            _generateNewNumberCommand ??= new RelayCommand(GenerateNewNumberAction);
        public RelayCommand SendGuessCommand =>
            _sendGuessCommand ??= new RelayCommand(SendGuessAction);
        public RelayCommand CalculateWinnerCommand =>
            _calculateWinnerCommand ??= new RelayCommand(CalculateWinnerAction);

        public string Username { get; set; }
        public int Guess { get; set; }

        public string Winner
        {
            get { return _winner; }
            set
            {
                _winner = value;
                OnPropertyChanged();
            }
        }

        private void CalculateWinnerAction()
        {
            WebClient client = new WebClient();
            string data = client.DownloadString("https://localhost:44376/api/Numbers/");
            Winner = data;
        }

        private async void SendGuessAction()
        {
            HttpClient client = new HttpClient();
            var content = JsonContent.Create(this.Guess, typeof(int));
            await client.PostAsync("https://localhost:44376/api/Numbers/" + this.Username, content);
        }

        private async void GenerateNewNumberAction()
        {
            HttpClient client = new HttpClient();
            var content = new StringContent(string.Empty);
            await client.PostAsync("https://localhost:44376/api/Numbers", content);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
