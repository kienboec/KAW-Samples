using System;

namespace QuestionService.Common
{
    [Serializable]
    public class AnswerSentMessage
    {
        public AnswerSentMessage()
        {
            
        }

        public AnswerSentMessage(int answerIndex)
        {
            this.AnswerIndex = answerIndex;
        }

        public AnswerSentMessage(string text)
        {
            if (text.StartsWith("answer: "))
            {
                AnswerIndex = int.Parse(text.Substring(8));
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public int AnswerIndex { get; set; }

        public override string ToString()
        {
            return $"answer: {AnswerIndex}";
        }
    }
}
