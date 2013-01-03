using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ibasa.IO
{
    public sealed class SeekableStream : System.IO.Stream
    {
        public SeekableStream(System.IO.Stream stream, bool autoCommit = false, bool force = false)
        {
            BaseStream = stream;
            AutoCommit = autoCommit;
            if (!BaseStream.CanSeek || force)
            {
                Buffer = new System.IO.MemoryStream();
                DiscardPosition = BaseStream.Position;
            }
        }

        private System.IO.MemoryStream Buffer;
        bool Dirty = false;
        bool AutoCommit;

        public System.IO.Stream BaseStream
        {
            get;
            private set;
        }

        public long CommitPosition
        {
            get { return Buffer == null ? 0 : BaseStream.Position; }
        }

        public long DiscardPosition
        {
            get;
            private set;
        }

        public override bool CanRead
        {
            get { return BaseStream.CanRead; }
        }

        public override bool CanSeek
        {
            get { return true; }
        }

        public override bool CanWrite
        {
            get { return BaseStream.CanWrite; }
        }

        public void Commit()
        {
            if(Dirty)
            {
                var position = Position;
                Position = CommitPosition;
                Buffer.CopyTo(BaseStream);
                Position = position;

                Dirty = false;
            }
        }

        public void Discard()
        {
            if (Buffer != null)
            {
                Commit();
                DiscardPosition = CommitPosition;
                Buffer.SetLength(0);
            }
        }

        public override void Flush()
        {
            Commit();
            BaseStream.Flush();
        }

        public override long Length
        {
            get
            {
                if (Buffer == null)
                {
                    return BaseStream.Length;
                }
                else
                {
                    throw new NotSupportedException("Cannot get stream length.");
                }
            }
        }

        private long BufferLength
        {
            get
            {
                return Buffer.Length + DiscardPosition;
            }
        }

        public override long Position
        {
            get
            {
                if (Buffer == null)
                {
                    return BaseStream.Position;
                }
                else
                {
                    return DiscardPosition + Buffer.Position;
                }
            }
            set
            {
                Seek(value, System.IO.SeekOrigin.Begin);
            }
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            if (Buffer == null)
            {
                return BaseStream.Read(buffer, offset, count);
            }
            else
            {
                int buffered = (int)Math.Min(count, BufferLength - Position);

                int read = Buffer.Read(buffer, offset, buffered);

                if (read == buffered && read != count && (!Dirty || AutoCommit))
                {
                    Commit();

                    int streamed = count - read;
                    read += BaseStream.Read(buffer, offset + read, streamed);
                }

                return read;
            }
        }

        public override long Seek(long offset, System.IO.SeekOrigin origin)
        {
            if (Buffer == null)
            {
                return BaseStream.Seek(offset, origin);
            }
            else
            {
                if (origin == System.IO.SeekOrigin.Begin)
                {
                    if (offset < DiscardPosition)
                    {
                        throw new ArgumentException("Cannot seek to before discard position.", "offset");
                    }

                    Buffer.Position = offset - DiscardPosition;
                }
                else if (origin == System.IO.SeekOrigin.Current)
                {
                    Seek(Position + offset, System.IO.SeekOrigin.Begin);
                }
                else if (origin == System.IO.SeekOrigin.End)
                {
                    throw new ArgumentException("Cannot seek relative to end of stream.", "origin");
                }

                return Buffer.Position;
            }
        }

        public override void SetLength(long value)
        {
            if (Buffer == null)
            {
                BaseStream.SetLength(value);
            }
            else
            {
                throw new NotSupportedException("Cannot set stream length.");
            }
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            if (Buffer == null)
            {
                BaseStream.Write(buffer, offset, count);
            }
            else
            {
                if (Position < CommitPosition)
                {
                    throw new InvalidOperationException("Cannot write before commit position.");
                }

                Buffer.Write(buffer, offset, count);
                Dirty = true;
            }
        }
    }
}
