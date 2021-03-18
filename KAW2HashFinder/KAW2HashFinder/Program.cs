using System;
using System.Security.Cryptography;
using System.Text;
using Apache.NMS;
using Apache.NMS.ActiveMQ;
using KAW2HashFinder.Common;

namespace KAW2HashFinder
{
    class Program
    {
        static void Main(string[] args)
        {
            // connect to activeMQ
            IConnectionFactory factory = new ConnectionFactory(HashFinderConfig.ActiveMQEndpoint);
            IConnection connection = factory.CreateConnection();
            connection.Start();
            ISession session = connection.CreateSession();

            IDestination dest = session.GetQueue(HashFinderConfig.RequestQueueName);
            IMessageProducer producer = session.CreateProducer(dest);

            // -----------------------------------------------------------------------------------------------

            // handle UI input and calculate Hash
            Console.Write("Please enter your pin: ");
            var pin = Console.ReadLine();
            var hashedPin = GetMD5Hash(pin);
            Console.WriteLine();
            Console.WriteLine("your hash is: " + hashedPin);

            // -----------------------------------------------------------------------------------------------

            for (int i = 0; i <= 9999; i++)
            {
                var objectMessage = producer.CreateObjectMessage(
                    new RequestMessage()
                    {
                        PinToCalculate = i.ToString("0000"),
                        ResultHash = hashedPin, 
                    });
                producer.Send(objectMessage);
            }

            // -----------------------------------------------------------------------------------------------

            Console.WriteLine("... processing finished");
        }

        public static string GetMD5Hash(string pin)
        {
            var hashedPinBytes = MD5.HashData(Encoding.UTF8.GetBytes(pin));

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < hashedPinBytes.Length; i++)
            {
                builder.Append(hashedPinBytes[i].ToString("x2"));
            }

            return builder.ToString();
        }
    }
}
