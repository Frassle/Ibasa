using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ibasa.SharpIL;
using System.IO;
using Ibasa.Numerics;
using Ibasa.IO;

namespace Ibasa.Media.Visual
{
    public sealed class Bitmap
    {
        #region Image
        private Resource property_Image;
        /// <summary>
        /// The data for this bitmap.
        /// </summary>
        public Resource Image
        {
            get { return property_Image; }
            set
            {
                global::System.Diagnostics.Contracts.Contract.Requires(value != null);
                global::System.Diagnostics.Contracts.Contract.Requires(value.MipLevels == 1);
                global::System.Diagnostics.Contracts.Contract.Requires(value.ArraySize == 1);

                global::System.Diagnostics.Contracts.Contract.Requires(
                    value.Format is SharpIL.Formats.B8G8R8UNorm ||
                    value.Format is SharpIL.Formats.B5G6R5UNorm);

                property_Image = value;
            }
        }
        #endregion

        public Bitmap(Resource image)
        {
            Image = image;
        }
        
        public Bitmap(string path)
        {
            using (var stream = File.OpenRead(path))
            {
                Load(stream);
            }
        }

        public Bitmap(Stream stream)
        {
            Load(stream);
        }

        static Ibasa.Collections.Bimap<System.Drawing.Imaging.PixelFormat, Format> FormatMap;
        static Bitmap()
        {
            FormatMap = new Collections.Bimap<System.Drawing.Imaging.PixelFormat, Format>();
            FormatMap.AddLeft(System.Drawing.Imaging.PixelFormat.Format32bppArgb, Format.B8G8R8A8UNorm);
            FormatMap.AddLeft(System.Drawing.Imaging.PixelFormat.Format32bppPArgb, Format.B8G8R8A8UNorm);
        }

        private void Load(Stream stream)
        {
            var bitmap = System.Drawing.Bitmap.FromStream(stream) as System.Drawing.Bitmap;

            Image = new Resource(new Numerics.Geometry.Size3i(bitmap.Width, bitmap.Height, 1), 1, 1, FormatMap.GetLeft(bitmap.PixelFormat).First());

            var data = bitmap.LockBits(new System.Drawing.Rectangle(0,0,bitmap.Width, bitmap.Height), System.Drawing.Imaging.ImageLockMode.ReadOnly, bitmap.PixelFormat);
            var bpp = Image.Format.GetByteCount(Numerics.Geometry.Size3i.Unit);
            for (int y = 0; y < Image.Height; ++y)
            {
                System.Runtime.InteropServices.Marshal.Copy(data.Scan0 + data.Stride * y, Image[0], y * Image.Width * bpp, data.Stride);
            }

            bitmap.UnlockBits(data);
        }

        public void Save(string path)
        {
            using (var stream = File.Open(path, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                Save(stream);
            }
        }

        public void Save(Stream stream)
        {
            var bitmap = new System.Drawing.Bitmap(Image.Width, Image.Height, FormatMap.GetRight(Image.Format).First());

            var data = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height), System.Drawing.Imaging.ImageLockMode.WriteOnly, bitmap.PixelFormat);
            var bpp = Image.Format.GetByteCount(Numerics.Geometry.Size3i.Unit);
            for (int y = 0; y < Image.Height; ++y)
            {
                System.Runtime.InteropServices.Marshal.Copy(Image[0], y * Image.Width * bpp, data.Scan0 + data.Stride * y, data.Stride);
            }

            bitmap.UnlockBits(data);

            bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);
        }
    }
}
