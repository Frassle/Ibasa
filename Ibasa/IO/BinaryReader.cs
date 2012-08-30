using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;
using System.IO;

namespace Ibasa.IO
{
    /// <summary>
    /// Reads primitive data types as binary values in a specific encoding.
    /// </summary>
    public class BinaryReader : IDisposable
    {
        /// <summary>
        /// Exposes access to the underlying stream of the BinaryReader.
        /// </summary>
        /// <returns>The underlying stream associated with the BinaryReader.</returns>
        public Stream BaseStream { get; private set; }
        /// <summary>
        /// The Encoding used by the BinaryReader.
        /// </summary>
        public Encoding Encoding { get; private set; }
        /// <summary>
        /// True if this reader owns the underlying stream.
        /// </summary>
        readonly protected bool OwnsStream;
        /// <summary>
        /// Internal working buffer.
        /// </summary>
        protected byte[] Buffer;

        protected void FillBuffer(int count)
        {
            Contract.Requires(0 <= count);
            Contract.Requires(count <= Buffer.Length);

            if (ReadBytes(Buffer, 0, count) != count)
                throw new EndOfStreamException();
        }

        /// <summary>
        /// Initializes a new instance of the BinaryReader class based on the
        /// supplied stream, using System.Text.UTF8Encoding, owning the input stream.
        /// </summary>
        /// <param name="input">A stream.</param>
        public BinaryReader(Stream input) :
            this(input, Encoding.UTF8, true)
        {
            Contract.Requires(input != null);
            Contract.Requires(input.CanRead);
        }
        /// <summary>
        /// Initializes a new instance of the BinaryReader class based on the
        /// supplied stream and a specific character encoding, owning the input stream.
        /// </summary>
        /// <param name="input">A stream.</param>
        /// <param name="encoding">The character encoding.</param>
        public BinaryReader(Stream input, Encoding encoding) :
            this(input, encoding, true)
        {
            Contract.Requires(input != null);
            Contract.Requires(encoding != null);
            Contract.Requires(input.CanRead);
        }
        /// <summary>
        /// Initializes a new instance of the BinaryReader class based on the
        /// supplied stream and a specific character encoding, optionally owning the input stream.
        /// </summary>
        /// <param name="input">A stream.</param>
        /// <param name="encoding">The character encoding.</param>
        /// <param name="ownsStream">Whether this BinaryReader should take ownership of the stream.</param>
        public BinaryReader(Stream input, Encoding encoding, bool ownsStream)
        {
            Contract.Requires(input != null);
            Contract.Requires(encoding != null);
            Contract.Requires(input.CanRead);

            BaseStream = input;
            Encoding = encoding;
            OwnsStream = ownsStream;
            Buffer = new byte[Math.Max(16, Encoding.GetMaxByteCount(1))];
        }

        /// <summary>
        /// Implicitly casts a <see cref="System.IO.BinaryReader"/> to an <see cref="Ibasa.IO.BinaryReader"/>.
        /// </summary>
        /// <param name="reader">A <see cref="System.IO.BinaryReader"/></param>
        /// <returns>An <see cref="Ibasa.IO.BinaryReader"/> using <see cref="System.Text.UTF8Encoding"/>.</returns>
        public static implicit operator BinaryReader(System.IO.BinaryReader reader)
        {
            return new BinaryReader(reader.BaseStream, Encoding.UTF8, false);
        }

        #region Dispose
        #region Dispose helper code
        /// <summary>
        /// Call this before every method that requires resources.
        /// </summary>
        void ObjectDisposedException()
        {
            if (IsDisposed)
                throw new ObjectDisposedException(GetType().FullName, string.Format("The {0} has been disposed.", GetType().FullName));
        }

        /// <summary>
        /// Occurs when Dispose is called.
        /// </summary>
        public event global::System.EventHandler Disposing;

#if DEBUG
        /// <summary>
        /// StackTrace from construction.
        /// </summary>
        private readonly global::System.Diagnostics.StackTrace Trace = new global::System.Diagnostics.StackTrace(true);
#endif

        /// <summary>
        /// Gets a value that indicates whether the object is disposed. 
        /// </summary>
        public bool IsDisposed { get; private set; }

