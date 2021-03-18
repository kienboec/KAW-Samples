using System;
using Apache.NMS;
using Apache.NMS.ActiveMQ;
using KAW2HashFinder.Common;

namespace KAW2HashFinder.Worker
{
    class Program
    {
        static void Main(string[] args)
        {
            // init connection to activeMQ
            IConnectionFactory factory = new ConnectionFactory("tcp://localhost:61616");
            ((ConnectionFactory)factory).PrefetchPolicy.SetAll(0);
            IConnection connection = factory.CreateConnection();
            connection.Start();
            ISession session = connection.CreateSession();

            IDestination requestDestination = session.GetQueue(HashFinderConfig.RequestQueueName);
            IMessageConsumer consumer = session.CreateConsumer(requestDestination);

            IDestination responseDestination = session.GetQueue(HashFinderConfig.ResponseQueueName);
            IMessageProducer producer = session.CreateProducer(responseDestination);

            // -----------------------------------------------------------------------------------------------

            // read worker-messages
            IMessage message;
            while ((message = consumer.Receive(TimeSpan.FromMinutes(1))) != null)
            {
                
                // map message body to our strongly typed message type
                var objectMessage = message as IObjectMessage;
                if (objectMessage?.Body is RequestMessage mapMessage)
                {
                    // debug output
                    Console.WriteLine(mapMessage.PinToCalculate);

                    // analyze pin
                    if (PinUtil.GetMD5Hash(mapMessage.PinToCalculate) == mapMessage.ResultHash)
                    {

                        // pin found!
                        Console.WriteLine(
                            $"yeaaah found the hash {mapMessage.PinToCalculate} - {mapMessage.ResultHash}");

                        producer.Send(producer.CreateObjectMessage(
                            new ResponseMessage()
                            {
                                ResultPin = mapMessage.PinToCalculate,
                                ResultHash = mapMessage.ResultHash
                            }));
                        break;
                    }
                }
            }

            while (consumer.Receive(TimeSpan.FromSeconds(1)) != null)
            {
            }

            Console.WriteLine("... processing finished");
            Console.WriteLine("Press enter to continue...");
            Console.ReadLine();
        }
    }
}
