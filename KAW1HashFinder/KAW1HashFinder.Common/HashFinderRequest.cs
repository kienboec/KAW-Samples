using System;

namespace KAW1HashFinder.Common
{
    [Serializable]
    public class HashFinderRequest
    {
        public string PinToCalculate { get; set; }
        public string HashToFind { get; set; }
    }
}
