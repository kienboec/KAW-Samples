using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using Apache.NMS;
using Apache.NMS.ActiveMQ;
using KAW1HashFinder.Common;

namespace KAW1HashFinder
{
    class Program
    {
        static void Main(string[] args)
        {
            // initialize connection to active mq with producer and consumer
            IConnectionFactory factory = new ConnectionFactory("tcp://localhost:61616");
            IConnection connection = factory.CreateConnection();
            connection.Start();
            ISession session = connection.CreateSession();

            IDestination destRequestQueue = session.GetQueue(HashFinderConfig.RequestQueueName);
            IMessageProducer producer = session.CreateProducer(destRequestQueue);

            IDestination destResponseQueue = session.GetQueue(HashFinderConfig.ResponseQueueName);
            IMessageConsumer consumer = session.CreateConsumer(destResponseQueue);

            // ------------------------------------------------------------------------------------------------------------

            // calculate a hash to find based on user input
            PrintTimestamp();
            Console.Write("Please enter your pin: ");
            var myPin = Console.ReadLine();
            PrintTimestamp();
            var hash = PinUtil.GetPinHash(myPin);
            PrintTimestamp();
            Console.WriteLine();
            Console.WriteLine("Your hash is: " + hash);

            // ------------------------------------------------------------------------------------------------------------

            // send workers their work to do
            for (int i = 0; i <= 9999; i++)
            {
                var objectMessage = producer.CreateObjectMessage(
                    new HashFinderRequest()
                    {
                        PinToCalculate = i.ToString("0000"), 
                        HashToFind = hash
                    });
                producer.Send(objectMessage);
            }
            PrintTimestamp();

            // ------------------------------------------------------------------------------------------------------------

            IMessage message;
            if ((message = consumer.Receive(TimeSpan.FromMinutes(2))) != null)
            {
                var objectMessage = message as IObjectMessage;
                var mapMessage = objectMessage?.Body as HashFinderResponse;
                Console.WriteLine("Successfully found pin by worker: " + (mapMessage?.FoundPin ?? "<invalid pin>"));
                PrintTimestamp();
            }

            PrintTimestamp();
            Console.WriteLine("... processing finished");
        }

        public static void PrintTimestamp()
        {
            Console.WriteLine($"Timestamp: {DateTime.Now.ToLongTimeString()}".PadRight(80, ' '));
        }
    }
}
