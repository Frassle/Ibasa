using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Ibasa.Numerics;
using Ibasa.Numerics.Geometry;

namespace Ibasa.Valve.Model
{
    public sealed class Phy
    {
        public Phy(string path)
        {
            using (var stream = File.OpenRead(path))
            {
                Load(stream);
            }
        }
        public Phy(Stream stream)
        {
            Load(stream);
        }

        public int Id { get; private set; }
        public int Checksum { get; private set; }

        private void Load(Stream stream)
        {
            BinaryReader reader = new BinaryReader(stream, Encoding.ASCII);

            if (reader.ReadInt32() != 16)
                throw new InvalidDataException("File header is not 16.");

            Id = reader.ReadInt32();
            int solids = reader.ReadInt32();
            Checksum = reader.ReadInt32();


            for (int i = 0; i < solids; ++i)
            {
                int solidSize = reader.ReadInt32();
                string vphysics = Encoding.ASCII.GetString(reader.ReadBytes(4));
                int version = reader.ReadUInt16();
                int modelType = reader.ReadUInt16();
                int surfaceSize = reader.ReadInt32();
                Vector3f dragAxisAreas = new Vector3f(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
                int axisMapSize = reader.ReadInt32();

                if (vphysics == "VPHY")
                {

                }
            }
        }
    }
}
