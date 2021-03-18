using System;
using System.Threading;
using Apache.NMS;
using Apache.NMS.ActiveMQ;
using KAW1HashFinder.Common;

namespace KAW1HashFinder.Worker
{
    class Program
    {
        static void Main(string[] args)
        {
            // initialize connection
            IConnectionFactory factory = new ConnectionFactory("tcp://localhost:61616");
            ((ConnectionFactory)factory).PrefetchPolicy.SetAll(0);
            IConnection connection = factory.CreateConnection();
            connection.Start();
            ISession session = connection.CreateSession();
            
            IDestination destRequestQueue = session.GetQueue(HashFinderConfig.RequestQueueName);
            IMessageConsumer consumer = session.CreateConsumer(destRequestQueue);

            IDestination destResponseQueue = session.GetQueue(HashFinderConfig.ResponseQueueName);
            IMessageProducer producer = session.CreateProducer(destResponseQueue);

            // ------------------------------------------------------------------------------------------------------------

            // receive packages
            IMessage message;
            while ((message = consumer.Receive(TimeSpan.FromMinutes(1))) != null)
            {
                var objectMessage = message as IObjectMessage;
                var mapMessage = objectMessage?.Body as HashFinderRequest;
                
                // analyze packages
                if (PinUtil.GetPinHash(mapMessage?.PinToCalculate) == (mapMessage?.HashToFind ?? "invalid hash"))
                {

                    // successfully found hash
                    Console.WriteLine("hash found for pin " + (mapMessage.PinToCalculate ?? "<invalid>"));

                    // answer to response queue 
                    producer.Send(
                        producer.CreateObjectMessage(new HashFinderResponse()
                        {
                            FoundPin= mapMessage.PinToCalculate,
                            HashToFind = mapMessage?.HashToFind
                        }));
                    break;
                }
            }
            
            // clean queue, so that in the next start of this service we don't need to bother with old data
            while (consumer.Receive(TimeSpan.FromSeconds(1)) != null)
            {
                // clean request queue
            }

            // ------------------------------------------------------------------------------------------------------------
            
            // keep window open, so we can see the std-out (and check if the pin to find is right)
            Console.WriteLine("... processing finished");
            Console.WriteLine("press enter to stop");
            Console.ReadLine();
        }
    }
}
