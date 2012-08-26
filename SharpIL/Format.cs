using System.Diagnostics.Contracts;
using System.IO;
using Ibasa.Numerics;
using Ibasa.Numerics.Geometry;
using Ibasa.SharpIL.Formats;

namespace Ibasa.SharpIL
{
    public abstract class Format<T> : Format
    {
        public Format(string name)
            : base(name)
        { }
        public Format(string name, Colord minColord, Colord maxColord, bool isNormalized)
            : base(name, maxColord, minColord, isNormalized)
        {
        }
        public Format(string name, Colord minColord, Colord maxColord, bool isNormalized, bool isCompressed)
            : base(name, minColord, maxColord, isNormalized, isCompressed)
        {
        }

        public abstract void GetBytes(
            T[] source, int index, int width, int height,
            Stream destination, int rowPitch, int slicePitch,
            Boxi sourceBoxi, Point3i destinationPoint);

        public virtual void GetBytes(
            T[] source, int index, int width, int height,
            byte[] destination, int rowPitch, int slicePitch,
            Boxi sourceBoxi, Point3i destinationPoint)
        {
            GetBytes(
                source, index, width, height,
                new MemoryStream(destination), rowPitch, slicePitch,
                sourceBoxi, destinationPoint);
        }

        public abstract void GetData(
            Stream source, int rowPitch, int slicePitch,
            T[] destination, int index, int width, int height,
            Boxi sourceBoxi, Point3i destinationPoint);
        
        public virtual void GetData(
            byte[] source, int rowPitch, int slicePitch,
            T[] destination, int index, int width, int height,
            Boxi sourceBoxi, Point3i destinationPoint)
        {
            GetData(
                new MemoryStream(source), rowPitch, slicePitch,
                destination, index, width, height,                
                sourceBoxi, destinationPoint);
        }
    }

    //[ContractClass(typeof(FormatContracts))]
    public abstract class Format
    {
        public static void Convert(Format sourceFormat, Format destinationFormat,
            Stream source, int sourceRowPitch, int sourceSlicePitch,
            Stream destination, int destinationRowPitch, int destinationSlicePitch,
            Boxi sourceBoxi, Point3i destinationPoint)
        {
            Contract.Requires(sourceFormat != null);
            Contract.Requires(destinationFormat != null);

            Colord[] colors = new Colord[sourceBoxi.Width * sourceBoxi.Height * sourceBoxi.Depth];

            sourceFormat.GetColords(
                source, sourceRowPitch, sourceSlicePitch, 
                colors, 0, sourceBoxi.Width, sourceBoxi.Height,
                sourceBoxi, Point3i.Zero);

            destinationFormat.GetBytes(
                colors, 0, sourceBoxi.Width, sourceBoxi.Height,
                destination, destinationRowPitch, destinationSlicePitch,
                new Boxi(Point3i.Zero, sourceBoxi.Size), destinationPoint);
        }

        public static void Convert(Format sourceFormat, Format destinationFormat,
            byte[] source, int sourceRowPitch, int sourceSlicePitch,
            byte[] destination, int destinationRowPitch, int destinationSlicePitch,
            Boxi sourceBoxi, Point3i destinationPoint)
        {
            Convert(sourceFormat, destinationFormat,
                new MemoryStream(source), sourceRowPitch, sourceSlicePitch,
                new MemoryStream(destination), destinationRowPitch, destinationSlicePitch,
                sourceBoxi, destinationPoint);
        }

        public Format(string name)
            : this(name, Colord.White, Colord.Black, true)
        { }        
        public Format(string name, Colord maxColord, Colord minColord, bool isNormalized)
            : this(name, minColord, maxColord, isNormalized, false)
        {
        }
        public Format(string name, Colord minColord, Colord maxColord, bool isNormalized, bool isCompressed)
        {
            Contract.Requires(!string.IsNullOrWhiteSpace(name), "name cannot be null or whitespace.");
            Contract.Requires(maxColord.R >= minColord.R, "maxColord must be greater than or equal to minColord");
            Contract.Requires(maxColord.G >= minColord.G, "maxColord must be greater than or equal to minColord");
            Contract.Requires(maxColord.B >= minColord.B, "maxColord must be greater than or equal to minColord");
            Contract.Requires(maxColord.A >= minColord.A, "maxColord must be greater than or equal to minColord");

            Name = name;
            MaxColord = maxColord;
            MinColord = minColord;
            IsNormalized = isNormalized;
            IsCompressed = isCompressed;
        }

        public string Name { get; private set; }
        public Colord MaxColord { get; private set; }
        public Colord MinColord { get; private set; }
        public bool IsNormalized { get; private set; }
        public bool IsCompressed { get; private set; }

        public abstract Size3i GetPhysicalSize(Size3i size);

