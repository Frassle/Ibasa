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
            : this()
        {
            if (context == Context.Null)
                throw new ArgumentNullException("context");

            unsafe
            {
                int error;
                Handle = Cl.CreateSampler(context.Handle,
                    normalizedCoords ? 1u : 0u, (uint)addressingMode, (uint)filterMode, &error);
                ClHelper.GetError(error);
            }
        }

        public void Retain()
        {
            ClHelper.ThrowNullException(Handle);
            ClHelper.GetError(Cl.RetainSampler(Handle));
        }

        public void Release()
        {
            ClHelper.ThrowNullException(Handle);
            int error = Cl.ReleaseSampler(Handle);
            Handle = IntPtr.Zero;
            ClHelper.GetError(error);
        }

        public AddressingMode AddressingMode
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                unsafe
                {
                    uint value;
                    UIntPtr param_value_size = new UIntPtr(sizeof(uint));
                    ClHelper.GetError(Cl.GetSamplerInfo(
                        Handle, Cl.SAMPLER_ADDRESSING_MODE, param_value_size, &value, null));
                    return (AddressingMode)value;
                }
            }
        }

        public FilterMode FilterMode
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                unsafe
                {
                    uint value;
                    UIntPtr param_value_size = new UIntPtr(sizeof(uint));
                    ClHelper.GetError(Cl.GetSamplerInfo(
                        Handle, Cl.SAMPLER_FILTER_MODE, param_value_size, &value, null));
                    return (FilterMode)value;
                }
            }
        }

        public Context Context
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                unsafe
                {
                    IntPtr value;
                    UIntPtr param_value_size = new UIntPtr((uint)IntPtr.Size);
                    ClHelper.GetError(Cl.GetSamplerInfo(
                        Handle, Cl.SAMPLER_CONTEXT, param_value_size, &value, null));
                    return new Context(value);
                }
            }
        }

        public bool NormalizedCoords
        {
            get
            {
                ClHelper.ThrowNullException(Handle);
                unsafe
                {
                    uint value;
                    UIntPtr param_value_size = new UIntPtr(sizeof(uint));
                    ClHelper.GetError(Cl.GetSamplerInfo(
                        Handle, Cl.SAMPLER_NORMALIZED_COORDS, param_value_size, &value, null));
                    return value != 0;
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
                    ClHelper.GetError(Cl.GetSamplerInfo(
                        Handle, Cl.SAMPLER_REFERENCE_COUNT, param_value_size, &value, null));
                    return value;
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
            if (obj is Sampler)
            {
                return Equals((Sampler)obj);
            }
            return false;
        }

        public bool Equals(Sampler other)
        {
            ClHelper.ThrowNullException(Handle);
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
            ClHelper.ThrowNullException(Handle);
            return string.Format("Sampler: {0}", Handle.ToString());
        }
    }
}
