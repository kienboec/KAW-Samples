using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FancyNumbersGame.UI.Communication
{
    public interface IComHandler
    {
        Task CallGenerateNewNumber();
        Task CallSendGuess(string username, int guess);
        Task<string> CallCalculateWinner();
    }
}
