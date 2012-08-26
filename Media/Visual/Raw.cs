using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ibasa.SharpIL;
using System.IO;
using Ibasa.Numerics;
using Ibasa.Numerics.Geometry;

namespace Ibasa.Media.Visual
{
    public sealed class Raw
    {
        #region Image
        private Resource property_Image;
        /// <summary>
        /// 
        /// </summary>
        public Resource Image
        {
            get { return property_Image; }
            set
            {
                global::System.Diagnostics.Contracts.Contract.Requires(value != null);
                global::System.Diagnostics.Contracts.Contract.Requires(value.MipLevels == 1);
                global::System.Diagnostics.Contracts.Contract.Requires(value.ArraySize == 1);

                property_Image = value;
            }
        }
        #endregion        
        
        public Raw(string path, Size3i size, Format format)
        {
            using (var stream = File.OpenRead(path))
            {
                Load(stream, size, format);
            }
        }

        public Raw(Stream stream, Size3i size, Format format)
        {
            Load(stream, size, format);
        }

        private void Load(Stream stream, Size3i size, Format format)
        {
            BinaryReader reader = new BinaryReader(stream, Encoding.ASCII);

            Resource resource = new Resource(size, 1, 1, format);

            int count = format.GetByteCount(size);
            int offset = 0;

            while(count != 0)
            {
                int read = stream.Read(resource[0], offset, count);
                if (read == 0)
                    throw new EndOfStreamException();
                offset += read;
                count -= read;
            }
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
            stream.Write(Image[0], 0, Image[0].Length);
        }
    }
}
