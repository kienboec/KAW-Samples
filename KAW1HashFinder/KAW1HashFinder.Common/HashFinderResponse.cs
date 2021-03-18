using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAW1HashFinder.Common
{
    [Serializable]
    public class HashFinderResponse
    {
        public string FoundPin { get; set; }
        public string HashToFind { get; set; }
    }
}
