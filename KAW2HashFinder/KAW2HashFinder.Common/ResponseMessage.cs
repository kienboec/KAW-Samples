using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAW2HashFinder.Common
{
    [Serializable]
    public class ResponseMessage
    {
        public string ResultPin { get; set; }
        public string ResultHash { get; set; }
    }
}