        public abstract int GetByteCount(Size3i size, out int rowPitch, out int slicePitch);
        public virtual int GetByteCount(Size3i size)
        {
            int rowPitch, slicePitch;
            return GetByteCount(size, out rowPitch, out slicePitch);
        }

        public abstract void GetBytes(
            Colord[] source, int index, int width, int height,
            Stream destination, int rowPitch, int slicePitch,
            Boxi sourceBoxi, Point3i destinationPoint);

        public virtual void GetBytes(
            Colord[] source, int index, int width, int height,
            byte[] destination, int rowPitch, int slicePitch,
            Boxi sourceBoxi, Point3i destinationPoint)
        {
            GetBytes(
                source, index, width, height,
                new MemoryStream(destination), rowPitch, slicePitch,
                sourceBoxi, destinationPoint);
        }

        public abstract Size3i GetColordCount(int byteCount, int rowPitch, int slicePitch);

        public abstract void GetColords(
            Stream source, int rowPitch, int slicePitch,
            Colord[] destination, int index, int width, int height,
            Boxi sourceBoxi, Point3i destinationPoint);
        
        public virtual void GetColords(
            byte[] source, int rowPitch, int slicePitch,
            Colord[] destination, int index, int width, int height,
            Boxi sourceBoxi, Point3i destinationPoint)
        {
            GetColords(
                new MemoryStream(source), rowPitch, slicePitch,
                destination, index, width, height,
                sourceBoxi, destinationPoint);
        }

        #region BC
        private static Format _BC1;
        /// <summary>
        /// Four-component block-compression format.
        /// </summary>
        public static Format BC1
        {
            get
            {
                if (_BC1 == null)
                    _BC1 = new BC1(Options.Default);
                return _BC1;
            }
        }
        private static Format _BC2;
        /// <summary>
        /// Four-component block-compression format.
        /// </summary>
        public static Format BC2
        {
            get
            {
                if (_BC2 == null)
                    _BC2 = new BC2(Options.Default);
                return _BC2;
            }
        }
        private static Format _BC3;
        /// <summary>
        /// Four-component block-compression format.
        /// </summary>
        public static Format BC3
        {
            get
            {
                if (_BC3 == null)
                    _BC3 = new BC3(Options.Default);
                return _BC3;
            }
        }
        private static Format _BC4;
        /// <summary>
        /// One-component block-compression format.
        /// </summary>
        public static Format BC4
        {
            get
            {
                if (_BC4 == null)
                    _BC4 = new BC4(Options.Default);
                return _BC4;
            }
        }
        private static Format _BC4Norm;
        /// <summary>
        /// One-component block-compression format.
        /// </summary>
        public static Format BC4Norm
        {
            get
            {
                if (_BC4Norm == null)
                    _BC4Norm = new BC4Norm(Options.Default);
                return _BC4Norm;
            }
        }
        private static Format _BC5;
        /// <summary>
        /// Two-component block-compression format.
        /// </summary>
        public static Format BC5
        {
            get
            {
                if (_BC5 == null)
                    _BC5 = new BC5(Options.Default);
                return _BC5;
            }
        }
        private static Format _BC5Norm;
        /// <summary>
        /// Two-component block-compression format.
        /// </summary>
        public static Format BC5Norm
        {
            get
            {
                if (_BC5Norm == null)
                    _BC5Norm = new BC5Norm(Options.Default);
                return _BC5Norm;
            }
        }
        //private static Format _BC6S;
        ///// <summary>
        ///// A block-compression format.
        ///// </summary>
        //public static Format BC6S
        //{
        //    get
        //    {
        //        if (_BC6S == null)
        //            _BC6S = new BC6S(Options.Default);
        //        return _BC6S;
        //    }
        //}
        //private static Format _BC6U;
        ///// <summary>
        ///// A block-compression format.
        ///// </summary>
        //public static Format BC6U
        //{
        //    get
        //    {
        //        if (_BC6U == null)
        //            _BC6U = new BC6U(Options.Default);
        //        return _BC6U;
        //    }
        //}
        private static Format _BC7;
        /// <summary>
        /// A block-compression format.
        /// </summary>
        public static Format BC7
        {
            get
            {
                if (_BC7 == null)
                    _BC7 = new BC7(Options.Default);
                return _BC7;
            }
        }
        #endregion
        
        #region B5GxR5Ax
        private static Format<ushort> _B5G6R5UNorm;
        /// <summary>
        /// A three-component, 16-bit unsigned-normalized integer format.
        /// </summary>
        public static Format<ushort> B5G6R5UNorm
        {
            get
            {
                if (_B5G6R5UNorm == null)
                    _B5G6R5UNorm = new B5G6R5UNorm();
                return _B5G6R5UNorm;
            }
        }
        private static Format<ushort> _B5G5R5A1UNorm;
        /// <summary>
        /// A four-component, 16-bit unsigned-normalized integer format that supports 1-bit alpha.
        /// </summary>
        public static Format<ushort> B5G5R5A1UNorm
        {
            get
            {
                if (_B5G5R5A1UNorm == null)
                    _B5G5R5A1UNorm = new B5G5R5A1UNorm();
                return _B5G5R5A1UNorm;
            }
        }
        #endregion

