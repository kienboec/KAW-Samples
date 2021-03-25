using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FancyNumbersGame.ActiveMQ.Common
{
    [Serializable]
    public class CalcWinnerResponseMessage
    {
        public string Text { get; set; }
    }
}
