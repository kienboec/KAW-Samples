using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestionService.Database
{
    public interface IDatabaseHandler
    {
        void EnsureCreated();
        DatabaseQuestion GetQuestion();
    }
}
