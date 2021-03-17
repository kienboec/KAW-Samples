using System;
using System.IO;
using Apache.NMS;
using Apache.NMS.ActiveMQ;
using SETI.Server;

namespace SETI.InClass
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
            IMessageProducer producer = session.CreateProducer(dest);

            var lines = File.ReadAllLines("spacemap.txt");
            foreach (var line in lines)
            {
                var objectMessage = producer.CreateObjectMessage(new SETIMessage() { Content = line });
                producer.Send(objectMessage);
            }

            Console.WriteLine("... processing finished");
        }
    }
}
