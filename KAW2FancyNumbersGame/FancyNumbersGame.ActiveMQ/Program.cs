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
        // following messages taken over from controller

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

            return $"{(winner?.Key) ?? "-"}: {(winner?.Value ?? 0).ToString()} ({_secretNumber})";
        }

        static void Main(string[] args)
        {
            // define all objects required for communication (connection, session, destinations (queue), producer, consumer)
            IConnectionFactory factory;
            IConnection connection;
            ISession session;

            IDestination callGenerateNewNumberDestination;
            IMessageConsumer callGenerateNewNumberConsumer;

            IDestination callSendGuessDestination;
            IMessageConsumer callSendGuessConsumer;

            IDestination callCalcWinnerRequestDestination;
            IMessageConsumer callCalcWinnerRequestConsumer;

            IDestination callCalcWinnerResponseDestination;
            IMessageProducer callCalcWinnerResponseProducer;

            factory = new ConnectionFactory("tcp://localhost:61616");
            ((ConnectionFactory) factory).PrefetchPolicy.SetAll(0);
            connection = factory.CreateConnection();
            connection.Start();
            session = connection.CreateSession();

            callGenerateNewNumberDestination = session.GetQueue("GenerateNewNumberQueue");
            callGenerateNewNumberConsumer = session.CreateConsumer(callGenerateNewNumberDestination);

            callSendGuessDestination = session.GetQueue("SendGuessQueue");
            callSendGuessConsumer = session.CreateConsumer(callSendGuessDestination);

            callCalcWinnerRequestDestination = session.GetQueue("CalcWinnerRequestQueue");
            callCalcWinnerRequestConsumer = session.CreateConsumer(callCalcWinnerRequestDestination);

            callCalcWinnerResponseDestination = session.GetQueue("CalcWinnerResponseQueue");
            callCalcWinnerResponseProducer = session.CreateProducer(callCalcWinnerResponseDestination);

            // define a task to be able to receive multiple messages
            // consider here a monitor to be able to work properly in multi-threaded environments (C# lock)
            var callGenerateNewNumberTask = Task.Run(() =>
            {
                IMessage message;
                while ((message = callGenerateNewNumberConsumer.Receive(TimeSpan.FromMinutes(60))) != null)
                {
                    // map message body to our strongly typed message type
                    var objectMessage = message as IObjectMessage;
                    if (objectMessage?.Body is GenerateNewNumberMessage)
                    {
                        GenerateNewNumber();
                        // Console.WriteLine($"Secret number generated: {_secretNumber}");
                    }
                }
            });

            // yet another reading task executing internal methods
            var callSendGuessMessageTask = Task.Run(() =>
            {
                IMessage message;
                while ((message = callSendGuessConsumer.Receive(TimeSpan.FromMinutes(60))) != null)
                {
                    // map message body to our strongly typed message type
                    var objectMessage = message as IObjectMessage;
                    if (objectMessage?.Body is SendGuessMessage sendGuessMessage)
                    {
                        AddGuess(sendGuessMessage.Username, sendGuessMessage.Guess);
                        // Console.WriteLine($"  add guess {sendGuessMessage.Username} {sendGuessMessage.Guess}");
                    }
                }
            });

            // task responsible for a communication with request and response (based on specific queues)
            var callCalcWinnerRequestTask = Task.Run(() =>
            {
                IMessage message;
                while ((message = callCalcWinnerRequestConsumer.Receive(TimeSpan.FromMinutes(60))) != null)
                {
                    // map message body to our strongly typed message type
                    var objectMessage = message as IObjectMessage;
                    if (objectMessage?.Body is CalcWinnerRequestMessage)
                    {
                        var text = CalculateWinner();
                        callCalcWinnerResponseProducer.Send(
                            callCalcWinnerResponseProducer.CreateObjectMessage(
                                new CalcWinnerResponseMessage() { Text = text}));
                        // Console.WriteLine("  " + text);
                    }
                }
            });

            // wait in the main-flow until all tasks are done
            Task.WaitAll(callGenerateNewNumberTask, callSendGuessMessageTask, callCalcWinnerRequestTask);
        }
    }
}
