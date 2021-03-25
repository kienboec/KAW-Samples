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
        private RelayCommand _generateNewNumberCommand;
        private RelayCommand _sendGuessCommand;
        private RelayCommand _calculateWinnerCommand;
        private string _winner;
        private IComHandler _comHandler;

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
            _comHandler = new ActiveMQComHandler();
        }

        private void GenerateNewNumberAction()
        {
            _comHandler.CallGenerateNewNumber();
        }

        private void SendGuessAction()
        {
            _comHandler.CallSendGuess(this.Username, this.Guess);
        }

        private void CalculateWinnerAction()
        {
            Winner = _comHandler.CallCalculateWinner();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
