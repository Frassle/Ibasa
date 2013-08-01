using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ibasa.IO
{
    public static class StreamExtensions
    {
        public static byte[] ReadBytes(this System.IO.Stream stream, int count)
        {
            var buffer = new byte[count];
            int read = ReadBytes(stream, buffer, 0, count);
            Array.Resize(ref buffer, read);
            return buffer;
        }

        public static int ReadBytes(this System.IO.Stream stream, byte[] buffer, int offset, int count)
        {
            int bytes_left = count;

            do
            {
                int read = stream.Read(buffer, offset, bytes_left);
                if (read == 0)
                    break;
                offset += read;
                bytes_left -= read;
            } while (bytes_left > 0);

            return count - bytes_left;
        }
    }
}