        /// <summary>
        /// Object finalizer, writes debug message if called.
        /// </summary>
        ~BinaryReader()
        {
#if DEBUG
            string message = "!! Forgot to dispose a " + GetType().FullName;
            message += "\n\nStack at construction:\n\n" + Trace + "!!";
            global::System.Diagnostics.Debug.WriteLine(message);
#else

#endif
        }

        /// <summary>
        /// Immediately releases the resources used by this object.
        /// </summary>
        public void Close()
        {
            if (!IsDisposed)
            {
                try
                {
                    global::System.EventHandler handler = Disposing;
                    if (handler != null)
                    {
                        handler(this, null);
                    }
                    OnDispose();
                    IsDisposed = true;
                }
                finally
                {
                    global::System.GC.SuppressFinalize(this);
                }
            }
        }
        void IDisposable.Dispose()
        {
            Close();
        }
        #endregion
        /// <summary>
        /// Immediately releases the resources used by this object. 
        /// </summary>
        void OnDispose()
        {
            if (OwnsStream)
                BaseStream.Dispose();
        }
        #endregion

        /// <summary>
        /// Gets or sets the position within the base stream.
        /// </summary>
        public long Position
        {
            get { return BaseStream.Position; }
            set
            {
                Contract.Requires(0 <= value);
                BaseStream.Position = value;
            }
        }
        /// <summary>
        /// Gets the length in bytes of the base stream.
        /// </summary>
        public long Length
        {
            get { return BaseStream.Length; }
        }
        /// <summary>
        /// Sets the length of the base stream.
        /// </summary>
        /// <param name="value">The desired length of the base stream in bytes.</param>
        public void SetLength(long value)
        {
            Contract.Requires(0 <= value);

            BaseStream.SetLength(value);
        }
        /// <summary>
        /// Sets the position within the base stream.
        /// </summary>
        /// <param name="offset">A byte offset relative to the origin parameter.</param>
        /// <param name="origin">
        /// A value of type SeekOrigin indicating the reference point used
        /// to obtain the new position.
        /// </param>
        /// <returns>The new position within the current stream.</returns>
        public long Seek(long offset, SeekOrigin origin)
        {
            return BaseStream.Seek(offset, origin);
        }

        /// <summary>
        /// Sets the size of the internal buffer.
        /// </summary>
        /// <param name="bytes">The number of bytes in the buffer, must be at least 16.</param>
        public void SetBufferSize(int bytes)
        {
            Contract.Requires(bytes >= 16);
            Buffer = bytes == Buffer.Length ? Buffer : new byte[bytes];
        }

        public int Read(byte[] buffer, int index, int count)
        {
            Contract.Requires(buffer != null);
            Contract.Requires(0 <= index && index <= buffer.Length);
            Contract.Requires(0 <= count);
            Contract.Requires(index + count <= buffer.Length);
            Contract.Ensures(0 <= Contract.Result<int>());
            Contract.Ensures(Contract.Result<int>() <= count);

            return BaseStream.Read(buffer, index, count);
        }

        public byte[] ReadBytes(int count)
        {
            Contract.Requires(0 <= count);
            Contract.Ensures(Contract.Result<byte[]>() != null);
            Contract.Ensures(Contract.Result<byte[]>().Length <= count);

            byte[] bytes = new byte[count];
            count = ReadBytes(bytes, 0, count);
            Array.Resize<byte>(ref bytes, count);
            return bytes;
        }
        public int ReadBytes(byte[] buffer, int index, int count)
        {
            Contract.Requires(buffer != null);
            Contract.Requires(0 <= index && index <= buffer.Length);
            Contract.Requires(0 <= count);
            Contract.Requires(index + count <= buffer.Length);
            Contract.Ensures(0 <= Contract.Result<int>());
            Contract.Ensures(Contract.Result<int>() <= count);

            int offset = index;
            do
            {
                int read = BaseStream.Read(buffer, offset, count);
                if (read == 0)
                    break;
                offset += read;
                count -= read;
            } while (count > 0);
            return offset - index;
        }
       
