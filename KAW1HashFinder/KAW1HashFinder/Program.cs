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
            Console.Write("Please enter your pin: ");
            var myPin = Console.ReadLine();
            var hash = GetPinHash(myPin);
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

            // ------------------------------------------------------------------------------------------------------------

            IMessage message;
            while ((message = consumer.Receive(TimeSpan.FromMinutes(2))) != null)
            {
                var objectMessage = message as IObjectMessage;
                var mapMessage = objectMessage?.Body as HashFinderResponse;
                Console.WriteLine("Successfully found pin by worker: " + (mapMessage?.FoundPin ?? "<invalid pin>"));
            }

            Console.WriteLine("... processing finished");
        }

        public static string GetPinHash(string pin)
        {
            var pinBytes = Encoding.UTF8.GetBytes(pin);
            var hashedPinBytes = MD5.HashData(pinBytes);
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < hashedPinBytes.Length; i++)
            {
                builder.Append(hashedPinBytes[i].ToString("x2"));
            }

            return builder.ToString();
        }
    }
}
