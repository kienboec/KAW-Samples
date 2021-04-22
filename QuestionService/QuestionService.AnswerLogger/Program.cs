using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using Apache.NMS;
using Apache.NMS.ActiveMQ;
using QuestionService.Common;

namespace QuestionService.AnswerLogger
{
    public class Program
    {
        // introduced only to have the right startup order in debugging
        private static async void WaitForRESTService()
        {
            Console.WriteLine("wait for rest service to be up and running... ");
            while (true)
            {
                try
                {
                    HttpClient client = new HttpClient();
                    var response = await client.GetAsync("https://localhost:44318/api/Alive");
                    response.EnsureSuccessStatusCode();
                    var stringContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("... connection established: " + stringContent);

                    break;
                }
                catch (Exception exc)
                {
                    Console.WriteLine("... no connection possible: " + exc.Message);
                    Thread.Sleep(1000);
                }
            }

            // Console.Clear();
        }

        static void Main(string[] args)
        {
            WaitForRESTService();

            IConnectionFactory factory = new ConnectionFactory("tcp://localhost:61616");
            using IConnection connection = factory.CreateConnection();
            connection.Start();

            using ISession session = connection.CreateSession();
            using IDestination dest = session.GetQueue("QuestionService.SentAnswers");

            using IMessageConsumer consumer = session.CreateConsumer(dest);

            IMessage message;
            while ((message = consumer.Receive(TimeSpan.FromHours(1))) != null)
            {
                if (message is IObjectMessage)
                {
                    var objectMessage = message as IObjectMessage;
                    AnswerSentTextFormatter formatter = new AnswerSentTextFormatter(objectMessage?.Body as AnswerSentMessage);
                    WriteOutputString(Console.Out, formatter);
                }
                else if (message is ITextMessage)
                {
                    var textMessage = message as ITextMessage;
                    AnswerSentTextFormatter formatter = new AnswerSentTextFormatter(new AnswerSentMessage(textMessage.Text));
                    WriteOutputString(Console.Out, formatter);
                }
            }
        }

        

        public static void WriteOutputString(TextWriter writer, ITextFormatter formatter)
        {
            string output = formatter.Format(); 
            writer.WriteLine(output);
        }
    }
}
