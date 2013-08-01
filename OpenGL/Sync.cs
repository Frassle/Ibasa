using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Ibasa.OpenGL
{
    public struct Sync : IEquatable<Sync>
    {
        public static readonly Sync Null = new Sync();

        public IntPtr Handle { get; internal set; }

        public Sync(IntPtr handle)
            : this()
        {
            unsafe
            {
                if (Gl.IsSync(Handle.ToPointer()) == 0)
                    throw new ArgumentException("handle is not an OpenGL sync object.", "handle");
                Handle = handle;
            }
        }

        public void Delete()
        {
            unsafe
            {
                GlHelper.ThrowNullException(Handle);
                Gl.DeleteSync(Handle.ToPointer());
                Handle = IntPtr.Zero;
                GlHelper.GetError();
            }
        }

        public string Label
        {
            get
            {
                GlHelper.ThrowNullException(Handle);
                unsafe
                {
                    int length;
                    Gl.GetObjectPtrLabel(Handle.ToPointer(), 0, &length, null);
                    byte* str = stackalloc byte[length];
                    Gl.GetObjectPtrLabel(Handle.ToPointer(), length, null, str);
                    return length == 0
                        ? string.Empty
                        : new string((sbyte*)str, 0, length, Encoding.ASCII);
                }
            }
            set
            {
                GlHelper.ThrowNullException(Handle);
                unsafe
                {
                    int length = Encoding.ASCII.GetByteCount(value);
                    byte* str = stackalloc byte[length];

                    fixed (char* source_ptr = value)
                    {
                        Encoding.ASCII.GetBytes(source_ptr, value.Length, str, length);
                    }

                    Gl.ObjectPtrLabel(Handle.ToPointer(), length, str);
                    GlHelper.GetError();
                }
            }
        }

        public bool Signaled
        {
            get
            {
                unsafe
                {
                    int value;
                    Gl.GetSynciv(Handle.ToPointer(), Gl.SYNC_STATUS, 1, null, &value);
                    return value == Gl.SIGNALED;
                }
            }
        }

        public override int GetHashCode()
        {
            GlHelper.ThrowNullException(Handle);
            return Handle.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            GlHelper.ThrowNullException(Handle);
            if (obj is Sync)
            {
                return Equals((Sync)obj);
            }
            return false;
        }

        public bool Equals(Sync other)
        {
            GlHelper.ThrowNullException(Handle);
            return Handle == other.Handle;
        }

        public static bool operator ==(Sync left, Sync right)
        {
            return left.Handle == right.Handle;
        }

        public static bool operator !=(Sync left, Sync right)
        {
            return left.Handle != right.Handle;
        }

        public override string ToString()
        {
            GlHelper.ThrowNullException(Handle);
            return string.Format("Sync: {0}", Handle.ToString());
        }
    }

    public static partial class Context
    {        
        public static Sync Fence()
        {
            unsafe
            {
                var handle = Gl.FenceSync(Gl.SYNC_GPU_COMMANDS_COMPLETE, 0);
                GlHelper.GetError();
                return new Sync(new IntPtr(handle));
            }
        }

        public static SyncStatus ClientWait(Sync sync, bool flush, TimeSpan timeout)
        {
            if (sync == Sync.Null)
                throw new ArgumentNullException("sync");

            unsafe
            {
                uint result = Gl.ClientWaitSync(
                    sync.Handle.ToPointer(),
                    flush ? Gl.SYNC_FLUSH_COMMANDS_BIT : 0,
                    (ulong)timeout.Ticks * 100);

                GlHelper.GetError();

                return (SyncStatus)result;
            }
        }

        public static void Wait(Sync sync)
        {
            if (sync == Sync.Null)
                throw new ArgumentNullException("sync");

            unsafe
            {
                Gl.WaitSync(sync.Handle.ToPointer(), 0, Gl.TIMEOUT_IGNORED);
                GlHelper.GetError();
            }
        }
    }
}
