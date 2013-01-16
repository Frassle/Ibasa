using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace Ibasa.Media.Riff
{
    public sealed class RiffReader
    {
        private sealed class RiffStream : System.IO.Stream
        {
            System.IO.Stream Stream;
            long Offset;
            long _Length;

            public RiffStream(System.IO.Stream input, long offset, long length)
            {
                Stream = input;
                Offset = offset;
                _Length = length;
            }

            public override bool CanRead
            {
                get { return true; }
            }

            public override bool CanSeek
            {
                get { return Stream.CanSeek; }
            }

            public override bool CanWrite
            {
                get { return false; }
            }

            public override void Flush()
            {
            }

            public override long Length
            {
                get
                {
                    return _Length;
                }
            }

            public override long Position
            {
                get
                {
                    return Stream.Position - Offset;
                }
                set
                {
                    Seek(value, System.IO.SeekOrigin.Begin);
                }
            }

            public override int Read(byte[] buffer, int offset, int count)
            {
                count = (int)Math.Min(count, Length - Position);

                var read = Stream.Read(buffer, offset, count);

                return read;
            }

            public override long Seek(long offset, System.IO.SeekOrigin origin)
            {
                if (origin == System.IO.SeekOrigin.Begin)
                {
                    offset = Stream.Seek(Offset + offset, System.IO.SeekOrigin.Begin);
                }
                else if (origin == System.IO.SeekOrigin.Current)
                {
                    offset = Stream.Seek(offset, System.IO.SeekOrigin.Current);
                }
                else if (origin == System.IO.SeekOrigin.End)
                {
                    offset = Stream.Seek(Offset + Length - offset, System.IO.SeekOrigin.Begin);
                }

                return offset - Offset;
            }

            public override void SetLength(long value)
            {
                throw new NotSupportedException();
            }

            public override void Write(byte[] buffer, int offset, int count)
            {
                throw new NotSupportedException();
            }
        }

        System.IO.Stream BaseStream;

        public RiffReader(System.IO.Stream input)
        {
            Contract.Requires(input.CanSeek);
            Contract.Requires(input.CanRead);

            BaseStream = input;
        }

        public FourCC Id { get; private set; }
        public FourCC? Type { get; private set; }

        public long Length { get { return Lengths.Peek(); } }
        long Offset { get { return Offsets.Peek(); } }

        public int Depth { get { return Lengths.Count; } }

        Stack<long> Lengths = new Stack<long>();
        Stack<long> Offsets = new Stack<long>();
        byte[] Buffer = new byte[4];

        public System.IO.Stream Data { get; private set; }

        static long PadByte(long position)
        {
            return position % 2 == 0 ? 0 : 1;
        }

        public bool Read()
        {
            if (Lengths.Count != 0)
            {
                if (!Type.HasValue)
                {
                    var length = Lengths.Pop();
                    var offset = Offsets.Pop();

                    var position = offset + length;
                    BaseStream.Position = position + PadByte(position);
                }

                if (BaseStream.Position == Offset + Length)
                {
                    var length = Lengths.Pop();
                    var offset = Offsets.Pop();

                    var position = offset + length;
                    BaseStream.Position = position + PadByte(position);
                }

                if (Lengths.Count == 0)
                {
                    return false;
                }
            }

            if(BaseStream.Read(Buffer, 0, 4) != 4)
            {
                return false;
            }
            Id = new FourCC(Buffer);

            if (BaseStream.Read(Buffer, 0, 4) != 4)
            {
                return false;
            }
            Lengths.Push(BitConverter.ToUInt32(Buffer, 0));

            Offsets.Push(BaseStream.Position);

            if (Id == "RIFF" || Id == "LIST")
            {
                if (BaseStream.Read(Buffer, 0, 4) != 4)
                {
                    return false;
                }
                Type = new FourCC(Buffer);
                Data = null;
            }
            else
            {
                Type = null;
                Data = new RiffStream(BaseStream, Offset, Length);
            }

            return true;
        }
    }
}
