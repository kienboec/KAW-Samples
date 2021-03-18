using System;

namespace KAW2HashFinder.Common
{
    [Serializable]
    public class RequestMessage
    {
        public string PinToCalculate { get; set; }
        public string ResultHash { get; set; }
    }
}
