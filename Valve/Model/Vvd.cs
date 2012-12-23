using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Ibasa.Numerics;
using Ibasa.Collections.Immutable;
using Ibasa.Numerics.Geometry;

namespace Ibasa.Valve.Model
{
    public sealed class Vvd
    {
        public struct BoneWeight
        {
            internal BoneWeight(double weight, int bone)
            {
                Weight = weight;
                Bone = bone;
            }

            public readonly double Weight;
            public readonly int Bone;
        }

        public struct Vertex
        {
            internal Vertex(ImmutableArray<BoneWeight> boneWeights,
                Vector3f position, Vector3f normal, Vector2f textureCoordinates, Vector4f tangent)
            {
                BoneWeights = boneWeights;
                Position = position;
                Normal = normal;
                TextureCoordinates = textureCoordinates;
                Tangent = tangent;
            }

            public readonly ImmutableArray<BoneWeight> BoneWeights;
            public readonly Vector3f Position;
            public readonly Vector3f Normal;
            public readonly Vector2f TextureCoordinates;
            public readonly Vector4f Tangent;
        }

        const int MaxNumberOfLods = 8;

        public Vvd(string path)
        {
            using (var stream = File.OpenRead(path))
            {
                Load(stream);
            }
        }
        public Vvd(Stream stream)
        {
            Load(stream);
        }

        public int Version { get; private set; }
        public int Checksum { get; private set; }
        public int Lods { get; private set; }
        public ImmutableArray<int> VertexCounts { get; private set; }
        public ImmutableArray<Vertex> Vertices { get; private set; }

        struct Fixup
        {
            public int Lod;
            public int SourceVertexID;
            public int VertexCount;
        }

        private void Load(Stream stream)
        {
            BinaryReader reader = new BinaryReader(stream, Encoding.ASCII);
            long basePosition = reader.BaseStream.Position;

            string signature = Encoding.ASCII.GetString(reader.ReadBytes(4));

            if (signature != "IDSV")
                throw new InvalidDataException("File signature does not match 'IDSV'.");

            Version = reader.ReadInt32();

            if (Version != 4)
                throw new InvalidDataException(string.Format("File version {0} does not match 4.", Version));

            Checksum = reader.ReadInt32();
            Lods = reader.ReadInt32();

            VertexCounts = new ImmutableArray<int>(MaxNumberOfLods);
            for (int i = 0; i < MaxNumberOfLods; ++i)
            {
                VertexCounts = VertexCounts.SetValue(reader.ReadInt32(), i);
            }

            Fixup[] fixups = new Fixup[reader.ReadInt32()];
            long fixupOffset = reader.ReadInt32();
            long vertexDataOffset = reader.ReadInt32();
            long tangentDataOffset = reader.ReadInt32();
            
            reader.BaseStream.Position = basePosition + fixupOffset;

            for (int i = 0; i < fixups.Length; ++i)
            {
                fixups[i].Lod = reader.ReadInt32();
                fixups[i].SourceVertexID = reader.ReadInt32();
                fixups[i].VertexCount = reader.ReadInt32();
            }

            long vertexCount = (tangentDataOffset - vertexDataOffset) / 48;

            ImmutableArray<BoneWeight>[] boneWeights = new ImmutableArray<BoneWeight>[vertexCount];
            Vector3f[] positions = new Vector3f[vertexCount];
            Vector3f[] normals = new Vector3f[vertexCount];
            Vector2f[] textureCoordinates = new Vector2f[vertexCount];
            Vector4f[] tangents = new Vector4f[vertexCount];

            reader.BaseStream.Position = basePosition + vertexDataOffset;
            for (int v = 0; v < vertexCount; ++v)
            {
                float boneWeight0 = reader.ReadSingle();
                float boneWeight1 = reader.ReadSingle();
                float boneWeight2 = reader.ReadSingle();
                byte bone0 = reader.ReadByte();
                byte bone1 = reader.ReadByte();
                byte bone2 = reader.ReadByte();

                byte boneCount = reader.ReadByte();

                switch (boneCount)
                {
                    case 0:
                        boneWeights[v] = new ImmutableArray<BoneWeight>(0);
                        break;
                    case 1:
                        boneWeights[v] = new ImmutableArray<BoneWeight>(new BoneWeight[] {
                            new BoneWeight(boneWeight0, bone0) });
                        break;
                    case 2:
                        boneWeights[v] = new ImmutableArray<BoneWeight>(new BoneWeight[] {
                            new BoneWeight(boneWeight0, bone0),
                            new BoneWeight(boneWeight1, bone1)});
                        break;
                    case 3:
                        boneWeights[v] = new ImmutableArray<BoneWeight>(new BoneWeight[] {
                            new BoneWeight(boneWeight0, bone0),
                            new BoneWeight(boneWeight1, bone1),
                            new BoneWeight(boneWeight2, bone2)});
                        break;
                }

                positions[v] = new Vector3f(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
                normals[v] = new Vector3f(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
                textureCoordinates[v] = new Vector2f(reader.ReadSingle(), reader.ReadSingle());
            }
            
            reader.BaseStream.Position = basePosition + tangentDataOffset;
            for (int v = 0; v < vertexCount; ++v)
            {
                tangents[v] = new Vector4f(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
            }

            Vertex[] vertices = new Vertex[vertexCount];

            for(int v=0;v<vertices.Length;++v)
            {
                vertices[v] = new Vertex(boneWeights[v], positions[v], normals[v], textureCoordinates[v], tangents[v]);
            }

            Vertices = new ImmutableArray<Vertex>(vertices);
        }
    }
}
