using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuestionService.Common;

namespace QuestionService.AnswerLogger
{
    public class AnswerSentTextFormatter : ITextFormatter
    {
        private readonly AnswerSentMessage _message;

        public AnswerSentTextFormatter(AnswerSentMessage message)
        {
            _message = message;
        }

        public string Format()
        {
            var outputNumberString = "Error";
            if (_message != null)
            {
                if (_message.AnswerIndex >= 1 && _message.AnswerIndex <= 4)
                {
                    outputNumberString = _message.AnswerIndex.ToString();
                }
            }

            return $"Answer received: {outputNumberString}";
        }
    }
}
