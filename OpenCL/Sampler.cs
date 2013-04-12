using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ibasa.OpenCL
{
    public struct Sampler : IEquatable<Sampler>
    {
        public static readonly Sampler Null = new Sampler();

        public IntPtr Handle { get; private set; }

        public Sampler(IntPtr handle)
            : this()
        {
            Handle = handle;
        }

        public Sampler(Context context, bool normalizedCoords, AddressingMode addressingMode, FilterMode filterMode)
        {
            if (context == Context.Null)
                throw new ArgumentNullException("context");

            unsafe
            {
                int error;
                Handle = CL.CreateSampler(context.Handle,
                    normalizedCoords ? 1u : 0u, (uint)addressingMode, (uint)filterMode, &error);
                CLHelper.GetError(error);
            }
        }

        public void Retain()
        {
            CLHelper.ThrowNullException(Handle);
            CLHelper.GetError(CL.RetainSampler(Handle));
        }

        public void Release()
        {
            CLHelper.ThrowNullException(Handle);
            int error = CL.ReleaseSampler(Handle);
            Handle = IntPtr.Zero;
            CLHelper.GetError(error);
        }

        public AddressingMode AddressingMode
        {
            get
            {
                CLHelper.ThrowNullException(Handle);
                unsafe
                {
                    uint value;
                    UIntPtr param_value_size = new UIntPtr(sizeof(uint));
                    CLHelper.GetError(CL.GetSamplerInfo(
                        Handle, CL.SAMPLER_ADDRESSING_MODE, param_value_size, &value, null));
                    return (AddressingMode)value;
                }
            }
        }

        public FilterMode FilterMode
        {
            get
            {
                CLHelper.ThrowNullException(Handle);
                unsafe
                {
                    uint value;
                    UIntPtr param_value_size = new UIntPtr(sizeof(uint));
                    CLHelper.GetError(CL.GetSamplerInfo(
                        Handle, CL.SAMPLER_FILTER_MODE, param_value_size, &value, null));
                    return (FilterMode)value;
                }
            }
        }

        public Context Context
        {
            get
            {
                CLHelper.ThrowNullException(Handle);
                unsafe
                {
                    IntPtr value;
                    UIntPtr param_value_size = new UIntPtr((uint)IntPtr.Size);
                    CLHelper.GetError(CL.GetSamplerInfo(
                        Handle, CL.SAMPLER_CONTEXT, param_value_size, &value, null));
                    return new Context(value);
                }
            }
        }

        public bool NormalizedCoords
        {
            get
            {
                CLHelper.ThrowNullException(Handle);
                unsafe
                {
                    uint value;
                    UIntPtr param_value_size = new UIntPtr(sizeof(uint));
                    CLHelper.GetError(CL.GetSamplerInfo(
                        Handle, CL.SAMPLER_NORMALIZED_COORDS, param_value_size, &value, null));
                    return value != 0;
                }
            }
        }

        public long ReferenceCount
        {
            get
            {
                CLHelper.ThrowNullException(Handle);
                unsafe
                {
                    uint value;
                    UIntPtr param_value_size = new UIntPtr(sizeof(uint));
                    CLHelper.GetError(CL.GetSamplerInfo(
                        Handle, CL.SAMPLER_REFERENCE_COUNT, param_value_size, &value, null));
                    return value;
                }
            }
        }

        public override int GetHashCode()
        {
            CLHelper.ThrowNullException(Handle);
            return Handle.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            CLHelper.ThrowNullException(Handle);
            if (obj is Sampler)
            {
                return Equals((Sampler)obj);
            }
            return false;
        }

        public bool Equals(Sampler other)
        {
            CLHelper.ThrowNullException(Handle);
            return Handle == other.Handle;
        }

        public static bool operator ==(Sampler left, Sampler right)
        {
            return left.Handle == right.Handle;
        }

        public static bool operator !=(Sampler left, Sampler right)
        {
            return left.Handle != right.Handle;
        }

        public override string ToString()
        {
            CLHelper.ThrowNullException(Handle);
            return Handle.ToString();
        }
    }
}