        #region B8G8R8A8
        private static Format<Vector4b> _B8G8R8A8UNorm;
        /// <summary>
        /// A four-component, 32-bit unsigned-normalized integer format that supports 8-bit alpha.
        /// </summary>
        public static Format<Vector4b> B8G8R8A8UNorm
        {
            get
            {
                if (_B8G8R8A8UNorm == null)
                    _B8G8R8A8UNorm = new B8G8R8A8UNorm();
                return _B8G8R8A8UNorm;
            }
        }
        #endregion

        #region B8G8R8
        private static Format<Vector3b> _B8G8R8UNorm;
        /// <summary>
        /// A three-component, 24-bit unsigned-normalized integer format.
        /// </summary>
        public static Format<Vector3b> B8G8R8UNorm
        {
            get
            {
                if (_B8G8R8UNorm == null)
                    _B8G8R8UNorm = new B8G8R8UNorm();
                return _B8G8R8UNorm;
            }
        }
        #endregion

        #region R32G32B32A32
        private static Format<Vector4f> _R32G32B32A32Float;
        /// <summary>
        /// A four-component, 128-bit floating-point format.
        /// </summary>
        public static Format<Vector4f> R32G32B32A32Float
        {
            get
            {
                if (_R32G32B32A32Float == null)
                    _R32G32B32A32Float = new R32G32B32A32Float();
                return _R32G32B32A32Float;
            }
        }
        private static Format<Vector4ui> _R32G32B32A32UInt;
        /// <summary>
        /// A four-component, 128-bit unsigned-integer format.
        /// </summary>
        public static Format<Vector4ui> R32G32B32A32UInt
        {
            get
            {
                if (_R32G32B32A32UInt == null)
                    _R32G32B32A32UInt = new R32G32B32A32UInt();
                return _R32G32B32A32UInt;
            }
        }
        private static Format<Vector4i> _R32G32B32A32Int;
        /// <summary>
        /// A four-component, 128-bit signed-integer format.
        /// </summary>
        public static Format<Vector4i> R32G32B32A32Int
        {
            get
            {
                if (_R32G32B32A32Int == null)
                    _R32G32B32A32Int = new R32G32B32A32Int();
                return _R32G32B32A32Int;
            }
        }
        #endregion

        #region R16G16B16A16
        private static Format<Vector4h> _R16G16B16A16Float;
        /// <summary>
        /// A four-component, 64-bit floating-point format.
        /// </summary>
        public static Format<Vector4h> R16G16B16A16Float
        {
            get
            {
                if (_R16G16B16A16Float == null)
                    _R16G16B16A16Float = new R16G16B16A16Float();
                return _R16G16B16A16Float;
            }
        }
        private static Format<Vector4us> _R16G16B16A16UInt;
        /// <summary>
        /// A four-component, 64-bit unsigned-integer format.
        /// </summary>
        public static Format<Vector4us> R16G16B16A16UInt
        {
            get
            {
                if (_R16G16B16A16UInt == null)
                    _R16G16B16A16UInt = new R16G16B16A16UInt();
                return _R16G16B16A16UInt;
            }
        }
        private static Format<Vector4s> _R16G16B16A16Int;
        /// <summary>
        /// A four-component, 64-bit signed-integer format.
        /// </summary>
        public static Format<Vector4s> R16G16B16A16Int
        {
            get
            {
                if (_R16G16B16A16Int == null)
                    _R16G16B16A16Int = new R16G16B16A16Int();
                return _R16G16B16A16Int;
            }
        }
        private static Format<Vector4us> _R16G16B16A16UNorm;
        /// <summary>
        /// A four-component, 64-bit unsigned-integer format.
        /// </summary>
        public static Format<Vector4us> R16G16B16A16UNorm
        {
            get
            {
                if (_R16G16B16A16UNorm == null)
                    _R16G16B16A16UNorm = new R16G16B16A16UNorm();
                return _R16G16B16A16UNorm;
            }
        }
        private static Format<Vector4s> _R16G16B16A16SNorm;
        /// <summary>
        /// A four-component, 64-bit signed-integer format.
        /// </summary>
        public static Format<Vector4s> R16G16B16A16SNorm
        {
            get
            {
                if (_R16G16B16A16SNorm == null)
                    _R16G16B16A16SNorm = new R16G16B16A16SNorm();
                return _R16G16B16A16SNorm;
            }
        }
        #endregion