        public char[] ReadChars(int count)
        {
            Contract.Requires(0 <= count);
            Contract.Ensures(Contract.Result<char[]>() != null);
            Contract.Ensures(Contract.Result<char[]>().Length <= count);

            char[] chars = new char[count];
            count = ReadChars(chars, 0, count);
            Array.Resize<char>(ref chars, count);
            return chars;
        }
        public int ReadChars(char[] chars, int index, int count)
        {
            Contract.Requires(chars != null);
            Contract.Requires(0 <= index && index <= chars.Length);
            Contract.Requires(0 <= count);
            Contract.Requires(index + count <= chars.Length);
            Contract.Ensures(0 <= Contract.Result<int>());
            Contract.Ensures(Contract.Result<int>() <= count);

            int charCount = 0;
            int byteCount = 0;

            Decoder decoder = Encoding.GetDecoder();
            bool flush = false;            
            while (charCount < count && !flush)
            {
                bool completed;
                int bytesUsed, charsUsed;

                int br = BaseStream.ReadByte();
                if (br == -1)
                    flush = true;
                else
                    Buffer[byteCount++] = (byte)br;

                decoder.Convert(
                    Buffer, 0, byteCount,
                    chars, index + charCount, count - charCount,
                    flush, out bytesUsed, out charsUsed, out completed);

                charCount += charsUsed;
                Array.Copy(Buffer, bytesUsed, Buffer, 0, byteCount - bytesUsed);
                byteCount -= bytesUsed;
            }

            return charCount;
        }

        public int PeekByte()
        {
            if (!BaseStream.CanSeek)
                return -1;

            long position = BaseStream.Position;
            int peek = BaseStream.ReadByte();
            BaseStream.Position = position;
            return peek;
        }
        public int PeekChar()
        {
            if (!BaseStream.CanSeek)
                return -1;

            try
            {
                long position = BaseStream.Position;
                int peek = ReadChar();
                BaseStream.Position = position;
                return peek;
            }
            catch (EndOfStreamException)
            {
                return -1;
            }
        }

        public string ReadString()
        {
            Contract.Ensures(Contract.Result<string>() != null);

            int length = (int)ReadVariableUInt32();
            byte[] bytes = ReadBytes(length);
            if (bytes.Length != length)
                throw new EndOfStreamException();
            return Encoding.GetString(bytes);
        }
        public decimal ReadDecimal()
        {
            FillBuffer(16);

            return BitConverter.ToDecimal(Buffer, 0);
        }
        public double ReadDouble()
        {
            FillBuffer(8);

            return BitConverter.ToDouble(Buffer, 0);
        }
        public float ReadSingle()
        {
            FillBuffer(4);

            return BitConverter.ToSingle(Buffer, 0);
        }
        public bool ReadBoolean()
        {
            return ReadByte() != 0;
        }
        public byte ReadByte()
        {
            int b = BaseStream.ReadByte();
            if (b == -1)
                throw new EndOfStreamException("The end of the stream is reached.");
            return (byte)b;
        }
        public char ReadChar()
        {
            char[] chars = ReadChars(1);
            if (chars.Length == 0)
                throw new EndOfStreamException();
            return chars[0];
        }
        public short ReadInt16()
        {
            FillBuffer(2);

            return BitConverter.ToInt16(Buffer, 0);
        }
        public int ReadInt32()
        {
            FillBuffer(4);
            
            return BitConverter.ToInt32(Buffer, 0);
        }
        public long ReadInt64()
        {
            FillBuffer(8);

            return BitConverter.ToInt64(Buffer, 0);
        }
        [CLSCompliant(false)]
        public sbyte ReadSByte()
        {
            int b = BaseStream.ReadByte();
            if (b == -1)
                throw new EndOfStreamException("The end of the stream is reached.");
            return (sbyte)(byte)b;
        }
        [CLSCompliant(false)]
        public ushort ReadUInt16()
        {
            FillBuffer(2);
            
            return BitConverter.ToUInt16(Buffer, 0);
        }
        [CLSCompliant(false)]
        public uint ReadUInt32()
        {
            FillBuffer(4);
            
            return BitConverter.ToUInt32(Buffer, 0);
        }
        [CLSCompliant(false)]
        public ulong ReadUInt64()
        {
            FillBuffer(8);
            
            return BitConverter.ToUInt64(Buffer, 0);
        }

				public short ReadVariableInt16()
				{
					ushort value = ReadVariableUInt16();
					return (short)((int)(value >> 1) ^ -(int)(value & 1));
				}
				public int ReadVariableInt32()
				{
					uint value = ReadVariableUInt32();
					return (int)(value >> 1) ^ -(int)(value & 1);
				}
				public long ReadVariableInt64()
				{
					ulong value = ReadVariableUInt64();
					return (long)(value >> 1) ^ -(long)(value & 1);
				}

