using System;

namespace FancyNumbersGame.ActiveMQ.Common
{
    [Serializable]
    public class SendGuessMessage
    {
        public string Username { get; set; }
        public int Guess { get; set; }

        public SendGuessMessage()
        {
        }

        public SendGuessMessage(string username, int guess)
        {
            this.Username = username;
            this.Guess = guess;
        }
    }
}