        #region R8G8B8A8
        private static Format<Vector4b> _R8G8B8A8UInt;
        /// <summary>
        /// A four-component, 32-bit unsigned-integer format.
        /// </summary>
        public static Format<Vector4b> R8G8B8A8UInt
        {
            get
            {
                if (_R8G8B8A8UInt == null)
                    _R8G8B8A8UInt = new R8G8B8A8UInt();
                return _R8G8B8A8UInt;
            }
        }
        private static Format<Vector4sb> _R8G8B8A8Int;
        /// <summary>
        /// A four-component, 32-bit signed-integer format.
        /// </summary>
        public static Format<Vector4sb> R8G8B8A8Int
        {
            get
            {
                if (_R8G8B8A8Int == null)
                    _R8G8B8A8Int = new R8G8B8A8Int();
                return _R8G8B8A8Int;
            }
        }
        private static Format<Vector4b> _R8G8B8A8UNorm;
        /// <summary>
        /// A four-component, 32-bit unsigned-integer format.
        /// </summary>
        public static Format<Vector4b> R8G8B8A8UNorm
        {
            get
            {
                if (_R8G8B8A8UNorm == null)
                    _R8G8B8A8UNorm = new R8G8B8A8UNorm();
                return _R8G8B8A8UNorm;
            }
        }
        private static Format<Vector4sb> _R8G8B8A8SNorm;
        /// <summary>
        /// A four-component, 32-bit signed-integer format.
        /// </summary>
        public static Format<Vector4sb> R8G8B8A8SNorm
        {
            get
            {
                if (_R8G8B8A8SNorm == null)
                    _R8G8B8A8SNorm = new R8G8B8A8SNorm();
                return _R8G8B8A8SNorm;
            }
        }
        #endregion
       
        #region R32G32B32
        private static Format<Vector3f> _R32G32B32Float;
        /// <summary>
        /// A three-component, 96-bit floating-point format.
        /// </summary>
        public static Format<Vector3f> R32G32B32Float
        {
            get
            {
                if (_R32G32B32Float == null)
                    _R32G32B32Float = new R32G32B32Float();
                return _R32G32B32Float;
            }
        }
        private static Format<Vector3ui> _R32G32B32UInt;
        /// <summary>
        /// A three-component, 96-bit unsigned-integer format.
        /// </summary>
        public static Format<Vector3ui> R32G32B32UInt
        {
            get
            {
                if (_R32G32B32UInt == null)
                    _R32G32B32UInt = new R32G32B32UInt();
                return _R32G32B32UInt;
            }
        }
        private static Format<Vector3i> _R32G32B32Int;
        /// <summary>
        /// A three-component, 96-bit signed-integer format.
        /// </summary>
        public static Format<Vector3i> R32G32B32Int
        {
            get
            {
                if (_R32G32B32Int == null)
                    _R32G32B32Int = new R32G32B32Int();
                return _R32G32B32Int;
            }
        }
        #endregion

        #region R8G8B8
        private static Format<Vector3b> _R8G8B8UNorm;
        /// <summary>
        /// A three-component, 24-bit unsigned-normalized integer format.
        /// </summary>
        public static Format<Vector3b> R8G8B8UNorm
        {
            get
            {
                if (_R8G8B8UNorm == null)
                    _R8G8B8UNorm = new R8G8B8UNorm();
                return _R8G8B8UNorm;
            }
        }
        #endregion

        #region R32G32
        private static Format<Vector2f> _R32G32Float;
        /// <summary>
        /// A two-component, 32-bit floating-point format.
        /// </summary>
        public static Format<Vector2f> R32G32Float
        {
            get
            {
                if (_R32G32Float == null)
                    _R32G32Float = new R32G32Float();
                return _R32G32Float;
            }
        }
        private static Format<Vector2ui> _R32G32UInt;
        /// <summary>
        /// A two-component, 32-bit unsigned-integer format.
        /// </summary>
        public static Format<Vector2ui> R32G32UInt
        {
            get
            {
                if (_R32G32UInt == null)
                    _R32G32UInt = new R32G32UInt();
                return _R32G32UInt;
            }
        }
        private static Format<Vector2i> _R32G32Int;
        /// <summary>
        /// A two-component, 32-bit signed-integer format.
        /// </summary>
        public static Format<Vector2i> R32G32Int
        {
            get
            {
                if (_R32G32Int == null)
                    _R32G32Int = new R32G32Int();
                return _R32G32Int;
            }
        }
        private static Format<Vector2ui> _R32G32;
        /// <summary>
        /// A two-component, 32-bit unsigned-integer format.
        /// </summary>
        public static Format<Vector2ui> R32G32
        {
            get
            {
                if (_R32G32 == null)
                    _R32G32 = new R32G32();
                return _R32G32;
            }
        }
        private static Format<Vector2i> _R32G32Norm;
        /// <summary>
        /// A two-component, 32-bit signed-integer format.
        /// </summary>
        public static Format<Vector2i> R32G32Norm
        {
            get
            {
                if (_R32G32Norm == null)
                    _R32G32Norm = new R32G32Norm();
                return _R32G32Norm;
            }
        }
        #endregion

