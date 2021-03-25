using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FancyNumbersGame.UI.Communication
{
    public interface IComHandler
    {
        void CallGenerateNewNumber();
        void CallSendGuess(string username, int guess);
        string CallCalculateWinner();
    }
}
