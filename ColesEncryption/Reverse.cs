using System;
using System.Collections.Generic;

namespace ColesEncryption
{
    static class Reverse
    {
        public static string GetReversed(string _string)
        {
            // The final string value after it has been reversed
            string finalStr = "";
            // Reverse index (from end to start)
            for(int i = _string.Length - 1; i >= 0; i--)
            {
                finalStr += _string[i];
            }
            return finalStr;
        }
    }
}