        #region R16G16
        private static Format<Vector2h> _R16G16Float;
        /// <summary>
        /// A two-component, 32-bit floating-point format.
        /// </summary>
        public static Format<Vector2h> R16G16Float
        {
            get
            {
                if (_R16G16Float == null)
                    _R16G16Float = new R16G16Float();
                return _R16G16Float;
            }
        }
        private static Format<Vector2us> _R16G16UInt;
        /// <summary>
        /// A two-component, 16-bit unsigned-integer format.
        /// </summary>
        public static Format<Vector2us> R16G16UInt
        {
            get
            {
                if (_R16G16UInt == null)
                    _R16G16UInt = new R16G16UInt();
                return _R16G16UInt;
            }
        }
        private static Format<Vector2s> _R16G16Int;
        /// <summary>
        /// A two-component, 16-bit signed-integer format.
        /// </summary>
        public static Format<Vector2s> R16G16Int
        {
            get
            {
                if (_R16G16Int == null)
                    _R16G16Int = new R16G16Int();
                return _R16G16Int;
            }
        }
        private static Format<Vector2us> _R16G16UNorm;
        /// <summary>
        /// A two-component, 16-bit unsigned-integer format.
        /// </summary>
        public static Format<Vector2us> R16G16UNorm
        {
            get
            {
                if (_R16G16UNorm == null)
                    _R16G16UNorm = new R16G16UNorm();
                return _R16G16UNorm;
            }
        }
        private static Format<Vector2s> _R16G16Norm;
        /// <summary>
        /// A two-component, 16-bit signed-integer format.
        /// </summary>
        public static Format<Vector2s> R16G16Norm
        {
            get
            {
                if (_R16G16Norm == null)
                    _R16G16Norm = new R16G16Norm();
                return _R16G16Norm;
            }
        }
        #endregion

        #region R8G8
        private static Format<Vector2b> _R8G8UInt;
        /// <summary>
        /// A two-component, 16-bit unsigned-integer format.
        /// </summary>
        public static Format<Vector2b> R8G8UInt
        {
            get
            {
                if (_R8G8UInt == null)
                    _R8G8UInt = new R8G8UInt();
                return _R8G8UInt;
            }
        }
        private static Format<Vector2sb> _R8G8Int;
        /// <summary>
        /// A two-component, 16-bit signed-integer format.
        /// </summary>
        public static Format<Vector2sb> R8G8Int
        {
            get
            {
                if (_R8G8Int == null)
                    _R8G8Int = new R8G8Int();
                return _R8G8Int;
            }
        }
        private static Format<Vector2b> _R8G8UNorm;
        /// <summary>
        /// A two-component, 16-bit unsigned-integer format.
        /// </summary>
        public static Format<Vector2b> R8G8UNorm
        {
            get
            {
                if (_R8G8UNorm == null)
                    _R8G8UNorm = new R8G8UNorm();
                return _R8G8UNorm;
            }
        }
        private static Format<Vector2sb> _R8G8Norm;
        /// <summary>
        /// A two-component, 16-bit signed-integer format.
        /// </summary>
        public static Format<Vector2sb> R8G8Norm
        {
            get
            {
                if (_R8G8Norm == null)
                    _R8G8Norm = new R8G8Norm();
                return _R8G8Norm;
            }
        }
        #endregion

        #region R32
        private static Format<float> _R32Float;
        /// <summary>
        /// A single-component, 32-bit floating-point format.
        /// </summary>
        public static Format<float> R32Float
        {
            get
            {
                if (_R32Float == null)
                    _R32Float = new R32Float();
                return _R32Float;
            }
        }
        private static Format<uint> _R32UInt;
        /// <summary>
        /// A single-component, 32-bit unsigned-integer format.
        /// </summary>
        public static Format<uint> R32UInt
        {
            get
            {
                if (_R32UInt == null)
                    _R32UInt = new R32UInt();
                return _R32UInt;
            }
        }
        private static Format<int> _R32Int;
        /// <summary>
        /// A single-component, 32-bit signed-integer format.
        /// </summary>
        public static Format<int> R32Int
        {
            get
            {
                if (_R32Int == null)
                    _R32Int = new R32Int();
                return _R32Int;
            }
        }
        #endregion

