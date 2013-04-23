using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Ibasa.OpenCL
{
    public struct Image : IEquatable<Image>
    {
        public static readonly Image Null = new Image();

        public IntPtr Handle { get; private set; }

        public Image(IntPtr handle)
            : this()
        {
            Handle = handle;
        }

        public Image(Context context, MemoryFlags flags, 
            ChannelOrder order, ChannelType type,
            ImageType imageType, ulong width, ulong height, ulong depth, 
            ulong arraySize, ulong rowPitch, ulong slicePitch, IntPtr hostPtr)
            : this()
        {
             if (context == Context.Null)
                throw new ArgumentNullException("context");

            if (flags.HasFlag(MemoryFlags.WriteOnly) & flags.HasFlag(MemoryFlags.ReadOnly))
                throw new ArgumentException("MemoryFlags.WriteOnly and MemoryFlags.ReadOnly are mutually exclusive.");
            if (flags.HasFlag(MemoryFlags.HostWriteOnly) & flags.HasFlag(MemoryFlags.HostReadOnly))
                throw new ArgumentException("MemoryFlags.HostWriteOnly and MemoryFlags.HostReadOnly are mutually exclusive.");
            if (flags.HasFlag(MemoryFlags.HostWriteOnly) & flags.HasFlag(MemoryFlags.HostNoAccess))
                throw new ArgumentException("MemoryFlags.HostWriteOnly and MemoryFlags.HostNoAccess are mutually exclusive.");
            if (flags.HasFlag(MemoryFlags.HostReadOnly) & flags.HasFlag(MemoryFlags.HostNoAccess))
                throw new ArgumentException("MemoryFlags.HostReadOnly and MemoryFlags.HostNoAccess are mutually exclusive.");
            
            if (width == 0)
                throw new ArgumentOutOfRangeException("width", width, "width is 0.");
            if (height == 0)
                throw new ArgumentOutOfRangeException("height", height, "height is 0.");
            if (depth == 0)
                throw new ArgumentOutOfRangeException("depth", depth, "depth is 0.");

            if (hostPtr == IntPtr.Zero)
            {
                if (flags.HasFlag(MemoryFlags.UseHostPtr))
                    throw new ArgumentException("MemoryFlags.UseHostPtr is not valid.");
                if (flags.HasFlag(MemoryFlags.CopyHostPtr))
                    throw new ArgumentException("MemoryFlags.CopyHostPtr is not valid.");

                if(rowPitch != 0)
                    throw new ArgumentOutOfRangeException("rowPitch", rowPitch, "rowPitch is not 0.");
                if(slicePitch != 0)
                    throw new ArgumentOutOfRangeException("slicePitch", slicePitch, "slicePitch is not 0.");
            }
            else
            {
                if (!flags.HasFlag(MemoryFlags.UseHostPtr) & !flags.HasFlag(MemoryFlags.CopyHostPtr))
                    throw new ArgumentException("MemoryFlags.UseHostPtr or MemoryFlags.CopyHostPtr is required.");
                if (flags.HasFlag(MemoryFlags.UseHostPtr) & flags.HasFlag(MemoryFlags.CopyHostPtr))
                    throw new ArgumentException("MemoryFlags.UseHostPtr and MemoryFlags.CopyHostPtr are mutually exclusive.");
                if (flags.HasFlag(MemoryFlags.UseHostPtr) & flags.HasFlag(MemoryFlags.AllocHostPtr))
                    throw new ArgumentException("MemoryFlags.UseHostPtr and MemoryFlags.AllocHostPtr are mutually exclusive.");
           

                if(rowPitch != 0 && rowPitch < width)
                    throw new ArgumentOutOfRangeException("rowPitch", rowPitch, "rowPitch is not 0 and is less than width.");
                if(slicePitch != 0 && slicePitch < height)
                    throw new ArgumentOutOfRangeException("slicePitch", slicePitch, "slicePitch is not 0 and is less than height.");
            }

            unsafe
            {
                Cl.image_format image_format = new Cl.image_format()
                {
                    image_channel_order = (uint)order,
                    image_channel_data_type = (uint)type,
                };

                Cl.image_desc image_desc = new Cl.image_desc()
                {
                    image_type = (uint)imageType,
                    image_width = new UIntPtr(width),
                    image_height = new UIntPtr(height),
                    image_depth = new UIntPtr(depth),
                    image_array_size = new UIntPtr(arraySize),
                    image_row_pitch = new UIntPtr(rowPitch),
                    image_slice_pitch = new UIntPtr(slicePitch),
                    num_mip_level = 0,
                    num_samples = 0,
                    buffer = IntPtr.Zero,
                };

                int error;
                Handle = Cl.CreateImage(
                    context.Handle, (ulong)flags,
                    &image_format, &image_desc, hostPtr.ToPointer(), &error);
                ClHelper.GetError(error);
            }
        }

        public static Image CreateImage2D(Context context, MemoryFlags flags, 
            ChannelOrder order, ChannelType type, ulong width, ulong height, 
            ulong pitch, IntPtr hostPtr)
        {
            if (context == Context.Null)
                throw new ArgumentNullException("context");

            if (flags.HasFlag(MemoryFlags.WriteOnly) & flags.HasFlag(MemoryFlags.ReadOnly))
                throw new ArgumentException("MemoryFlags.WriteOnly and MemoryFlags.ReadOnly are mutually exclusive.");
            if (flags.HasFlag(MemoryFlags.HostWriteOnly) & flags.HasFlag(MemoryFlags.HostReadOnly))
                throw new ArgumentException("MemoryFlags.HostWriteOnly and MemoryFlags.HostReadOnly are mutually exclusive.");
            if (flags.HasFlag(MemoryFlags.HostWriteOnly) & flags.HasFlag(MemoryFlags.HostNoAccess))
                throw new ArgumentException("MemoryFlags.HostWriteOnly and MemoryFlags.HostNoAccess are mutually exclusive.");
            if (flags.HasFlag(MemoryFlags.HostReadOnly) & flags.HasFlag(MemoryFlags.HostNoAccess))
                throw new ArgumentException("MemoryFlags.HostReadOnly and MemoryFlags.HostNoAccess are mutually exclusive.");
            
            if (width == 0)
                throw new ArgumentOutOfRangeException("width", width, "width is 0.");
            if (height == 0)
                throw new ArgumentOutOfRangeException("height", height, "height is 0.");

            if (hostPtr == IntPtr.Zero)
            {
                if (flags.HasFlag(MemoryFlags.UseHostPtr))
                    throw new ArgumentException("MemoryFlags.UseHostPtr is not valid.");
                if (flags.HasFlag(MemoryFlags.CopyHostPtr))
                    throw new ArgumentException("MemoryFlags.CopyHostPtr is not valid.");

                if(pitch != 0)
                    throw new ArgumentOutOfRangeException("pitch", pitch, "pitch is not 0.");
            }
            else
            {
                if (!flags.HasFlag(MemoryFlags.UseHostPtr) & !flags.HasFlag(MemoryFlags.CopyHostPtr))
                    throw new ArgumentException("MemoryFlags.UseHostPtr or MemoryFlags.CopyHostPtr is required.");
                if (flags.HasFlag(MemoryFlags.UseHostPtr) & flags.HasFlag(MemoryFlags.CopyHostPtr))
                    throw new ArgumentException("MemoryFlags.UseHostPtr and MemoryFlags.CopyHostPtr are mutually exclusive.");
                if (flags.HasFlag(MemoryFlags.UseHostPtr) & flags.HasFlag(MemoryFlags.AllocHostPtr))
                    throw new ArgumentException("MemoryFlags.UseHostPtr and MemoryFlags.AllocHostPtr are mutually exclusive.");
           

                if(pitch != 0 && pitch < width)
                    throw new ArgumentOutOfRangeException("pitch", pitch, "pitch is not 0 and is less than width.");
            }

            unsafe
            {
                Cl.image_format image_format = new Cl.image_format()
                {
                    image_channel_order = (uint)order,
                    image_channel_data_type = (uint)type,
                };

                int error;
                var handle = Cl.CreateImage2D(
                    context.Handle, (ulong)flags,
                    &image_format,
                    new UIntPtr(width), new UIntPtr(height),
                    new UIntPtr(pitch), hostPtr.ToPointer(), &error);
                ClHelper.GetError(error);

                return new Image(handle);
            }
        }

        public static Image CreateImage3D(Context context, MemoryFlags flags, 
            ChannelOrder order, ChannelType type, ulong width, ulong height, ulong depth, 
            ulong rowPitch, ulong slicePitch, IntPtr hostPtr)
        {
            if (context == Context.Null)
                throw new ArgumentNullException("context");

            if (flags.HasFlag(MemoryFlags.WriteOnly) & flags.HasFlag(MemoryFlags.ReadOnly))
                throw new ArgumentException("MemoryFlags.WriteOnly and MemoryFlags.ReadOnly are mutually exclusive.");
            if (flags.HasFlag(MemoryFlags.HostWriteOnly) & flags.HasFlag(MemoryFlags.HostReadOnly))
                throw new ArgumentException("MemoryFlags.HostWriteOnly and MemoryFlags.HostReadOnly are mutually exclusive.");
            if (flags.HasFlag(MemoryFlags.HostWriteOnly) & flags.HasFlag(MemoryFlags.HostNoAccess))
                throw new ArgumentException("MemoryFlags.HostWriteOnly and MemoryFlags.HostNoAccess are mutually exclusive.");
            if (flags.HasFlag(MemoryFlags.HostReadOnly) & flags.HasFlag(MemoryFlags.HostNoAccess))
                throw new ArgumentException("MemoryFlags.HostReadOnly and MemoryFlags.HostNoAccess are mutually exclusive.");
            
            if (width == 0)
                throw new ArgumentOutOfRangeException("width", width, "width is 0.");
            if (height == 0)
                throw new ArgumentOutOfRangeException("height", height, "height is 0.");
            if (depth == 0)
                throw new ArgumentOutOfRangeException("depth", depth, "depth is 0.");

            if (hostPtr == IntPtr.Zero)
            {
                if (flags.HasFlag(MemoryFlags.UseHostPtr))
                    throw new ArgumentException("MemoryFlags.UseHostPtr is not valid.");
                if (flags.HasFlag(MemoryFlags.CopyHostPtr))
                    throw new ArgumentException("MemoryFlags.CopyHostPtr is not valid.");

                if(rowPitch != 0)
                    throw new ArgumentOutOfRangeException("rowPitch", rowPitch, "rowPitch is not 0.");
                if(slicePitch != 0)
                    throw new ArgumentOutOfRangeException("slicePitch", slicePitch, "slicePitch is not 0.");
            }
            else
            {
                if (!flags.HasFlag(MemoryFlags.UseHostPtr) & !flags.HasFlag(MemoryFlags.CopyHostPtr))
                    throw new ArgumentException("MemoryFlags.UseHostPtr or MemoryFlags.CopyHostPtr is required.");
                if (flags.HasFlag(MemoryFlags.UseHostPtr) & flags.HasFlag(MemoryFlags.CopyHostPtr))
                    throw new ArgumentException("MemoryFlags.UseHostPtr and MemoryFlags.CopyHostPtr are mutually exclusive.");
                if (flags.HasFlag(MemoryFlags.UseHostPtr) & flags.HasFlag(MemoryFlags.AllocHostPtr))
                    throw new ArgumentException("MemoryFlags.UseHostPtr and MemoryFlags.AllocHostPtr are mutually exclusive.");
           

                if(rowPitch != 0 && rowPitch < width)
                    throw new ArgumentOutOfRangeException("rowPitch", rowPitch, "rowPitch is not 0 and is less than width.");
                if(slicePitch != 0 && slicePitch < height)
                    throw new ArgumentOutOfRangeException("slicePitch", slicePitch, "slicePitch is not 0 and is less than height.");
            }

            unsafe
            {
                Cl.image_format image_format = new Cl.image_format()
                {
                    image_channel_order = (uint)order,
                    image_channel_data_type = (uint)type,
                };

                int error;
                var handle = Cl.CreateImage3D(
                    context.Handle, (ulong)flags,
                    &image_format,
                    new UIntPtr(width), new UIntPtr(height), new UIntPtr(depth), 
                    new UIntPtr(rowPitch), new UIntPtr(slicePitch), hostPtr.ToPointer(), &error);
                ClHelper.GetError(error);

                return new Image(handle);
            }
        }

        private delegate void CallbackDelegete(IntPtr buffer, IntPtr user_data);
        private static void Callback(IntPtr buffer, IntPtr user_data)
        {
            var handel = GCHandle.FromIntPtr(user_data);
            var data = (Tuple<Action<Image, object>, object>)handel.Target;
            handel.Free();

            data.Item1(new Image(buffer), data.Item2);
        }

        public void SetDestructorCallback(Action<Image, object> notify, object user_data)
        {
            ClHelper.ThrowNullException(Handle);

            if (notify == null)
                throw new ArgumentNullException("notify");

            unsafe
            {
                var function_ptr = IntPtr.Zero;
                var data = Tuple.Create(notify, user_data);
                var data_handle = GCHandle.Alloc(data);

                function_ptr = Marshal.GetFunctionPointerForDelegate(new CallbackDelegete(Callback));

                try
                {
                    ClHelper.GetError(Cl.SetMemObjectDestructorCallback(
                        Handle, function_ptr, GCHandle.ToIntPtr(data_handle).ToPointer()));
                }
                catch (EntryPointNotFoundException)
                {
                    data_handle.Free();
                    throw ClHelper.VersionException(1, 1);
                }
                catch (Exception)
                {
                    data_handle.Free();
                    throw;
                }
            }
        }

        public void RetainImage()
        {
            ClHelper.ThrowNullException(Handle);
            ClHelper.GetError(Cl.RetainMemObject(Handle));
        }

        public void ReleaseImage()
        {
            ClHelper.ThrowNullException(Handle);
            int error = Cl.ReleaseMemObject(Handle);
            Handle = IntPtr.Zero;
            ClHelper.GetError(error);
        }

        public Tuple<ChannelOrder, ChannelType> Format
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                unsafe
                {
                    Cl.image_format value;
                    UIntPtr param_value_size = new UIntPtr((uint)Marshal.SizeOf(typeof(Cl.image_format)));
                    ClHelper.GetError(Cl.GetImageInfo(
                        Handle, Cl.IMAGE_FORMAT, param_value_size, &value, null));
                    return Tuple.Create(
                        (ChannelOrder)value.image_channel_order, 
                        (ChannelType)value.image_channel_data_type);
                }
            }
        }

        public ulong ElementSize 
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                unsafe
                {
                    ulong value;
                    UIntPtr param_value_size = new UIntPtr(sizeof(ulong));
                    ClHelper.GetError(Cl.GetMemObjectInfo(
                        Handle, Cl.IMAGE_ELEMENT_SIZE, param_value_size, &value, null));
                    return value;
                }
            }
        }
        
        public ulong RowPitch 
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                unsafe
                {
                    ulong value;
                    UIntPtr param_value_size = new UIntPtr(sizeof(ulong));
                    ClHelper.GetError(Cl.GetMemObjectInfo(
                        Handle, Cl.IMAGE_ROW_PITCH, param_value_size, &value, null));
                    return value;
                }
            }
        }
        
        public ulong SlicePitch 
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                unsafe
                {
                    ulong value;
                    UIntPtr param_value_size = new UIntPtr(sizeof(ulong));
                    ClHelper.GetError(Cl.GetMemObjectInfo(
                        Handle, Cl.IMAGE_SLICE_PITCH, param_value_size, &value, null));
                    return value;
                }
            }
        }
        
        public ulong Width
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                unsafe
                {
                    ulong value;
                    UIntPtr param_value_size = new UIntPtr(sizeof(ulong));
                    ClHelper.GetError(Cl.GetMemObjectInfo(
                        Handle, Cl.IMAGE_WIDTH, param_value_size, &value, null));
                    return value;
                }
            }
        }
        
        public ulong Height
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                unsafe
                {
                    ulong value;
                    UIntPtr param_value_size = new UIntPtr(sizeof(ulong));
                    ClHelper.GetError(Cl.GetMemObjectInfo(
                        Handle, Cl.IMAGE_HEIGHT, param_value_size, &value, null));
                    return value;
                }
            }
        }
        
        public ulong Depth
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                unsafe
                {
                    ulong value;
                    UIntPtr param_value_size = new UIntPtr(sizeof(ulong));
                    ClHelper.GetError(Cl.GetMemObjectInfo(
                        Handle, Cl.IMAGE_DEPTH, param_value_size, &value, null));
                    return value;
                }
            }
        }
        
        public ulong ArraySize
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                unsafe
                {
                    ulong value;
                    UIntPtr param_value_size = new UIntPtr(sizeof(ulong));
                    ClHelper.GetError(Cl.GetMemObjectInfo(
                        Handle, Cl.IMAGE_ARRAY_SIZE, param_value_size, &value, null));
                    return value;
                }
            }
        }

        public MemoryFlags MemoryFlags
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                unsafe
                {
                    ulong value;
                    UIntPtr param_value_size = new UIntPtr(sizeof(ulong));
                    ClHelper.GetError(Cl.GetMemObjectInfo(
                        Handle, Cl.MEM_FLAGS, param_value_size, &value, null));
                    return (MemoryFlags)value;
                }
            }
        }

        public MemoryObjectType MemoryObjectType
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                unsafe
                {
                    uint value;
                    UIntPtr param_value_size = new UIntPtr(sizeof(uint));
                    ClHelper.GetError(Cl.GetMemObjectInfo(
                        Handle, Cl.MEM_TYPE, param_value_size, &value, null));
                    return (MemoryObjectType)value;
                }
            }
        }

        public ulong Size
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                unsafe
                {
                    ulong value;
                    UIntPtr param_value_size = new UIntPtr(sizeof(ulong));
                    ClHelper.GetError(Cl.GetMemObjectInfo(
                        Handle, Cl.MEM_SIZE, param_value_size, &value, null));
                    return value;
                }
            }
        }

        public IntPtr HostPtr
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                unsafe
                {
                    IntPtr value;
                    UIntPtr param_value_size = new UIntPtr((uint)IntPtr.Size);
                    ClHelper.GetError(Cl.GetMemObjectInfo(
                        Handle, Cl.MEM_HOST_PTR, param_value_size, &value, null));
                    return value;
                }
            }
        }

        public long MapCount
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                unsafe
                {
                    uint value;
                    UIntPtr param_value_size = new UIntPtr(sizeof(uint));
                    ClHelper.GetError(Cl.GetMemObjectInfo(
                        Handle, Cl.MEM_MAP_COUNT, param_value_size, &value, null));
                    return value;
                }
            }
        }

        public long ReferenceCount
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                unsafe
                {
                    uint value;
                    UIntPtr param_value_size = new UIntPtr(sizeof(uint));
                    ClHelper.GetError(Cl.GetMemObjectInfo(
                        Handle, Cl.MEM_REFERENCE_COUNT, param_value_size, &value, null));
                    return value;
                }
            }
        }

        Context Context
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                unsafe
                {
                    IntPtr value;
                    UIntPtr param_value_size = new UIntPtr((uint)IntPtr.Size);
                    ClHelper.GetError(Cl.GetMemObjectInfo(
                        Handle, Cl.MEM_CONTEXT, param_value_size, &value, null));
                    return new Context(value);
                }
            }
        }
        
        public override int GetHashCode()
        {
            ClHelper.ThrowNullException(Handle);
            return Handle.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            ClHelper.ThrowNullException(Handle);
            if (obj is Image)
            {
                return Equals((Image)obj);
            }
            return false;
        }

        public bool Equals(Image other)
        {
            ClHelper.ThrowNullException(Handle);
            return Handle == other.Handle;
        }

        public static bool operator ==(Image left, Image right)
        {
            return left.Handle == right.Handle;
        }

        public static bool operator !=(Image left, Image right)
        {
            return left.Handle != right.Handle;
        }

        public override string ToString()
        {
            ClHelper.ThrowNullException(Handle);
            return string.Format("Image: {0}", Handle.ToString());
        }
    }
}