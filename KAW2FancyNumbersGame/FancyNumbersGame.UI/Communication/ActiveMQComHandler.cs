using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apache.NMS;
using Apache.NMS.ActiveMQ;
using FancyNumbersGame.ActiveMQ.Common;

namespace FancyNumbersGame.UI.Communication
{
    public class ActiveMQComHandler : IComHandler
    {
        private IConnectionFactory _factory;
        private IConnection _connection;
        private ISession _session;

        private IDestination _callGenerateNewNumberDestination;
        private IMessageProducer _callGenerateNewNumberProducer;


        private IDestination _callSendGuessDestination;
        private IMessageProducer _callSendGuessProducer;

        private IDestination _callCalcWinnerRequestDestination;
        private IMessageProducer _callCalcWinnerRequestProducer;

        private IDestination _callCalcWinnerResponseDestination;
        private IMessageConsumer _callCalcWinnerResponseConsumer;

        public ActiveMQComHandler()
        {
            // create Com-Objects for ActiveMQ
            _factory = new ConnectionFactory("tcp://localhost:61616");
            ((ConnectionFactory)_factory).PrefetchPolicy.SetAll(0);
            _connection = _factory.CreateConnection();
            _connection.Start();
            _session = _connection.CreateSession();

            _callGenerateNewNumberDestination = _session.GetQueue("GenerateNewNumberQueue");
            _callGenerateNewNumberProducer = _session.CreateProducer(_callGenerateNewNumberDestination);

            _callSendGuessDestination = _session.GetQueue("SendGuessQueue");
            _callSendGuessProducer = _session.CreateProducer(_callSendGuessDestination);

            _callCalcWinnerRequestDestination = _session.GetQueue("CalcWinnerRequestQueue");
            _callCalcWinnerRequestProducer = _session.CreateProducer(_callCalcWinnerRequestDestination);

            _callCalcWinnerResponseDestination = _session.GetQueue("CalcWinnerResponseQueue");
            _callCalcWinnerResponseConsumer = _session.CreateConsumer(_callCalcWinnerResponseDestination);
        }

        public void CallGenerateNewNumber()
        {
            // producer bound to specific Queue (=Destination) sends a message
            _callGenerateNewNumberProducer.Send(
                _callGenerateNewNumberProducer.CreateObjectMessage(
                    new GenerateNewNumberMessage()));
        }

        public void CallSendGuess(string username, int guess)
        {
            // producer bound to specific Queue (=Destination) sends a message
            _callSendGuessProducer.Send(
                _callSendGuessProducer.CreateObjectMessage(
                    new SendGuessMessage() { Username = username, Guess = guess}));
        }

        public string CallCalculateWinner()
        {
            // producer bound to specific Queue (=Destination) sends a message
            _callCalcWinnerRequestProducer.Send(
                _callCalcWinnerRequestProducer.CreateObjectMessage(
                    new CalcWinnerRequestMessage()));

            // our internal protocol defines that after this request a response will be sent
            // so wait for 1min to receive this message which consists the winner-string
            IMessage message;
            while ((message = _callCalcWinnerResponseConsumer.Receive(TimeSpan.FromMinutes(1))) != null)
            {
                // map message body to our strongly typed message type
                var objectMessage = message as IObjectMessage;
                if (objectMessage?.Body is CalcWinnerResponseMessage responseMessage)
                {
                    return responseMessage.Text;
                }
            }

            return "Error in communication";
        }
    }
}
