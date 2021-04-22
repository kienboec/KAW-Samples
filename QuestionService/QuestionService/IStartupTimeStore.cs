using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestionService
{
    public interface IStartupTimeStore
    {
        DateTime StartupTime { get; set; }
        void Initialize();
    }
}
