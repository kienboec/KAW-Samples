using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Apache.NMS;
using Apache.NMS.ActiveMQ;
using Apache.NMS.ActiveMQ.Commands;
using FancyNumbersGame.ActiveMQ.Common;

namespace FancyNumbersGame.UI.Communication
{
    public class ActiveMQComHandler : IComHandler
    {
        private IConnectionFactory _factory;
        private IConnection _connection;
        private ISession _session;

        private IDestination _generateNewNumberDestination;
        private IMessageProducer _generateNewNumberProducer;

        private IDestination _sendGuessDestination;
        private IMessageProducer _sendGuessProducer;

        private IDestination _requestWinnerCalculationDestination;
        private IMessageProducer _requestWinnerCalculationProducer;

        private IDestination _responseWinnerCalculationDestination;
        private IMessageConsumer _responseWinnerCalculationConsumer;

        public ActiveMQComHandler()
        {
            _factory = new ConnectionFactory("tcp://localhost:61616");
            ((ConnectionFactory)_factory).PrefetchPolicy.SetAll(0);
            _connection = _factory.CreateConnection();
            _connection.Start();
            _session = _connection.CreateSession();

        }

        public async Task CallGenerateNewNumber()
        {
            if (_generateNewNumberProducer == null)
            {
                _generateNewNumberDestination = _session.GetQueue("GenerateNewNumberQueue");
                _generateNewNumberProducer = _session.CreateProducer(_generateNewNumberDestination);
            }

            _generateNewNumberProducer.Send(
                _generateNewNumberProducer.CreateObjectMessage(
                    new GenerateNumberMessage() ));
        }

        public async Task CallSendGuess(string username, int guess)
        {
            if (_sendGuessProducer == null)
            {
                _sendGuessDestination = _session.GetQueue("SendGuessQueue");
                _sendGuessProducer = _session.CreateProducer(_sendGuessDestination);
            }

            _sendGuessProducer.Send(
                _sendGuessProducer.CreateObjectMessage(
                    new SendGuessMessage(username, guess)));
        }

        public async Task<string> CallCalculateWinner()
        {
            try
            {
                if (_requestWinnerCalculationProducer == null)
                {
                    _requestWinnerCalculationDestination = _session.GetQueue("RequestWinnerCalculationQueue");
                    _requestWinnerCalculationProducer = _session.CreateProducer(_requestWinnerCalculationDestination);

                    _responseWinnerCalculationDestination = _session.GetQueue("ResponseWinnerCalculationQueue");
                    _responseWinnerCalculationConsumer = _session.CreateConsumer(_responseWinnerCalculationDestination);
                }

                _requestWinnerCalculationProducer.Send(
                    _requestWinnerCalculationProducer.CreateObjectMessage(
                        new RequestWinnerCalculation())); // TODO: rename to -Message

                IMessage message;
                while ((message = _responseWinnerCalculationConsumer.Receive(TimeSpan.FromMinutes(1))) != null)
                {
                    // map message body to our strongly typed message type
                    var objectMessage = message as IObjectMessage;
                    if (objectMessage?.Body is ResponseWinnerCalculation responseMessage) // TODO rename -Message
                    {
                        return responseMessage.Text;
                    }
                }
            }
            catch (Exception exc)
            {
                // TODO: reopen connection, proper handling of exception type
            }

            return "Error in receiving message";
        }
    }
}
