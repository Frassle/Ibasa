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
    public sealed class Mdl
    {
        public struct Texture1
        {
            public string Name;
            public int Flags;
            public int Used;
        }

        public enum BoneControllerType
        {
            X, Y, Z, XR, YR, ZR, M
        }

        public struct BoneController
        {
            internal BoneController(int bone, BoneControllerType type, float start, float end, int rest, int inputField)
            {
                Bone = bone;
                Type = type;
                Start = start;
                End = end;
                Rest = rest;
                InputField = inputField;
            }

            public readonly int Bone;
            public readonly BoneControllerType Type;
            public readonly float Start;
            public readonly float End;
            public readonly int Rest;
            public readonly int InputField;
        }

        public struct Bone
        {
            internal Bone(string name, int parent, ImmutableArray<int> boneControllers,
                Vector3f position, Quaternionf quaternion, Vector3f rotation, Vector3f positionScale, Vector3f rotationScale,
                Matrix4x4f poseToBone, Quaternionf alignment,
                int flags, int procType, int procIndex, int physicsBone, int surfacePropIdx, int contents)
            {
                Name = name;
                Parent = parent;
                BoneControllers = boneControllers;

                Position = position;
                Quaternion = quaternion;
                Rotation = rotation;

                PositionScale = positionScale;
                RotationScale = rotationScale;

                PoseToBone = poseToBone;
                Alignment = alignment;
                Flags = flags;
                ProcType = procType;
                ProcIndex = procIndex;
                PhysicsBone = physicsBone;
                SurfacePropIdx = surfacePropIdx;
                Contents = contents;
            }

            public readonly string Name;
            public int Parent;
            public ImmutableArray<int> BoneControllers;

            public Vector3f Position;
            public Quaternionf Quaternion;
            public Vector3f Rotation;

            public Vector3f PositionScale;
            public Vector3f RotationScale;

            public Matrix4x4f PoseToBone;
            public Quaternionf Alignment;
            public int Flags;
            public int ProcType;
            public int ProcIndex;
            public int PhysicsBone;
            public int SurfacePropIdx;
            public int Contents;
        }

        public struct HitBox
        {
            internal HitBox(int bone, int group, Boxf boundingBox, string name)
            {
                Bone = bone;
                Group = group;
                BoundingBox = boundingBox;
                Name = name;
            }

            public readonly int Bone;
            public readonly int Group;
            public readonly Boxf BoundingBox;
            public readonly string Name;
        }

        public struct HitBoxSet
        {
            internal HitBoxSet(string name, ImmutableArray<HitBox> hitBoxes)
            {
                Name = name;
                HitBoxes = hitBoxes;
            }

            public readonly string Name;
            public readonly ImmutableArray<HitBox> HitBoxes;
        }

        public struct LocalAnimation
        {
        }

        public struct LocalSequence
        {
        }

        public struct Texture
        {
            internal Texture(string name, int flags)
            {
                Name = name;
                Flags = flags;
            }

            public readonly string Name;
            public readonly int Flags;
        }

        public struct BodyPart
        {
            internal BodyPart(string name, int @base, ImmutableArray<Model> models)
            {
                Name = name;
                Base = @base;
                Models = models;
            }

            public readonly string Name;
            public readonly int Base;
            public readonly ImmutableArray<Model> Models;
        }

        public struct Model
        {
            internal Model(string name, int type, float boundingRadius, 
                ImmutableArray<Mesh> meshes,
                int vertexCount, int vertexIndex, int tangentIndex,
                ImmutableArray<Eyeball> eyeballs)
            {
                Name = name;
                Type = type;
                BoundingRadius = boundingRadius;
                Meshes = meshes;
                VertexCount = vertexCount;
                VertexIndex = vertexIndex;
                TangentIndex = tangentIndex;
                Eyeballs = eyeballs;
            }

            public readonly string Name;
            public readonly int Type;
            public readonly float BoundingRadius;
            public readonly ImmutableArray<Mesh> Meshes;
            public readonly int VertexCount;
            public readonly int VertexIndex;
            public readonly int TangentIndex;
            public readonly ImmutableArray<Eyeball> Eyeballs;
        }

        public struct Mesh
        {
            internal Mesh(int material, int vertexCount, int vertexOffset, ImmutableArray<Flex> flexes,
                int materialType, int materialParam, int meshId, Vector3f center, ImmutableArray<int> lodVertices)
            {
                Material = material;
                VertexCount = vertexCount;
                VertexOffset = vertexOffset;
                Flexes = flexes;
                MaterialType = materialType;
                MaterialParam = materialParam;
                MeshId = meshId;
                Center = center;
                LodVertices = lodVertices;
            }

            public readonly int Material;
            public readonly int VertexCount;
            public readonly int VertexOffset;
            public readonly ImmutableArray<Flex> Flexes;
            public readonly int MaterialType;
            public readonly int MaterialParam;
            public readonly int MeshId;
            public readonly Vector3f Center;
            public readonly ImmutableArray<int> LodVertices;
        }

        public struct Flex
        {
        }

        public struct Eyeball
        {
        }

        public Mdl(string path)
        {
            using (var stream = File.OpenRead(path))
            {
                Load(stream);
            }
        }
        public Mdl(Stream stream)
        {
            Load(stream);
        }

        [Flags]
        public enum MdlFlags
        {
            /// <summary>
            /// This flag is set if no hitbox information was specified
            /// </summary>
            AutoGeneratedHitBox = (1 << 0),
            /// <summary>
            /// NOTE:  This flag is set at loadtime, not mdl build time so that we don't have to rebuild
            /// models when we change materials.
            /// </summary>
            UsesEnvCubemap = (1 << 1),
            /// <summary>
            /// Use this when there are translucent parts to the model but we're not going to sort it 
            /// </summary>
            ForceOpaque = (1 << 2),
            /// <summary>
            /// Use this when we want to render the opaque parts during the opaque pass
            /// and the translucent parts during the translucent pass
            /// </summary>
            TranslucentTwoPass = (1 << 3),
            /// <summary>
            /// This is set any time the .qc files has $staticprop in it
            /// Means there's no bones and no transforms
            /// </summary>
            StaticProp = (1 << 4),
            /// <summary>
            /// NOTE:  This flag is set at loadtime, not mdl build time so that we don't have to rebuild
            /// models when we change materials.
            /// </summary>
            UsesFBTexture = (1 << 5),
            /// <summary>
            /// This flag is set by studiomdl.exe if a separate "$shadowlod" entry was present
            /// for the .mdl (the shadow lod is the last entry in the lod list if present)
            /// </summary>
            HasShadowLod = (1 << 6),
            /// <summary>
            /// NOTE:  This flag is set at loadtime, not mdl build time so that we don't have to rebuild
            /// models when we change materials.
            /// </summary>
            UsesBumpMapping = (1 << 7),
            /// <summary>
            /// NOTE:  This flag is set when we should use the actual materials on the shadow LOD
            /// instead of overriding them with the default one (necessary for translucent shadows)
            /// </summary>
            UsesShadowLodMaterials = (1 << 8),
            /// <summary>
            /// NOTE:  This flag is set when we should use the actual materials on the shadow LOD
            /// instead of overriding them with the default one (necessary for translucent shadows)
            /// </summary>
            Obsolete = (1 << 9),
            Unused = (1 << 10),
            /// <summary>
            /// NOTE:  This flag is set at mdl build time
            /// </summary>
            NoForcedFade = (1 << 11),
            /// <summary>
            /// NOTE:  The npc will lengthen the viseme check to always include two phonemes
            /// </summary>
            ForcePhonemeCrossfade = (1 << 12),
            /// <summary>
            /// This flag is set when the .qc has $constantdirectionallight in it
            /// If set, we use constantdirectionallightdot to calculate light intensity
            /// rather than the normal directional dot product
            /// only valid if STUDIOHDR_FLAGS_STATIC_PROP is also set
            /// </summary>
            ConstantDirectionalLightDot = (1 << 13),
            /// <summary>
            /// Flag to mark delta flexes as already converted from disk format to memory format
            /// </summary>
            FlexesConverted = (1 << 14),
            /// <summary>
            /// Indicates the studiomdl was built in preview mode
            /// </summary>
            BuiltInPreviewMode = (1 << 15),
            /// <summary>
            /// Ambient boost (runtime flag)
            /// </summary>
            AmbiantBoost = (1 << 16),
            /// <summary>
            /// Don't cast shadows from this model (useful on first-person models)
            /// </summary>
            DoNotCastShadows = (1 << 17),
            /// <summary>
            /// alpha textures should cast shadows in vrad on this model (ONLY prop_static!)
            /// </summary>
            CastTextureShadows = (1 << 18),
        }

        public int Version { get; private set; }
        public int Checksum { get; private set; }
        public string Name { get; private set; }
        public Vector3f EyePosition { get; private set; }
        public Vector3f IlluminationPosition { get; private set; }
        public Boxf Hull { get; private set; }
        public Boxf ViewHull { get; private set; }
        public MdlFlags Flags { get; private set; }

        public ImmutableArray<Bone> Bones { get; private set; }
        public ImmutableArray<BoneController> BoneControllers { get; private set; }
        public ImmutableArray<HitBoxSet> HitBoxSets { get; private set; }
        public ImmutableArray<LocalAnimation> LocalAnimations { get; private set; }
        public ImmutableArray<LocalSequence> LocalSequences { get; private set; }
        public ImmutableArray<Texture> Textures { get; private set; }
        public ImmutableArray<string> CdTextures { get; private set; }
        public ImmutableArray<BodyPart> BodyParts { get; private set; }

        private void Load(Stream stream)
        {
            var reader = new Ibasa.IO.BinaryReader(stream, Encoding.ASCII);
            long baseOffset = reader.BaseStream.Position;

            string signature = Encoding.ASCII.GetString(reader.ReadBytes(4));

            if (signature != "IDST")
                throw new InvalidDataException("File signature does not match 'IDST'.");

            Version = reader.ReadInt32();

            if ((Version < 44 || Version > 49))
                throw new InvalidDataException(string.Format("File version {0} does not match 44 to 49.", Version));

            Checksum = reader.ReadInt32();
            Name = Encoding.ASCII.GetString(reader.ReadBytes(64)).Trim('\0');
            int length = reader.ReadInt32(); //file length

            EyePosition = new Vector3f(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
            IlluminationPosition = new Vector3f(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());

            Hull = new Boxf(
                new Point3f(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle()),
                new Size3f(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle()));

            ViewHull = new Boxf(
                new Point3f(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle()),
                new Size3f(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle()));

            Flags = (MdlFlags)reader.ReadInt32();

            int bonesCount = reader.ReadInt32();
            long bonesOffset = baseOffset + reader.ReadInt32();

            int boneControllersCount = reader.ReadInt32();
            long boneControllersOffset = baseOffset + reader.ReadInt32();

            int hitBoxSetsCount = reader.ReadInt32();
            long hitBoxSetsOffset = baseOffset + reader.ReadInt32();

            int localAnimCount = reader.ReadInt32();
            long localAnimOffset = baseOffset + reader.ReadInt32();

            int localSeqCount = reader.ReadInt32();
            long localSeqOffset = baseOffset + reader.ReadInt32();

            int activityListVersion = reader.ReadInt32(); 
            int eventsIndexed = reader.ReadInt32();

            int texturesCount = reader.ReadInt32();
            long texturesOffset = baseOffset + reader.ReadInt32();

            int cdTexturesCount = reader.ReadInt32();
            long cdTexturesOffset = baseOffset + reader.ReadInt32();

            int skinRefsCount = reader.ReadInt32();
            int skinFamiliesCount = reader.ReadInt32();
            long skinOffset = baseOffset + reader.ReadInt32();

            int bodyPartsCount = reader.ReadInt32();
            long bodyPartsOffset = baseOffset + reader.ReadInt32();

            int localAttachmentsCount = reader.ReadInt32();
            long localAttachmentsOffset = baseOffset + reader.ReadInt32();

            int localNodesCount = reader.ReadInt32();
            long localNodesOffset = baseOffset + reader.ReadInt32();
            long localNodesNameOffset = baseOffset + reader.ReadInt32();

            int flexDescsCount = reader.ReadInt32();
            long flexDescsOffset = baseOffset + reader.ReadInt32();

            int flexControllersCount = reader.ReadInt32();
            long flexControllersOffset = baseOffset + reader.ReadInt32();

            int flexRulesCount = reader.ReadInt32();
            long flexRulesOffset = baseOffset + reader.ReadInt32();

            int ikChainsCount = reader.ReadInt32();
            long ikChainsOffset = baseOffset + reader.ReadInt32();

            int mouthsCount = reader.ReadInt32();
            long mouthsOffset = baseOffset + reader.ReadInt32();

            //===
            //Start reading from offsets
            //===

            reader.BaseStream.Position = bonesOffset;
            Bones = LoadBones(reader, bonesCount);

            reader.BaseStream.Position = boneControllersOffset;
            BoneControllers = LoadBoneControllers(reader, boneControllersCount);

            //reader.BaseStream.Position = hitBoxSetsOffset;
            //HitBoxSets = LoadHitBoxSets(reader, hitBoxSetsCount);

            reader.BaseStream.Position = localAnimOffset;
            LocalAnimations = LoadLocalAnimations(reader, localAnimCount);

            reader.BaseStream.Position = localSeqOffset;
            LocalSequences = LoadLocalSequences(reader, localSeqCount);

            reader.BaseStream.Position = texturesOffset;
            Textures = LoadTextures(reader, texturesCount);

            reader.BaseStream.Position = cdTexturesOffset;
            CdTextures = LoadStringTable(reader, cdTexturesCount, baseOffset);

            //skin refs
            //TODO

            reader.BaseStream.Position = bodyPartsOffset;
            BodyParts = LoadBodyParts(reader, bodyPartsCount);
        }

        ImmutableArray<Bone> LoadBones(Ibasa.IO.BinaryReader reader, int count)
        {
            long offset = reader.BaseStream.Position;

            Bone[] bones = new Bone[count];
            for (int i = 0; i < count; ++i)
            {
                reader.BaseStream.Position = offset;

                long nameOffset = offset + reader.ReadInt32();
                int parent = reader.ReadInt32();
                ImmutableArray<int> boneControllers = new ImmutableArray<int>(new int[] {
                    reader.ReadInt32(),reader.ReadInt32(),reader.ReadInt32(),
                    reader.ReadInt32(),reader.ReadInt32(),reader.ReadInt32()});

                Vector3f position = new Vector3f(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
                Quaternionf quaternion = new Quaternionf(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
                Vector3f rotation = new Vector3f(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());

                Vector3f positionScale = new Vector3f(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
                Vector3f rotationScale = new Vector3f(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());

                Matrix4x4f poseToBone = new Matrix4x4f(
                    reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(),
                    reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(),
                    reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(),
                    0, 0, 0, 1);
                Quaternionf alignment = new Quaternionf(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
                int flags = reader.ReadInt32();
                int procType = reader.ReadInt32();
                int procIndex = reader.ReadInt32();
                int physicsBone = reader.ReadInt32();
                int surfacePropIdx = reader.ReadInt32();
                int contents = reader.ReadInt32();

                reader.Seek(8 * 4, SeekOrigin.Current); //skip 8 ints

                offset = reader.BaseStream.Position;

                reader.BaseStream.Position = nameOffset;
                string name = LoadString(reader);

                bones[i] = new Bone(name, parent, boneControllers, position, quaternion, rotation,
                    positionScale, rotationScale, poseToBone, alignment, flags,
                    procType, procIndex, physicsBone, surfacePropIdx, contents);
            }
            return new ImmutableArray<Bone>(bones);
        }

        ImmutableArray<BoneController> LoadBoneControllers(Ibasa.IO.BinaryReader reader, int count)
        {
            BoneController[] boneControllers = new BoneController[count];
            for (int i = 0; i < count; ++i)
            {
                boneControllers[i] = new BoneController(
                    reader.ReadInt32(),
                    (BoneControllerType)reader.ReadInt32(),
                    reader.ReadSingle(),
                    reader.ReadSingle(),
                    reader.ReadInt32(),
                    reader.ReadInt32());
                reader.Seek(8 * 4, SeekOrigin.Current); //skip 8 ints
            }
            return new ImmutableArray<BoneController>(boneControllers);
        }

        ImmutableArray<HitBoxSet> LoadHitBoxSets(Ibasa.IO.BinaryReader reader, int count)
        {
            long offset = reader.BaseStream.Position;

            HitBoxSet[] hitBoxSets = new HitBoxSet[count];
            for (int i = 0; i < count; ++i)
            {
                reader.BaseStream.Position = offset;

                long nameOffset = offset + reader.ReadInt32();
                int hitBoxCount = reader.ReadInt32();
                long hitBoxOffset = offset + reader.ReadInt32();

                offset = reader.BaseStream.Position;

                reader.BaseStream.Position = nameOffset;
                string name = LoadString(reader);
                reader.BaseStream.Position = hitBoxOffset;
                ImmutableArray<HitBox> hitBoxes = LoadHitBoxes(reader, hitBoxCount);
                hitBoxSets[i] = new HitBoxSet(name, hitBoxes);
            }
            return new ImmutableArray<HitBoxSet>(hitBoxSets);
        }

        ImmutableArray<HitBox> LoadHitBoxes(Ibasa.IO.BinaryReader reader, int count)
        {
            long offset = reader.BaseStream.Position;

            HitBox[] hitBoxes = new HitBox[count];
            for (int i = 0; i < count; ++i)
            {
                reader.BaseStream.Position = offset;

                int bone = reader.ReadInt32();
                int group = reader.ReadInt32();
                Boxf box = new Boxf(
                    new Point3f(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle()),
                    new Size3f(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle()));
                long nameOffset = offset + reader.ReadInt32();

                reader.Seek(8*4, SeekOrigin.Current); //skip 8 ints

                offset = reader.BaseStream.Position;

                reader.BaseStream.Position = nameOffset;
                string name = LoadString(reader);

                hitBoxes[i] = new HitBox(bone, group, box, name);
            }
            return new ImmutableArray<HitBox>(hitBoxes);
        }

        ImmutableArray<LocalAnimation> LoadLocalAnimations(Ibasa.IO.BinaryReader reader, int count)
        {
            long offset = reader.BaseStream.Position;

            LocalAnimation[] localAnimations = new LocalAnimation[count];
            for (int i = 0; i < count; ++i)
            {
                reader.BaseStream.Position = offset;


                offset = reader.BaseStream.Position;
            }
            return new ImmutableArray<LocalAnimation>(localAnimations);
        }

        ImmutableArray<LocalSequence> LoadLocalSequences(Ibasa.IO.BinaryReader reader, int count)
        {
            long offset = reader.BaseStream.Position;

            LocalSequence[] localSequences = new LocalSequence[count];
            for (int i = 0; i < count; ++i)
            {
                reader.BaseStream.Position = offset;


                offset = reader.BaseStream.Position;
            }
            return new ImmutableArray<LocalSequence>(localSequences);
        }

        ImmutableArray<Texture> LoadTextures(Ibasa.IO.BinaryReader reader, int count)
        {
            long offset = reader.BaseStream.Position;

            Texture[] textures = new Texture[count];
            for (int i = 0; i < count; ++i)
            {
                reader.BaseStream.Position = offset;

                long nameOffset = offset + reader.ReadInt32();
                int flags = reader.ReadInt32();
                int used = reader.ReadInt32();
                int unused = reader.ReadInt32();

                reader.Seek(12 * 4, SeekOrigin.Current); //skip 12 ints

                offset = reader.BaseStream.Position;

                reader.BaseStream.Position = nameOffset;
                string name = LoadString(reader);

                textures[i] = new Texture(name, flags);
            }
            return new ImmutableArray<Texture>(textures);
        }

        ImmutableArray<BodyPart> LoadBodyParts(Ibasa.IO.BinaryReader reader, int count)
        {
            long offset = reader.BaseStream.Position;

            BodyPart[] bodyParts = new BodyPart[count];
            for (int i = 0; i < count; ++i)
            {
                reader.BaseStream.Position = offset;

                long nameOffset = offset + reader.ReadInt32();
                int modelCount = reader.ReadInt32();
                int @base = reader.ReadInt32();
                long modelOffset = offset + reader.ReadInt32();

                offset = reader.BaseStream.Position;

                reader.BaseStream.Position = nameOffset;
                string name = LoadString(reader);

                reader.BaseStream.Position = modelOffset;
                ImmutableArray<Model> models = LoadModels(reader, modelCount);

                bodyParts[i] = new BodyPart(name, @base, models);
            }
            return new ImmutableArray<BodyPart>(bodyParts);
        }

        ImmutableArray<Model> LoadModels(Ibasa.IO.BinaryReader reader, int count)
        {
            long offset = reader.BaseStream.Position;

            Model[] models = new Model[count];
            for (int i = 0; i < count; ++i)
            {
                reader.BaseStream.Position = offset;

                string name = Encoding.ASCII.GetString(reader.ReadBytes(64)).Trim('\0');
                int type = reader.ReadInt32();
                float boundingRadius = reader.ReadSingle();

                int meshCount = reader.ReadInt32();
                long meshOffset = offset + reader.ReadInt32();

                int vertexCount = reader.ReadInt32();
                int vertexIndex = reader.ReadInt32();
                int tangentIndex = reader.ReadInt32();

                int numattachments = reader.ReadInt32();
                int attachmentindex = reader.ReadInt32();

                int eyeballCount = reader.ReadInt32();
                long eyeballOffset = offset + reader.ReadInt32();

                int vertexData = reader.ReadInt32();
                int indexData = reader.ReadInt32();

                reader.Seek(8 * 4, SeekOrigin.Current); //skip 8 ints

                offset = reader.BaseStream.Position;

                reader.BaseStream.Position = meshOffset;
                ImmutableArray<Mesh> meshes = LoadMeshes(reader, meshCount);

                reader.BaseStream.Position = meshOffset;
                ImmutableArray<Eyeball> eyeballs = LoadEyeballs(reader, meshCount);

                models[i] = new Model(name, type, boundingRadius, meshes, vertexCount, vertexIndex, tangentIndex, eyeballs);
            }
            return new ImmutableArray<Model>(models);
        }

        ImmutableArray<Eyeball> LoadEyeballs(Ibasa.IO.BinaryReader reader, int count)
        {
            long offset = reader.BaseStream.Position;

            Eyeball[] eyeballs = new Eyeball[count];
            for (int i = 0; i < count; ++i)
            {
                offset = reader.BaseStream.Position;

                reader.BaseStream.Position = offset;
            }
            return new ImmutableArray<Eyeball>(eyeballs);
        }

        ImmutableArray<Mesh> LoadMeshes(Ibasa.IO.BinaryReader reader, int count)
        {
            long offset = reader.BaseStream.Position;

            Mesh[] meshes = new Mesh[count];
            for (int i = 0; i < count; ++i)
            {
                reader.BaseStream.Position = offset;

                int material = reader.ReadInt32();
                int modelIndex = reader.ReadInt32();

                int vertexCount = reader.ReadInt32();
                int vertexOffset = reader.ReadInt32();

                int flexCount = reader.ReadInt32();
                long flexOffset = offset + reader.ReadInt32();

                int materialType = reader.ReadInt32();
                int materialParam = reader.ReadInt32();

                int meshId = reader.ReadInt32();

                Vector3f center = new Vector3f(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());

                int modelvertexdata = reader.ReadInt32();

                int[] numLODVertexes = new int[8];
                for (int l = 0; l < 8; ++l)
                {
                    numLODVertexes[l] = reader.ReadInt32();
                }
                ImmutableArray<int> lods = new ImmutableArray<int>(numLODVertexes);

                reader.Seek(8 * 4, SeekOrigin.Current); //skip 8 ints

                offset = reader.BaseStream.Position;

                reader.BaseStream.Position = flexOffset;
                ImmutableArray<Flex> flexes = LoadFlexes(reader, flexCount);

                meshes[i] = new Mesh(material, vertexCount, vertexOffset, flexes, materialType, materialParam, meshId, center, lods);
            }
            return new ImmutableArray<Mesh>(meshes);
        }

        ImmutableArray<Flex> LoadFlexes(Ibasa.IO.BinaryReader reader, int count)
        {
            return null;
        }

        ImmutableArray<string> LoadStringTable(Ibasa.IO.BinaryReader reader, int count, long baseOffset)
        {
            long[] offsets = new long[count];
            for (int i = 0; i < count; ++i)
            {
                offsets[i] = baseOffset + reader.ReadInt32();
            }
            string[] strings = new string[count];
            for (int i = 0; i < count; ++i)
            {
                reader.BaseStream.Position = offsets[i];
                strings[i] = LoadString(reader);
            }
            return new ImmutableArray<string>(strings);
        }

        string LoadString(Ibasa.IO.BinaryReader reader)
        {
            StringBuilder builder = new StringBuilder();
            char c;
            while ((c = reader.ReadChar()) != '\0')
            {
                builder.Append(c);
            }
            return builder.ToString();
        }
    }
}
