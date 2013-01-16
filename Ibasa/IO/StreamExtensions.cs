using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ibasa.IO
{
    public static class StreamExtensions
    {
        public static byte[] ReadBytes(this System.IO.Stream stream, long count)
        {
            var buffer = new byte[count];
            ReadBytes(stream, buffer, 0, count);
            return buffer;
        }

        public static void ReadBytes(this System.IO.Stream stream, byte[] buffer, int offset, long count)
        {
            long bytes_left = count;

            do
            {
                int read = stream.Read(buffer, offset, (int)bytes_left);
                if (read == 0)
                    break;
                offset += read;
                bytes_left -= read;
            } while (bytes_left > 0);

            if (bytes_left != 0)
                throw new System.IO.EndOfStreamException(string.Format("Could not read {0} bytes.", count));
        }
    }
}
