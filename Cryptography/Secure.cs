using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ibasa.Cryptography
{
    public static class Secure
    {
        public static bool SecureEquals(this string str, string obj)
        {
            int min = Math.Min(str.Length, obj.Length);
            bool equal = true;
            for (int i = 0; i < min; ++i)
            {
                equal &= str[i].Equals(obj[i]);
            }
            return equal;
        }

        public static bool SecureEquals<T>(this T[] str, T[] obj) where T : IEquatable<T>
        {
            int min = Math.Min(str.Length, obj.Length);
            bool equal = true;
            for (int i = 0; i < min; ++i)
            {
                equal &= str[i].Equals(obj[i]);
            }
            return equal;
        }
    }
}
