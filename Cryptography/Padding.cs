using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ibasa.Cryptography
{
    delegate void PaddingFunction(ArraySegment<byte> data, int padding);

    public static class Padding
    {
        public static void PCKS7(ArraySegment<byte> data, int padding)
        {
            byte pad = (byte)padding;

            for (int i = data.Count - padding; i < data.Count; ++i)
            {
                data[i] = pad;
            }
        }

        public static void Zeros(ArraySegment<byte> data, int padding)
        {
            for (int i = data.Count - padding; i < data.Count; ++i)
            {
                data[i] = 0;
            }
        }

        public static void OAEP(ArraySegment<byte> data, int padding)
        {
            for (int i = data.Count - padding; i < data.Count; ++i)
            {
                data[i] = 0;
            }
        }
    }
}
