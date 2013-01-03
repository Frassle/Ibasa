using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ibasa.Media.Riff
{
    public sealed class RiffWriter
    {
        private sealed class RiffStream : System.IO.Stream
        {
            System.IO.Stream BaseStream;
            long Offset;
            long _Length;

            public RiffStream(System.IO.Stream input, long offset)
            {
                BaseStream = input;
                Offset = offset;
                _Length = 0;
            }

            public override bool CanRead
            {
                get { return false; }
            }

            public override bool CanSeek
            {
                get { return BaseStream.CanSeek; }
            }

            public override bool CanWrite
            {
                get { return true; }
            }

            public override void Flush()
            {
                BaseStream.Flush();
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
                    return BaseStream.Position - Offset;
                }
                set
                {
                    Seek(value, System.IO.SeekOrigin.Begin);
                }
            }

            public override int Read(byte[] buffer, int offset, int count)
            {
                throw new NotSupportedException();
            }

            public override long Seek(long offset, System.IO.SeekOrigin origin)
            {
                if (origin == System.IO.SeekOrigin.Begin)
                {
                    offset = BaseStream.Seek(Offset + offset, System.IO.SeekOrigin.Begin);
                }
                else if (origin == System.IO.SeekOrigin.Current)
                {
                    offset = BaseStream.Seek(offset, System.IO.SeekOrigin.Current);
                }
                else if (origin == System.IO.SeekOrigin.End)
                {
                    offset = BaseStream.Seek(Offset + Length - offset, System.IO.SeekOrigin.Begin);
                }

                return offset + Offset;
            }

            public override void SetLength(long value)
            {
                throw new NotSupportedException();
            }

            public override void Write(byte[] buffer, int offset, int count)
            {
                count = (int)Math.Min(count, Position - _Length);

                BaseStream.Write(buffer, offset, count);
            }
        }

        Ibasa.IO.SeekableStream BaseStream;

        Stack<long> Offsets = new Stack<long>();

        public RiffWriter(System.IO.Stream output)
        {
            BaseStream = new Ibasa.IO.SeekableStream(output);
        }

        static long PadByte(long position)
        {
            return position % 2 == 0 ? 0 : 1;
        }

        public void WriteStartDocument(FourCC type)
        {
            BaseStream.Write(BitConverter.GetBytes((int)new FourCC("RIFF")), 0, 4);
            Offsets.Push(BaseStream.Position);
            BaseStream.Position += 4;
            BaseStream.Write(BitConverter.GetBytes((int)type), 0, 4);
        }

        public void WriteEndDocument()
        {
            var offset = Offsets.Pop();
            var position = BaseStream.Position;
            var length = (offset + 4) - position;

            BaseStream.Write(BitConverter.GetBytes(length), 0, 4);

            BaseStream.Position = position + PadByte(position);
            BaseStream.Commit();
        }

        public void WriteStartList(FourCC type)
        {
            BaseStream.Write(BitConverter.GetBytes((int)new FourCC("LIST")), 0, 4);
            Offsets.Push(BaseStream.Position);
            BaseStream.Position += 4;
            BaseStream.Write(BitConverter.GetBytes((int)type), 0, 4);
        }

        public void WriteEndList()
        {
            var offset = Offsets.Pop();
            var position = BaseStream.Position;
            var length = (offset + 4) - position;

            BaseStream.Write(BitConverter.GetBytes(length), 0, 4);

            BaseStream.Position = position + PadByte(position);
        }

        public void WriteStartChunk(FourCC id)
        {
            BaseStream.Write(BitConverter.GetBytes((int)id), 0, 4);
            Offsets.Push(BaseStream.Position);
            BaseStream.Position += 4;

            Data = new RiffStream(BaseStream, BaseStream.Position);
        }

        public void WriteEndChunk()
        {
            Data.Close();
            Data = null;

            var offset = Offsets.Pop();
            var position = BaseStream.Position;
            var length = (offset + 4) - position;

            BaseStream.Write(BitConverter.GetBytes(length), 0, 4);

            BaseStream.Position = position + PadByte(position);
        }

        public System.IO.Stream Data
        {
            get;
            private set;
        }
    }
}