				[CLSCompliant(false)]
				public ushort ReadVariableUInt16()
				{
					byte b;
					uint result = 0;
					int shift = 0;
					do
					{
						if(shift == 21)
							throw new FormatException("The stream is corrupted.");

						b = ReadByte();
						result |= (uint)(b & 0x7f) << shift;
						shift += 7;
					} while((b & 0x80) != 0);
					return (ushort)result;
				}

				[CLSCompliant(false)]
				public uint ReadVariableUInt32()
				{
					byte b;
					uint result = 0;
					int shift = 0;
					do
					{
						if(shift == 35)
							throw new FormatException("The stream is corrupted.");

						b = ReadByte();
						result |= (uint)(b & 0x7f) << shift;
						shift += 7;
					} while((b & 0x80) != 0);
					return result;
				}

				[CLSCompliant(false)]
				public ulong ReadVariableUInt64()
				{
					byte b;
					ulong result = 0;
					int shift = 0;
					do
					{
						if(shift == 70)
							throw new FormatException("The stream is corrupted.");

						b = ReadByte();
						result |= (ulong)(b & 0x7f) << shift;
						shift += 7;
					} while((b & 0x80) != 0);
					return result;
				}

        /// <summary>
        /// Reads a structure of type T.
        /// </summary>
        /// <typeparam name="T">The type of structure.</typeparam>
        /// <exception cref="System.IO.EndOfStreamException">There are not enough bytes left to read in a structure of type T</exception>
        public T Read<T>() where T : struct
        {
            int size = System.Runtime.InteropServices.Marshal.SizeOf(typeof(T));
            byte[] buffer = size <= Buffer.Length ? Buffer : new byte[size];
            int read = Read(buffer, 0, size);
            if (read != size)
                throw new EndOfStreamException(string.Format("There are not enough bytes left to read in a structure of type {0}", typeof(T).Name));

            T structure = new T();
            System.Runtime.InteropServices.GCHandle handle = System.Runtime.InteropServices.GCHandle.Alloc(structure, System.Runtime.InteropServices.GCHandleType.Pinned);
            System.Runtime.InteropServices.Marshal.Copy(buffer, 0, handle.AddrOfPinnedObject(), size);
            handle.Free();

            return structure;
        }

        /// <summary>
        /// Reads structures from the reader into an array.
        /// </summary>
        /// <typeparam name="T">The type of structure.</typeparam>
        /// <param name="array">The array segment to contain the structures read from the stream.</param>
        /// <exception cref="System.IO.EndOfStreamException">There are not enough bytes left to read in a structure of type T</exception>
        /// <exception cref="System.ArgumentNullException">array is null.</exception>
        public void Read<T>(ArraySegment<T> array) where T : struct
        {
            if (array == null)
                throw new ArgumentNullException("array is null.");

            int size = System.Runtime.InteropServices.Marshal.SizeOf(typeof(T));
            byte[] buffer = size <= Buffer.Length ? Buffer : new byte[size];
            System.Runtime.InteropServices.GCHandle handle = System.Runtime.InteropServices.GCHandle.Alloc(array.Array, System.Runtime.InteropServices.GCHandleType.Pinned);
            IntPtr target = handle.AddrOfPinnedObject();
            for (int i = 0; i < array.Count; ++i)
            {
                int read = Read(buffer, 0, size);
                if (read != size)
                    throw new EndOfStreamException(string.Format("There are not enough bytes left to read in a structure of type {0}", typeof(T).Name));

                System.Runtime.InteropServices.Marshal.Copy(buffer, 0, target, size);
                target += size;
            }
            handle.Free();
        }

        /// <summary>
        /// Reads structures from the reader into an array.
        /// </summary>
        /// <typeparam name="T">The type of structure.</typeparam>
        /// <param name="count">The number of structures to read from the stream.</param>
        /// <returns>The array of structures read from the stream.</param>
        /// <exception cref="System.IO.EndOfStreamException">There are not enough bytes left to read in a structure of type T</exception>
        /// <exception cref="System.ArgumentNullException">array is null.</exception>
        public T[] Read<T>(int count) where T : struct
        {
            T[] array = new T[count];
            Read<T>(array);
            return array;
        }
    }
}
