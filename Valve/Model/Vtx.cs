using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Ibasa.Collections.Immutable;

namespace Ibasa.Valve.Model
{
    public sealed class Vtx
    {
        public struct MaterialReplacement
        {
            internal MaterialReplacement(int id, string name)
            {
                Id = id;
                Name = name;
            }

            public readonly int Id;
            public readonly string Name;
        }

        public struct BoneStateChange
        {
            internal BoneStateChange(int hardwareId, int newBoneId)
            {
                HardwareId = hardwareId;
                NewBoneId = newBoneId;
            }

            public readonly int HardwareId;
            public readonly int NewBoneId;
        }

        public struct Vertex
        {
            internal Vertex(ImmutableArray<int> boneWeightIndices, int originalMeshVertexIndex, ImmutableArray<int> boneIndices)
            {
                BoneWeightIndices = boneWeightIndices;
                OriginalMeshVertexIndex = originalMeshVertexIndex;
                BoneIndices = boneIndices;
            }

            public readonly ImmutableArray<int> BoneWeightIndices;
            public readonly int OriginalMeshVertexIndex;
            public readonly ImmutableArray<int> BoneIndices;
        }

        [Flags]
        public enum StripFlags
        {
            IsTriList = 0x01,
            IsTriStrip= 0x02,
        }

        public struct Strip
        {
            internal Strip(int indexCount, int indexOffset, int vertexCount, int vertexOffset, int boneCount, StripFlags flags,
                ImmutableArray<BoneStateChange> boneStateChanges)
            {
                IndexCount = indexCount;
                IndexOffset = indexOffset;

                VertexCount = vertexCount;
                VertexOffset = vertexOffset;

                BoneCount = boneCount;

                Flags = flags;

                BoneStateChanges = boneStateChanges;
            }

            public readonly int IndexCount;
            public readonly int IndexOffset;
            
            public readonly int VertexCount;
            public readonly int VertexOffset;

            public readonly int BoneCount;

            public readonly StripFlags Flags;

            public readonly ImmutableArray<BoneStateChange> BoneStateChanges;
        }

        [Flags]
        public enum StripGroupFlags
        {
            None = 0x00,
            IsFlexed = 0x01,
            IsHardwareSkinned = 0x02,
            IsDeltaFlexed = 0x04,
        }

        public struct StripGroup
        {
            internal StripGroup(
                ImmutableArray<Vertex> vertices, ImmutableArray<int> indices, 
                StripGroupFlags flags, ImmutableArray<Strip> strips)
            {
                Vertices = vertices;
                Indices = indices;
                Flags = flags;
                Strips = strips;
            }

            public readonly ImmutableArray<Vertex> Vertices;
            public readonly ImmutableArray<int> Indices;
            public readonly StripGroupFlags Flags;
            public readonly ImmutableArray<Strip> Strips;
        }

        [Flags]
        public enum MeshFlags
        {
            IsTeeth = 0x01,
            IsEyes = 0x02,
        }

        public struct Mesh
        {
            internal Mesh(MeshFlags flags, ImmutableArray<StripGroup> stripGroups)
            {
                Flags = flags;
                StripGroups = stripGroups;
            }

            public readonly MeshFlags Flags;
            public readonly ImmutableArray<StripGroup> StripGroups;
        }

        public struct Lod
        {
            internal Lod(float switchPoint, ImmutableArray<Mesh> meshes)
            {
                SwitchPoint = switchPoint;
                Meshes = meshes;
            }

            public readonly float SwitchPoint;
            public readonly ImmutableArray<Mesh> Meshes;
        }

        public struct Model
        {
            internal Model(ImmutableArray<Lod> lods)
            {
                Lods = lods;
            }

            public readonly ImmutableArray<Lod> Lods;
        }

        public struct BodyPart
        {
            internal BodyPart(ImmutableArray<Model> models)
            {
                Models = models;
            }

            public readonly ImmutableArray<Model> Models;
        }
        
        public Vtx(string path)
        {
            using (var stream = File.OpenRead(path))
            {
                Load(stream);
            }
        }
        public Vtx(Stream stream)
        {
            Load(stream);
        }

        public int Version { get; private set; }
        public int VertexCacheSize { get; private set; }
        public int MaxBonesPerStrip { get; private set; }
        public int MaxBonesPerTri { get; private set; }
        public int MaxBonesPerVert { get; private set; }
        public int Checksum { get; private set; }
        public int LodCount { get; private set; }
        public ImmutableArray<ImmutableArray<MaterialReplacement>> MaterialReplacements { get; private set; }

        public ImmutableArray<BodyPart> BodyParts { get; private set; }

