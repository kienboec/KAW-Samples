using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Apache.NMS;
using Apache.NMS.ActiveMQ;
using QuestionService.Common;

namespace QuestionService.NetworkCommunication
{
    public class ServiceCommunication : IServiceCommunication
    {
        private IConnectionFactory _factory = new ConnectionFactory("tcp://localhost:61616");
        private IConnection _connection = null;
        private ISession _session = null;
        private IDestination _sentAnswerDestination = null;
        private IMessageProducer _sentAnswerProducer = null;

        public ServiceCommunication()
        {
            _connection = _factory.CreateConnection();
            _connection.Start();
            _session = _connection.CreateSession();
            _sentAnswerDestination = _session.GetQueue("QuestionService.SentAnswers");
            _sentAnswerProducer = _session.CreateProducer(_sentAnswerDestination);
        }

        public void SendAnswerSentMessage(in int id)
        {
            // <EnableUnsafeBinaryFormatterSerialization>true</EnableUnsafeBinaryFormatterSerialization>
            var message = _sentAnswerProducer.CreateTextMessage((new AnswerSentMessage(id)).ToString());
            _sentAnswerProducer.Send(message);
        }
    }
}
