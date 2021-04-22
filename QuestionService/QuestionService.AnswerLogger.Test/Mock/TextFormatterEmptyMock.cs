using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionService.AnswerLogger.Test.Mock
{
    public class TextFormatterEmptyMock : ITextFormatter
    {
        public string Format()
        {
            return string.Empty;
        }
    }
}