        private void Load(Stream stream)
        {
            BinaryReader reader = new BinaryReader(stream, Encoding.ASCII);
            long basePosition = reader.BaseStream.Position;

            Version = reader.ReadInt32();

            if (Version != 7)
                throw new InvalidDataException(string.Format("File version {0} does not match 7.", Version));

            VertexCacheSize = reader.ReadInt32();
            MaxBonesPerStrip = reader.ReadUInt16();
            MaxBonesPerTri = reader.ReadUInt16();
            MaxBonesPerVert = reader.ReadInt32();
            Checksum = reader.ReadInt32();
            LodCount = reader.ReadInt32();

            long materialOffset = basePosition + reader.ReadInt32();

            int bodyPartsCount = reader.ReadInt32();
            long bodyPartsOffset = basePosition + reader.ReadInt32();

            reader.BaseStream.Position = materialOffset;
            LoadMaterialReplacements(reader);

            reader.BaseStream.Position = bodyPartsOffset;
            BodyParts = LoadBodyParts(reader, bodyPartsCount);
        }

        private void LoadMaterialReplacements(BinaryReader reader)
        {
            StringBuilder builder = new StringBuilder();

            int[] counts = new int[LodCount];
            long[] offsets = new long[LodCount];
            for (int i = 0; i < LodCount; ++i)
            {
                long basePosition = reader.BaseStream.Position;
                counts[i] = reader.ReadInt32();
                offsets[i] = basePosition + reader.ReadInt32();
            }

            ImmutableArray<MaterialReplacement>[] materialReplacements = new ImmutableArray<MaterialReplacement>[LodCount];

            for (int i = 0; i < LodCount; ++i)
            {
                reader.BaseStream.Position = offsets[i];

                int[] ids = new int[counts[i]];
                long[] nameOffsets = new long[counts[i]];

                for (int j = 0; j < counts[i]; ++j)
                {
                    ids[j] = reader.ReadUInt16();
                    nameOffsets[j] = offsets[i] + reader.ReadInt32();
                }

                MaterialReplacement[] replacements = new MaterialReplacement[counts[i]];

                for (int j = 0; j < counts[i]; ++j)
                {
                    reader.BaseStream.Position = nameOffsets[j];
                    builder.Clear();
                    for (char c; (c = reader.ReadChar()) != '\0'; )
                    {
                        builder.Append(c);
                    }

                    replacements[j] = new MaterialReplacement(ids[j], builder.ToString());
                }

                materialReplacements[i] = new ImmutableArray<MaterialReplacement>(replacements);
            }
            MaterialReplacements = new ImmutableArray<ImmutableArray<MaterialReplacement>>(materialReplacements);
        }

        private ImmutableArray<BodyPart> LoadBodyParts(BinaryReader reader, int count)
        {
            BodyPart[] bodyParts = new BodyPart[count];
            for (int i = 0; i < count; ++i)
            {
                long offset = reader.BaseStream.Position;

                int modelCount = reader.ReadInt32();
                long modelOffset = offset + reader.ReadInt32();

                long save = reader.BaseStream.Position;
                reader.BaseStream.Position = modelOffset;
                ImmutableArray<Model> models = LoadModels(reader, modelCount);
                reader.BaseStream.Position = save;

                bodyParts[i] = new BodyPart(models);
            }
            return new ImmutableArray<BodyPart>(bodyParts);
        }

        private ImmutableArray<Model> LoadModels(BinaryReader reader, int count)
        {
            Model[] models = new Model[count];
            for (int i = 0; i < count; ++i)
            {
                long offset = reader.BaseStream.Position;

                int lodCount = reader.ReadInt32();
                long lodOffset = offset + reader.ReadInt32();

                long save = reader.BaseStream.Position;
                reader.BaseStream.Position = lodOffset;
                ImmutableArray<Lod> lods = LoadLods(reader, lodCount);
                reader.BaseStream.Position = save;

                models[i] = new Model(lods);
            }
            return new ImmutableArray<Model>(models);
        }

        private ImmutableArray<Lod> LoadLods(BinaryReader reader, int count)
        {
            Lod[] lods = new Lod[count];
            for (int i = 0; i < count; ++i)
            {
                long offset = reader.BaseStream.Position;

                int meshCount = reader.ReadInt32();
                long meshOffset = offset + reader.ReadInt32();
                float switchPoint = reader.ReadSingle();

                long save = reader.BaseStream.Position;
                reader.BaseStream.Position = meshOffset;
                ImmutableArray<Mesh> meshes = LoadMeshes(reader, meshCount);
                reader.BaseStream.Position = save;

                lods[i] = new Lod(switchPoint, meshes);
            }
            return new ImmutableArray<Lod>(lods);
        }

        private ImmutableArray<Mesh> LoadMeshes(BinaryReader reader, int count)
        {
            Mesh[] meshes = new Mesh[count];
            for (int i = 0; i < count; ++i)
            {
                long offset = reader.BaseStream.Position;

                int stripGroupCount = reader.ReadInt32();
                long stripGroupOffset = offset + reader.ReadInt32();
                MeshFlags flags = (MeshFlags)reader.ReadByte();

                long save = reader.BaseStream.Position;
                reader.BaseStream.Position = stripGroupOffset;
                ImmutableArray<StripGroup> stripGroups = LoadStripGroups(reader, stripGroupCount);
                reader.BaseStream.Position = save;

                meshes[i] = new Mesh(flags, stripGroups);
            }
            return new ImmutableArray<Mesh>(meshes);
        }

