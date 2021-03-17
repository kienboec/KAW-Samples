using System;
using System.Threading;
using Apache.NMS;
using Apache.NMS.ActiveMQ;
using SETI.Server;

namespace SETI.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            IConnectionFactory factory = new ConnectionFactory("tcp://localhost:61616");
            IConnection connection = factory.CreateConnection();
            connection.Start();
            ISession session = connection.CreateSession();
            IDestination dest = session.GetQueue("KAW.Seti");
            IMessageConsumer consumer = session.CreateConsumer(dest);
            IMessage message;
            while ((message = consumer.Receive(TimeSpan.FromMilliseconds(2000))) != null)
            {
                var objectMessage = message as IObjectMessage;
                var mapMessage = objectMessage?.Body as SETIMessage;
                if (mapMessage?.Content?.Contains(">D") ?? false)
                {
                    Console.WriteLine(mapMessage.Content);
                }
                Thread.Sleep(TimeSpan.FromMinutes(1));
            }

            Console.WriteLine("... processing finished");
        }
    }
}
