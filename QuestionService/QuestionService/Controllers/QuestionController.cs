using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Apache.NMS;
using Apache.NMS.ActiveMQ;
using QuestionService.Common;
using QuestionService.Database;
using QuestionService.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QuestionService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        public StartupTimeStore Store { get; }

        private static QuestionDbContext _db = null;
        private static IConnectionFactory _factory = new ConnectionFactory("tcp://localhost:61616");
        private static IConnection _connection = null;
        private static ISession _session = null;
        private static IDestination _sentAnswerDestination = null;
        private static IMessageProducer _sentAnswerProducer = null;

        public QuestionController(StartupTimeStore store)
        {
            Store = store;
        }

        static QuestionController()
        {
            _db = new QuestionDbContext();
            _db.Database.EnsureCreated();

            var dbQuestion = _db.Questions.FirstOrDefault();
            if (dbQuestion == null)
            {
                _db.Questions.Add(new DatabaseQuestion()
                {
                    Text = "What is the best programming language?",

                    Answer1 = "Java",
                    Answer2 = "C#",
                    Answer3 = "TypeScript",
                    Answer4 = "All are awesome",

                    RightAnswerIndex = 4,
                });
                _db.Questions.Add(new DatabaseQuestion()
                {
                    Text = "What is the second best programming language?",

                    Answer1 = "Java",
                    Answer2 = "C#",
                    Answer3 = "TypeScript",
                    Answer4 = "All are still the same amount awesome",

                    RightAnswerIndex = 4,
                });
                _db.Questions.Add(new DatabaseQuestion()
                {
                    Id = 10000,
                    Text = "What is the third best programming language?",

                    Answer1 = "Java",
                    Answer2 = "C#",
                    Answer3 = "TypeScript",
                    Answer4 = "All are still the same amount awesome",

                    RightAnswerIndex = 4,
                });
                _db.SaveChanges();
                dbQuestion = _db.Questions.FirstOrDefault();
                
                // postgres=# select * from question;
                //   Id   |                     Text                      | Answer1 | Answer2 |  Answer3   |                Answer4                | RightAnswerIndex
                // -------+-----------------------------------------------+---------+---------+------------+---------------------------------------+------------------
                //      1 | What is the best programming language?        | Java    | C#      | TypeScript | All are awesome                       |                4
                //      2 | What is the second best programming language? | Java    | C#      | TypeScript | All are still the same amount awesome |                4
                //  10000 | What is the third best programming language?  | Java    | C#      | TypeScript | All are still the same amount awesome |                4
                // (3 Zeilen)
            }

            if (dbQuestion != null)
            {
                _question = new Question(dbQuestion);
                _rightAnswerId = dbQuestion.RightAnswerIndex;
            }

            _connection = _factory.CreateConnection();
            _connection.Start();
            _session = _connection.CreateSession();
            _sentAnswerDestination = _session.GetQueue("QuestionService.SentAnswers");
            _sentAnswerProducer = _session.CreateProducer(_sentAnswerDestination);
        }

        private static readonly int[] _answers = new[] { 0, 0, 0, 0 };
        private static Question _question;

        private static int _rightAnswerId = 0;

        [HttpGet]
        public Question Get()
        {
            return _question;
        }

        [HttpPost("answers")]
        public void Post([FromBody] int id)
        {
            if (id >= 1 && id <= 4)
            {
                _answers[id - 1] = _answers[id - 1] + 1;
            }

            var message = _sentAnswerProducer.CreateTextMessage((new AnswerSentMessage(id)).ToString());
            _sentAnswerProducer.Send(message);
        }

        [HttpGet("answers")]
        public int[] GetAnswers()
        {
            return _answers;
        }

        [HttpDelete("answers")]
        public void DeleteAnswers()
        {
            _answers[0] = 0;
            _answers[1] = 0;
            _answers[2] = 0;
            _answers[3] = 0;
        }

        [HttpGet("result")]
        public int GetResult()
        {
            return _rightAnswerId;
        }
    }
}
