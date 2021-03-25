using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Apache.NMS;
using Apache.NMS.ActiveMQ;
using FancyNumbersGame.ActiveMQ.Common;

namespace FancyNumbersGame.ActiveMQ
{
    class Program
    {
        private static int _secretNumber = 0;
        private static Dictionary<string, int> _guesses = new Dictionary<string, int>();

        private static void GenerateNewNumber()
        {
            var random = new Random(DateTime.Now.Millisecond);
            _secretNumber = random.Next(0, 100);
            _guesses.Clear();
        }

        private static void AddGuess(string username, int guess)
        {
            _guesses[username] = guess;
        }

        private static string CalculateWinner()
        {
            KeyValuePair<string, int>? winner = null;
            foreach (var guess in _guesses)
            {
                if (winner == null || Math.Abs(guess.Value - _secretNumber) < Math.Abs(winner.Value.Value - _secretNumber))
                {
                    winner = guess;
                }
            }

            // return winner; // writes object as json
            return $"{(winner?.Key) ?? "-"}: {(winner?.Value ?? 0).ToString()} ({_secretNumber})";
        }

        static void Main(string[] args)
        {
            // init connection to activeMQ
            IConnectionFactory factory = new ConnectionFactory("tcp://localhost:61616");
            ((ConnectionFactory)factory).PrefetchPolicy.SetAll(0);
            IConnection connection = factory.CreateConnection();
            connection.Start();
            ISession session = connection.CreateSession();

            Task generateNewNumberTask = Task.Run(() =>
            {
                IDestination generateNewNumberDestination = session.GetQueue("GenerateNewNumberQueue");
                IMessageConsumer generateNewNumberConsumer = session.CreateConsumer(generateNewNumberDestination);

                IMessage message;
                while ((message = generateNewNumberConsumer.Receive(TimeSpan.FromMinutes(60))) != null)
                {
                    // map message body to our strongly typed message type
                    var objectMessage = message as IObjectMessage;
                    // TODO: rename to GenerateNewNumberMessage
                    if (objectMessage?.Body is GenerateNumberMessage generateNewNumberMessage)
                    {
                        GenerateNewNumber();
                        Console.WriteLine(_secretNumber.ToString());
                    }
                }
            });

            Task sendGuessTask = Task.Run(() =>
            {
                IDestination sendGuessDestination = session.GetQueue("SendGuessQueue");
                IMessageConsumer sendGuessConsumer = session.CreateConsumer(sendGuessDestination);

                IMessage message;
                while ((message = sendGuessConsumer.Receive(TimeSpan.FromMinutes(60))) != null)
                {
                    // map message body to our strongly typed message type
                    var objectMessage = message as IObjectMessage;
                    if (objectMessage?.Body is SendGuessMessage sendGuessMessage)
                    {
                        AddGuess(sendGuessMessage.Username, sendGuessMessage.Guess);
                        Console.WriteLine($"{sendGuessMessage.Username} - {sendGuessMessage.Guess}");
                    }
                }
            });

            Task requestWinnerCalculationTask = Task.Run(() =>
            {
                IDestination requestWinnerCalculationDestination = session.GetQueue("RequestWinnerCalculationQueue");
                IMessageConsumer requestWinnerCalculationConsumer = session.CreateConsumer(requestWinnerCalculationDestination);

                IDestination responseWinnerCalculationDestination = session.GetQueue("ResponseWinnerCalculationQueue");
                IMessageProducer responseWinnerCalculationProducer = session.CreateProducer(responseWinnerCalculationDestination);

                IMessage message;
                while ((message = requestWinnerCalculationConsumer.Receive(TimeSpan.FromMinutes(60))) != null)
                {
                    // map message body to our strongly typed message type
                    var objectMessage = message as IObjectMessage;
                    if (objectMessage?.Body is RequestWinnerCalculation)
                    {
                        var winnerText = CalculateWinner();
                        responseWinnerCalculationProducer.Send(
                            responseWinnerCalculationProducer.CreateObjectMessage(
                                new ResponseWinnerCalculation()
                                {
                                    Text = winnerText
                                }));

                        Console.WriteLine(winnerText);
                    }
                }
            });

            Task.WaitAll(generateNewNumberTask, sendGuessTask);
        }
    }
}
