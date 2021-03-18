using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
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

            IDestination requestDestination = session.GetQueue(HashFinderConfig.RequestQueueName);
            IMessageProducer producer = session.CreateProducer(requestDestination);

            IDestination responseDestination = session.GetQueue(HashFinderConfig.ResponseQueueName);
            IMessageConsumer consumer = session.CreateConsumer(responseDestination);

            // -----------------------------------------------------------------------------------------------

            // handle UI input and calculate Hash
            Console.Write("Please enter your pin: ");
            var pin = Console.ReadLine();
            var hashedPin = PinUtil.GetMD5Hash(pin);
            Console.WriteLine();
            Console.WriteLine("your hash is: " + hashedPin);

            // -----------------------------------------------------------------------------------------------

            var itemFound = false;
            Task t = Task.Run(() =>
            {
                // create messages for workers
                for (int i = 0; i <= 9999 && !itemFound; i++)
                {
                    var objectMessage = producer.CreateObjectMessage(
                        new RequestMessage()
                        {
                            PinToCalculate = i.ToString("0000"),
                            ResultHash = hashedPin,
                        });
                    producer.Send(objectMessage);
                }
            });

            // -----------------------------------------------------------------------------------------------

            // read worker-responses
            IMessage message;
            if ((message = consumer.Receive(TimeSpan.FromMinutes(1))) != null)
            {
                itemFound = true;
                // map message body to our strongly typed message type
                var objectMessage = message as IObjectMessage;
                var mapMessage = objectMessage?.Body as ResponseMessage;

                Console.WriteLine($"pin is: {mapMessage.ResultPin} ({mapMessage.ResultHash})");
            }

            Task.WaitAll(new[] {t});

            // final UI candy
            Console.WriteLine("... processing finished");
            Console.WriteLine("Press enter to continue...");
            Console.ReadLine();
        }


    }
}
