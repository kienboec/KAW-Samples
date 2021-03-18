using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace KAW2HashFinder.Common
{
    public static class PinUtil
    {
        public static string GetMD5Hash(string pin)
        {
            var hashedPinBytes = MD5.HashData(Encoding.UTF8.GetBytes(pin));

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < hashedPinBytes.Length; i++)
            {
                builder.Append(hashedPinBytes[i].ToString("x2"));
            }

            return builder.ToString();
        }
    }
}