        private ImmutableArray<StripGroup> LoadStripGroups(BinaryReader reader, int count)
        {
            StripGroup[] stripGroups = new StripGroup[count];
            for (int i = 0; i < count; ++i)
            {
                long offset = reader.BaseStream.Position;

                int vertexCount = reader.ReadInt32();
                long vertexOffset = offset + reader.ReadInt32();

                int indexCount = reader.ReadInt32();
                long indexOffset = offset + reader.ReadInt32();

                int stripCount = reader.ReadInt32();
                long stripOffset = offset + reader.ReadInt32();

                StripGroupFlags flags = (StripGroupFlags)reader.ReadByte();

                long save = reader.BaseStream.Position;
                reader.BaseStream.Position = vertexOffset;
                ImmutableArray<Vertex> vertices = LoadVertices(reader, vertexCount);
                reader.BaseStream.Position = indexOffset;
                ImmutableArray<int> indices = LoadIndices(reader, indexCount);
                reader.BaseStream.Position = stripOffset;
                ImmutableArray<Strip> strips = LoadStrips(reader, stripCount);
                reader.BaseStream.Position = save;

                stripGroups[i] = new StripGroup(vertices, indices, flags, strips);
            }
            return new ImmutableArray<StripGroup>(stripGroups);
        }

        private ImmutableArray<Strip> LoadStrips(BinaryReader reader, int count)
        {
            Strip[] strips = new Strip[count];
            for (int i = 0; i < count; ++i)
            {
                long offset = reader.BaseStream.Position;

                int indexCount = reader.ReadInt32();
                int indexOffset = reader.ReadInt32();
                int vertexCount = reader.ReadInt32();
                int vertexOffset = reader.ReadInt32();
                int boneCount = reader.ReadUInt16();
                StripFlags flags = (StripFlags)reader.ReadByte();
                int boneStateChangesCount = reader.ReadInt32();
                long boneStateChangesOffset = offset + reader.ReadInt32();

                long save = reader.BaseStream.Position;
                reader.BaseStream.Position = boneStateChangesOffset;

                strips[i] = new Strip(indexCount, indexOffset, vertexCount, vertexOffset, boneCount, flags,
                    LoadBoneStateChanges(reader, boneStateChangesCount));

                reader.BaseStream.Position = save;
            }
            return new ImmutableArray<Strip>(strips);
        }

        private ImmutableArray<BoneStateChange> LoadBoneStateChanges(BinaryReader reader, int count)
        {
            BoneStateChange[] boneStateChanges = new BoneStateChange[count];
            for (int i = 0; i < count; ++i)
            {
                boneStateChanges[i] = new BoneStateChange(reader.ReadInt32(), reader.ReadInt32());
            }
            return new ImmutableArray<BoneStateChange>(boneStateChanges);
        }

        private ImmutableArray<Vertex> LoadVertices(BinaryReader reader, int count)
        {
            Vertex[] vertices = new Vertex[count];
            for (int i = 0; i < count; ++i)
            {
                byte boneWeightIndex0 = reader.ReadByte();
                byte boneWeightIndex1 = reader.ReadByte();
                byte boneWeightIndex2 = reader.ReadByte();
                byte boneCount = reader.ReadByte();
                ushort index = reader.ReadUInt16();
                byte boneIndex0 = reader.ReadByte();
                byte boneIndex1 = reader.ReadByte();
                byte boneIndex2 = reader.ReadByte();

                int[] boneWeightIndices = new int[boneCount];
                int[] boneIndices = new int[boneCount];

                switch (boneCount)
                {
                    case 1:
                        boneWeightIndices[0] = boneWeightIndex0;
                        boneIndices[0] = boneIndex0;
                        break;
                    case 2:
                        boneWeightIndices[0] = boneWeightIndex0;
                        boneWeightIndices[1] = boneWeightIndex1;
                        boneIndices[0] = boneIndex0;
                        boneIndices[1] = boneIndex1;
                        break;
                    case 3:
                        boneWeightIndices[0] = boneWeightIndex0;
                        boneWeightIndices[1] = boneWeightIndex1;
                        boneWeightIndices[2] = boneWeightIndex2;
                        boneIndices[0] = boneIndex0;
                        boneIndices[1] = boneIndex1;
                        boneIndices[2] = boneIndex2;
                        break;
                }

                vertices[i] = new Vertex(
                    new ImmutableArray<int>(boneWeightIndices),
                    index,
                    new ImmutableArray<int>(boneIndices));
            }
            return new ImmutableArray<Vertex>(vertices);
        }

        private ImmutableArray<int> LoadIndices(BinaryReader reader, int count)
        {
            int[] indices = new int[count];
            for (int i = 0; i < count; ++i)
            {
                indices[i] = reader.ReadUInt16();
            }
            return new ImmutableArray<int>(indices);
        }
    }
}
