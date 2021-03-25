using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FancyNumbersGame.ActiveMQ.Common
{
    [Serializable]
    public class SendGuessMessage
    {
        public string Username { get; set; }
        public int Guess{ get; set; }
    }
}
