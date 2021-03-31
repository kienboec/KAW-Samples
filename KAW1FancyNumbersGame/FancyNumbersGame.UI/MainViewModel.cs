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
using FancyNumbersGame.UI.Communication;

namespace FancyNumbersGame.UI
{
    class MainViewModel : INotifyPropertyChanged
    {
        private IComHandler _comHandler = null;

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

        public MainViewModel()
        {
            // _comHandler = new ActiveMQComHandler();
            _comHandler = new RESTComHandler();
        }

        private async void GenerateNewNumberAction()
        {
            await _comHandler.CallGenerateNewNumber();
        }

        private async void SendGuessAction()
        {
            await _comHandler.CallSendGuess(this.Username, this.Guess);
        }

        private async void CalculateWinnerAction()
        {
            Winner = await _comHandler.CallCalculateWinner();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
