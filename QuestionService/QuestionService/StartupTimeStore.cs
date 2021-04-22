using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestionService
{
    public class StartupTimeStore : IStartupTimeStore
    {
        public DateTime StartupTime { get; set; } = DateTime.MinValue;
        public void Initialize()
        {
            StartupTime = DateTime.Now;
        }
    }
}
