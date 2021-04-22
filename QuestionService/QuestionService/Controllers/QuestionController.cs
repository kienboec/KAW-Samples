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
using QuestionService.NetworkCommunication;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QuestionService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        public IStartupTimeStore Store { get; }

        private static IDatabaseHandler _db = null;
        private static IServiceCommunication _srvCom = null;

        public QuestionController(IStartupTimeStore store, IDatabaseHandler db, IServiceCommunication srvCom)
        {
            Store = store;
            _db = db;
            _srvCom = srvCom;

            if (_question == null)
            {
                var dbQuestion = _db.GetQuestion();
                _question = new Question(dbQuestion);
                _rightAnswerId = dbQuestion.RightAnswerIndex;
            }
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

            _srvCom.SendAnswerSentMessage(id);
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