        #region R16
        private static Format<Half> _R16Float;
        /// <summary>
        /// A single-component, 16-bit floating-point format.
        /// </summary>
        public static Format<Half> R16Float
        {
            get
            {
                if (_R16Float == null)
                    _R16Float = new R16Float();
                return _R16Float;
            }
        }
        private static Format<ushort> _R16UInt;
        /// <summary>
        /// A single-component, 16-bit unsigned-integer format.
        /// </summary>
        public static Format<ushort> R16UInt
        {
            get
            {
                if (_R16UInt == null)
                    _R16UInt = new R16UInt();
                return _R16UInt;
            }
        }
        private static Format<short> _R16Int;
        /// <summary>
        /// A single-component, 16-bit signed-integer format.
        /// </summary>
        public static Format<short> R16Int
        {
            get
            {
                if (_R16Int == null)
                    _R16Int = new R16Int();
                return _R16Int;
            }
        }
        private static Format<ushort> _R16UNorm;
        /// <summary>
        /// A single-component, 16-bit unsigned-integer format.
        /// </summary>
        public static Format<ushort> R16UNorm
        {
            get
            {
                if (_R16UNorm == null)
                    _R16UNorm = new R16UNorm();
                return _R16UNorm;
            }
        }
        private static Format<short> _R16Norm;
        /// <summary>
        /// A single-component, 16-bit signed-integer format.
        /// </summary>
        public static Format<short> R16Norm
        {
            get
            {
                if (_R16Norm == null)
                    _R16Norm = new R16Norm();
                return _R16Norm;
            }
        }
        #endregion

        #region R8
        private static Format<byte> _R8UInt;
        /// <summary>
        /// A single-component, 8-bit unsigned-integer format.
        /// </summary>
        public static Format<byte> R8UInt
        {
            get
            {
                if (_R8UInt == null)
                    _R8UInt = new R8UInt();
                return _R8UInt;
            }
        }
        private static Format<sbyte> _R8Int;
        /// <summary>
        /// A single-component, 8-bit signed-integer format.
        /// </summary>
        public static Format<sbyte> R8Int
        {
            get
            {
                if (_R8Int == null)
                    _R8Int = new R8Int();
                return _R8Int;
            }
        }
        private static Format<byte> _R8UNorm;
        /// <summary>
        /// A single-component, 8-bit unsigned-integer format.
        /// </summary>
        public static Format<byte> R8UNorm
        {
            get
            {
                if (_R8UNorm == null)
                    _R8UNorm = new R8UNorm();
                return _R8UNorm;
            }
        }
        private static Format<sbyte> _R8Norm;
        /// <summary>
        /// A single-component, 8-bit signed-integer format.
        /// </summary>
        public static Format<sbyte> R8Norm
        {
            get
            {
                if (_R8Norm == null)
                    _R8Norm = new R8Norm();
                return _R8Norm;
            }
        }
        private static Format<byte> _A8UNorm;
        /// <summary>
        /// A single-component, 8-bit unsigned-integer format.
        /// </summary>
        public static Format<byte> A8UNorm
        {
            get
            {
                if (_A8UNorm == null)
                    _A8UNorm = new A8UNorm();
                return _A8UNorm;
            }
        }
        #endregion

        #region R10G10B10A2
        //        DXGI_FORMAT_R10G10B10A2_UNORM

        //    A four-component, 32-bit unsigned-integer format.
        //DXGI_FORMAT_R10G10B10A2_UINT

        //    A four-component, 32-bit unsigned-integer format.
        #endregion

        #region R11G11B10/R9G9B9E5
        //DXGI_FORMAT_R11G11B10_FLOAT

        //    Three partial-precision floating-point numbers encodeded into a single 32-bit value (a variant of s10e5). There are no sign bits, and there is a 5-bit biased (15) exponent for each channel, 6-bit mantissa for R and G, and a 5-bit mantissa for B, as shown in the following illustration.

        //    Illustration of the bits in the three partial-precision floating-point numbers
        //    DXGI_FORMAT_R9G9B9E5_SHAREDEXP

        //Three partial-precision floating-point numbers encoded into a single 32-bit value all sharing the same 5-bit exponent (variant of s10e5). There is no sign bit, and there is a shared 5-bit biased (15) exponent and a 9-bit mantissa for each channel, as shown in the following illustration. 2.

        //Illustration of the bits in the three partial-precision floating-point numbers
        #endregion

        #region R8G8_B8G8/G8R8_G8B8
//DXGI_FORMAT_R8G8_B8G8_UNORM

//    A four-component, 32-bit unsigned-normalized integer format. 3
//DXGI_FORMAT_G8R8_G8B8_UNORM

//    A four-component, 32-bit unsigned-normalized integer format. 3
        #endregion

