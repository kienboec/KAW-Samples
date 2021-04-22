using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestionService.NetworkCommunication
{
    public interface IServiceCommunication
    {
        void SendAnswerSentMessage(in int id);
    }
}
