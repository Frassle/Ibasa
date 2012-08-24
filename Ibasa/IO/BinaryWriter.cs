using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace Ibasa.IO
{
    /// <summary>
    /// Writes primitive types in binary to a stream and supports writing strings
    /// in a specific encoding.
    /// </summary>
    public class BinaryWriter : IDisposable
    {
        /// <summary>
        /// Exposes access to the underlying stream of the BinaryWriter.
        /// </summary>
        /// <returns>The underlying stream associated with the BinaryWriter.</returns>
        public System.IO.Stream BaseStream { get; private set; }
        /// <summary>
        /// The Encoding used by the BinaryWriter.
        /// </summary>
        public Encoding Encoding { get; private set; }
        /// <summary>
        /// True if this writer owns the underlying stream.
        /// </summary>
        protected readonly bool OwnsStream;
        /// <summary>
        /// Internal working buffer.
        /// </summary>
        protected readonly byte[] Buffer;

        /// <summary>
        /// Initializes a new instance of the BinaryWriter class based on the
        /// supplied stream, using System.Text.UTF8Encoding, owning the input stream.
        /// </summary>
        /// <param name="input">A stream.</param>
        public BinaryWriter(System.IO.Stream input) :
            this(input, Encoding.UTF8, true)
        {
            Contract.Requires(input != null);
            Contract.Requires(input.CanWrite);
        }
        /// <summary>
        /// Initializes a new instance of the BinaryWriter class based on the
        /// supplied stream and a specific character encoding, owning the input stream.
        /// </summary>
        /// <param name="input">A stream.</param>
        /// <param name="encoding">The character encoding.</param>
        public BinaryWriter(System.IO.Stream input, Encoding encoding) :
            this(input, encoding, true)
        {
            Contract.Requires(input != null);
            Contract.Requires(encoding != null);
            Contract.Requires(input.CanWrite);
        }
        /// <summary>
        /// Initializes a new instance of the BinaryWriter class based on the
        /// supplied stream and a specific character encoding, optionally owning the input stream.
        /// </summary>
        /// <param name="input">A stream.</param>
        /// <param name="encoding">The character encoding.</param>
        /// <param name="ownsStream">Whether this BinaryWriter should take ownership of the stream.</param>
        public BinaryWriter(System.IO.Stream input, Encoding encoding, bool ownsStream)
        {
            Contract.Requires(input != null);
            Contract.Requires(encoding != null);
            Contract.Requires(input.CanWrite);

            BaseStream = input;
            Encoding = encoding;
            OwnsStream = ownsStream;
            Buffer = new byte[16];
        }

        /// <summary>
        /// Implicitly casts a <see cref="System.IO.BinaryWriter"/> to an <see cref="Ibasa.IO.BinaryWriter"/>.
        /// </summary>
        /// <param name="writer">A <see cref="System.IO.BinaryWriter"/></param>
        /// <returns>An <see cref="Ibasa.IO.BinaryWriter"/> using <see cref="System.Text.UTF8Encoding"/>.</returns>
        public static implicit operator BinaryWriter(System.IO.BinaryWriter writer)
        {            
            return new BinaryWriter(writer.BaseStream, Encoding.UTF8, false);
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
        ~BinaryWriter()
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
        /// Clears all buffers for the current writer and causes any buffered data to
        /// be written to the underlying device.
        /// </summary>
        public void Flush()
        {
            BaseStream.Flush();
        }

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
        /// A value of type System.IO.SeekOrigin indicating the reference point used
        /// to obtain the new position.
        /// </param>
        /// <returns>The new position within the current stream.</returns>
        public long Seek(long offset, System.IO.SeekOrigin origin)
        {
            return BaseStream.Seek(offset, origin);
        }
        
        public void Write(byte[] buffer)
        {
            Contract.Requires(buffer != null);

            Write(buffer, 0, buffer.Length);
        }
        public void Write(byte[] buffer, int index, int count)
        {
            Contract.Requires(buffer != null);
            Contract.Requires(0 <= index); 
            Contract.Requires(index < buffer.Length);
            Contract.Requires(0 <= count);
            Contract.Requires(index + count <= buffer.Length);

            BaseStream.Write(buffer, index, count);
        }
        public void Write(char[] chars)
        {
            Contract.Requires(chars != null);

            byte[] buffer = Encoding.GetBytes(chars);
            BaseStream.Write(buffer, 0, buffer.Length);
        }
        public void Write(char[] chars, int index, int count)
        {
            Contract.Requires(chars != null);
            Contract.Requires(0 <= index);
            Contract.Requires(index < chars.Length);
            Contract.Requires(0 <= count);
            Contract.Requires(index + count <= chars.Length);

            byte[] buffer = Encoding.GetBytes(chars, index, count);
            BaseStream.Write(buffer, 0, buffer.Length);
        }
        void Write7BitEncodedInt(int value)
        {
            uint uvalue = (uint)value;
            while (uvalue >= 0x80)
            {
                BaseStream.WriteByte((byte)(uvalue | 0x80));
                uvalue >>= 7;
            }
            BaseStream.WriteByte((byte)uvalue);
        }
        public void Write(string value)
        {
            Contract.Requires(value != null);

            byte[] bytes = Encoding.GetBytes(value);
            Write7BitEncodedInt(bytes.Length);
            Write(bytes);
        }
        public virtual void Write(decimal value)
        {
            BitConverter.GetBytes(Buffer, 0, value);
            Write(Buffer, 0, 16);
        }
        public virtual void Write(double value)
        {
            BitConverter.GetBytes(Buffer, 0, value);
            Write(Buffer, 0, 8);
        }
        public virtual void Write(float value)
        {
            BitConverter.GetBytes(Buffer, 0, value);
            Write(Buffer, 0, 4);
        }
        public virtual void Write(bool value)
        {
            BaseStream.WriteByte(value ? (byte)0 : byte.MaxValue);
        }
        public virtual void Write(byte value)
        {
            BaseStream.WriteByte(value);
        }
        public virtual void Write(char value)
        {
            Write((short)value);
        }
        public virtual void Write(short value)
        {
            BitConverter.GetBytes(Buffer, 0, value);
            Write(Buffer, 0, 2);
        }
        public virtual void Write(int value)
        {
            BitConverter.GetBytes(Buffer, 0, value);
            Write(Buffer, 0, 4);
        }
        public virtual void Write(long value)
        {
            BitConverter.GetBytes(Buffer, 0, value);
            Write(Buffer, 0, 8);
        }
        [CLSCompliant(false)]
        public virtual void Write(sbyte value)
        {
            BaseStream.WriteByte((byte)value);
        }
        [CLSCompliant(false)]
        public virtual void Write(ushort value)
        {
            BitConverter.GetBytes(Buffer, 0, value);
            Write(Buffer, 0, 2);
        }
        [CLSCompliant(false)]
        public virtual void Write(uint value)
        {
            BitConverter.GetBytes(Buffer, 0, value);
            Write(Buffer, 0, 4);
        }
        [CLSCompliant(false)]
        public virtual void Write(ulong value)
        {
            BitConverter.GetBytes(Buffer, 0, value);
            Write(Buffer, 0, 8);
        }
    }
}