        /*
DXGI_FORMAT_R32G8X24_TYPELESS

    A two-component, 64-bit typeless format.
DXGI_FORMAT_D32_FLOAT_S8X24_UINT

    A 32-bit floating-point component, and two unsigned-integer components (with an additional 32 bits).
DXGI_FORMAT_R32_FLOAT_X8X24_TYPELESS

    A 32-bit floating-point component, and two typeless components (with an additional 32 bits).
DXGI_FORMAT_X32_TYPELESS_G8X24_UINT

    A 32-bit typeless component, and two unsigned-integer components (with an additional 32 bits).

DXGI_FORMAT_R24G8_TYPELESS

    A two-component, 32-bit typeless format.
DXGI_FORMAT_D24_UNORM_S8_UINT

    A 32-bit z-buffer format that uses 24 bits for the depth channel and 8 bits for the stencil channel.
DXGI_FORMAT_R24_UNORM_X8_TYPELESS

    A 32-bit format, that contains a 24 bit, single-component, unsigned-normalized integer, with an additional typeless 8 bits.
DXGI_FORMAT_X24_TYPELESS_G8_UINT

    A 32-bit format, that contains a 24 bit, single-component, typeless format, with an additional 8 bit unsigned integer component.

DXGI_FORMAT_R1_UNORM

    A single-component, 1-bit unsigned-normalized integer format. 2.

DXGI_FORMAT_R10G10B10_XR_BIAS_A2_UNORM

    A four-component, 32-bit format that supports 2-bit alpha. 4

         */
    }

    //[ContractClassFor(typeof(Format))]
    //internal abstract class FormatContracts : Format
    //{
    //    public FormatContracts()
    //        : base(string.Empty, Colord.White, Colord.Black, false)
    //    {
    //    }

    //    public override Size3i GetPhysicalSize(Size3i size)
    //    {
    //        Contract.Ensures(Contract.Result<Size3i>().Width > 0, "width must be greater than 0");
    //        Contract.Ensures(Contract.Result<Size3i>().Height > 0, "height must be greater than 0");
    //        Contract.Ensures(Contract.Result<Size3i>().Depth > 0, "depth must be greater than 0");

    //        throw new NotImplementedException();
    //    }

    //    public override int GetByteCount(Size3i size, out int rowPitch, out int slicePitch)
    //    {
    //        Contract.Requires(size.Width > 0, "width must be greater than 0");
    //        Contract.Requires(size.Height > 0, "height must be greater than 0");
    //        Contract.Requires(size.Depth > 0, "depth must be greater than 0");
    //        Contract.Ensures(Contract.ValueAtReturn(out rowPitch) > 0, "rowPitch must be greater than 0");
    //        Contract.Ensures(Contract.ValueAtReturn(out slicePitch) > 0, "slicePitch must be greater than 0");
    //        Contract.Ensures(Contract.Result<int>() > 0, "result must be greater than 0");

    //        throw new NotImplementedException();
    //    }

    //    public override void GetBytes(
    //        Colord[] source, int index, int width, int height, 
    //        Stream destination, int rowPitch, int slicePitch, 
    //        Boxi sourceBoxi, Point3i destinationPoint)
    //    {
    //        Contract.Requires(source != null, "source cannot be null.");
    //        Contract.Requires(index >= 0, "index must be zero or greater.");
    //        Contract.Requires(width > 0, "width must be greater than 0");
    //        Contract.Requires(height > 0, "height must be greater than 0");
    //        Contract.Requires(width <= destinationPoint.X + sourceBoxi.Width);
    //        Contract.Requires(height <= destinationPoint.Y + sourceBoxi.Height);
    //        Contract.Requires(
    //            index + 
    //            destinationPoint.X * sourceBoxi.Width +
    //            destinationPoint.Y * sourceBoxi.Width * sourceBoxi.Height +
    //            destinationPoint.Z * sourceBoxi.Width * sourceBoxi.Height * sourceBoxi.Depth
    //            <= source.Length, "index, sourceBoxi and destinationPoint must specify a valid range in source.");

    //        Contract.Requires(destination != null, "destination cannot be null.");
    //        Contract.Requires(destination.CanWrite, "destination must be writeable.");
    //        Contract.Requires(rowPitch > 0, "rowPitch must be greater than 0");
    //        Contract.Requires(slicePitch > 0, "slicePitch must be greater than 0");
    //        Contract.Requires(GetByteCount(destinationSize) <= destination.Length, "destinationSize must specify a valid range in destination.");

    //        Contract.Requires(sourceBoxi.X >= 0, "sourceBoxi.X must be zero or greater.");
    //        Contract.Requires(sourceBoxi.Y >= 0, "sourceBoxi.Y must be zero or greater.");
    //        Contract.Requires(sourceBoxi.Z >= 0, "sourceBoxi.Z must be zero or greater.");
    //        Contract.Requires(sourceBoxi.Width > 0, "sourceBoxi.Width must be greater than 0.");
    //        Contract.Requires(sourceBoxi.Height > 0, "sourceBoxi.Height must be greater than 0.");
    //        Contract.Requires(sourceBoxi.Depth > 0, "sourceBoxi.Depth must be greater than 0.");
    //        Contract.Requires(sourceBoxi.Right <= width, "sourceBoxi.Right must be less than or equal to width.");
    //        Contract.Requires(sourceBoxi.Top <= height, "sourceBoxi.Top must be less than or equal to height.");
    //        Contract.Requires(sourceBoxi.Back <= sourceSize.Depth, "sourceBoxi.Back must be less than or equal to sourceSize.Depth.");

