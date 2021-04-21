using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuestionService.Database;

namespace QuestionService.Model
{
    public class Question
    {
        public Question()
        {
            
        }

        public Question(DatabaseQuestion dbQuestion)
        {
            this.Text = dbQuestion.Text;
            this.Answer1 = dbQuestion.Answer1;
            this.Answer2 = dbQuestion.Answer2;
            this.Answer3 = dbQuestion.Answer3;
            this.Answer4 = dbQuestion.Answer4;
        }

        public string Text { get; set; }

        public string Answer1 { get; set; }
        public string Answer2 { get; set; }
        public string Answer3 { get; set; }
        public string Answer4 { get; set; }
    }
}