    //        Contract.Requires(destinationPoint.X >= 0, "destinationPoint.X must be zero or greater.");
    //        Contract.Requires(destinationPoint.Y >= 0, "destinationPoint.Y must be zero or greater.");
    //        Contract.Requires(destinationPoint.Z >= 0, "destinationPoint.Z must be zero or greater.");
    //        Contract.Requires(destinationPoint.X + sourceBoxi.Width <= width, "destinationPoint.X + sourceBoxi.Width must be less than or equal to width.");
    //        Contract.Requires(destinationPoint.Y + sourceBoxi.Height <= height, "destinationPoint.Y + sourceBoxi.Height must be less than or equal to height.");
    //        Contract.Requires(destinationPoint.Z + sourceBoxi.Depth <= destinationSize.Depth, "destinationPoint.Z + sourceBoxi.Depth must be less than or equal to destinationSize.Depth.");
            
    //        throw new NotImplementedException();
    //    }

    //    public override int GetColordCount(int byteCount)
    //    {
    //        Contract.Requires(byteCount >= 0, "byteCount must be zero or greater.");

    //        throw new NotImplementedException();
    //    }

    //    public override void GetColords(Stream source, int rowPitch, int slicePitch, Size3i sourceSize, Colord[] destination, int index, Size3i destinationSize, Boxi sourceBoxi, Point3i destinationPoint)
    //    {
    //        Contract.Requires(source != null, "source cannot be null.");
    //        Contract.Requires(source.CanRead, "source must be readable.");
    //        Contract.Requires(rowPitch > 0, "rowPitch must be greater than 0");
    //        Contract.Requires(slicePitch > 0, "slicePitch must be greater than 0");
    //        Contract.Requires(width > 0, "width must be greater than 0");
    //        Contract.Requires(height > 0, "height must be greater than 0");
    //        Contract.Requires(sourceSize.Depth > 0, "sourceSize.Depth must be greater than 0");
    //        Contract.Requires(GetByteCount(sourceSize) <= source.Length, "sourceSize must specify a valid range in source.");

    //        Contract.Requires(destination != null, "destination cannot be null.");
    //        Contract.Requires(index >= 0, "index must be zero or greater.");
    //        Contract.Requires(width > 0, "width must be greater than 0");
    //        Contract.Requires(height > 0, "height must be greater than 0");
    //        Contract.Requires(destinationSize.Depth > 0, "destinationSize.Depth must be greater than 0");
    //        Contract.Requires(index + (width * height * destinationSize.Depth) <= destination.Length, "index and destinationSize must specify a valid range in destination.");

    //        Contract.Requires(sourceBoxi.X >= 0, "sourceBoxi.X must be zero or greater.");
    //        Contract.Requires(sourceBoxi.Y >= 0, "sourceBoxi.Y must be zero or greater.");
    //        Contract.Requires(sourceBoxi.Z >= 0, "sourceBoxi.Z must be zero or greater.");
    //        Contract.Requires(sourceBoxi.Width > 0, "sourceBoxi.Width must be greater than 0.");
    //        Contract.Requires(sourceBoxi.Height > 0, "sourceBoxi.Height must be greater than 0.");
    //        Contract.Requires(sourceBoxi.Depth > 0, "sourceBoxi.Depth must be greater than 0.");
    //        Contract.Requires(sourceBoxi.Right <= width, "sourceBoxi.Right must be less than or equal to width.");
    //        Contract.Requires(sourceBoxi.Top <= height, "sourceBoxi.Top must be less than or equal to height.");
    //        Contract.Requires(sourceBoxi.Back <= sourceSize.Depth, "sourceBoxi.Back must be less than or equal to sourceSize.Depth.");

    //        Contract.Requires(destinationPoint.X >= 0, "destinationPoint.X must be zero or greater.");
    //        Contract.Requires(destinationPoint.Y >= 0, "destinationPoint.Y must be zero or greater.");
    //        Contract.Requires(destinationPoint.Z >= 0, "destinationPoint.Z must be zero or greater.");
    //        Contract.Requires(destinationPoint.X + sourceBoxi.Width <= width, "destinationPoint.X + sourceBoxi.Width must be less than or equal to width.");
    //        Contract.Requires(destinationPoint.Y + sourceBoxi.Height <= height, "destinationPoint.Y + sourceBoxi.Height must be less than or equal to height.");
    //        Contract.Requires(destinationPoint.Z + sourceBoxi.Depth <= destinationSize.Depth, "destinationPoint.Z + sourceBoxi.Depth must be less than or equal to destinationSize.Depth.");

    //        throw new NotImplementedException();
    //    }
    //}
}
