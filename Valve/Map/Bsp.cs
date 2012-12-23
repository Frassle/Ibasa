using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Ibasa.Numerics;
using Ibasa.SharpIL;
using Ibasa.Valve.Material;
using Ibasa.Numerics.Geometry;

namespace Ibasa.Valve.Map
{
    public sealed class Bsp
    {
        struct Model
        {
            public Boxf bound;      // The bounding box of the Model
            public Vector3f origin; // origin of model, usually (0,0,0)
            public long node_id0;   // index of first BSP node
            public long node_id1;   // index of the first Clip node
            public long node_id2;   // index of the second Clip node
            public long node_id3;   // usually zero
            public long numleafs;   // number of BSP leaves
            public long face_id;    // index of Faces
            public long face_num;   // number of Faces
        }

        public struct Edge
        {
            internal Edge(int vertex0, int vertex1)
            {
                Vertex0 = vertex0;
                Vertex1 = vertex1;
            }

            public readonly int Vertex0;
            public readonly int Vertex1;
        }

        public struct Surface
        {
            internal Surface(Vector4f s, Vector4f t, int textureIndex, int flags)
            {
                S = s;
                T = t;
                TextureIndex = textureIndex;
                Flags = flags;
            }

            public readonly Vector4f S; // S vector, horizontal in texture space
            public readonly Vector4f T; // T vector, vertical in texture space
            public readonly int TextureIndex; // Index of Mip Texture
            public readonly int Flags;
        }

        public struct Face
        {
            public Face(int planeIndex, int side, int surfaceEdgeIndex, int surfaceEdgeCount,
                int surfaceIndex, int l1, int l2, int l3, int l4, long lightmap)
            {
                PlaneIndex = planeIndex;
                Side = side;
                SurfaceEdgeIndex = surfaceEdgeIndex;
                SurfaceEdgeCount = surfaceEdgeCount;
                SurfaceIndex = surfaceIndex;
                typelight = l1;
                baselight = l2;
                light0 = l3;
                light1 = l4;
                Lightmap = lightmap;
            }

            public readonly int PlaneIndex;       // The plane in which the face lies
                                                  // must be in [0,numplanes[ 
            public readonly int Side;             // 0 if in front of the plane, 1 if behind the plane
            public readonly int SurfaceEdgeIndex; // first edge in the List of edges
                                                  // must be in [0,numledges[
            public readonly int SurfaceEdgeCount; // number of edges in the List of edges
            public readonly int SurfaceIndex;     // index of the Texture info the face is part of
                                                  // must be in [0,numtexinfos[ 
            public readonly int typelight;        // type of lighting, for the face
            public readonly int baselight;        // from 0xFF (dark) to 0 (bright)
            public readonly int light0;           // two additional light models  
            public readonly int light1;           // two additional light models  
            public readonly long Lightmap;        // Pointer inside the general light map, or -1
                                                  // this define the start of the face light map
        }

        public string Entities;
        public Planef[] Planes;
        public Resource[] Textures;
        public Vector3f[] Vertices;
        public byte[] Visibility;
        public Surface[] Surfaces;
        public Face[] Faces;
        public byte[] Lighting;
        public Edge[] Edges;
        public int[] SurfaceEdges;

        //#define entities       0
        //#define planes        1
        //#define textures      2
        //#define vertexes      3
        //#define visibility    4
        //#define nodes         5
        //#define texinfo       6
        //#define faces         7
        //#define lighting      8
        //#define clipnodes     9
        //#define leafs        10
        //#define marksurfaces 11
        //#define edges        12
        //#define surfedges    13
        //#define models       14

        Ibasa.IO.BinaryReader Reader;

        public Bsp(string path, IEnumerable<Package.Wad> wads) :
            this(File.OpenRead(path), wads)
        { }
        public Bsp(Stream stream, IEnumerable<Package.Wad> wads)
        {
            if (!stream.CanSeek || !stream.CanRead)
                throw new ArgumentException("stream must be seekable and readable.", "stream");
            if (wads == null)
                wads = System.Linq.Enumerable.Empty<Package.Wad>();

            Reader = new Ibasa.IO.BinaryReader(stream, Encoding.ASCII);

            if (Reader.ReadUInt32() != 30)
                throw new InvalidDataException("Version is not 30.");

            long entitiesOffset = Reader.ReadUInt32();
            long entitiesCount = Reader.ReadUInt32();
            long planesOffset = Reader.ReadUInt32();
            long planesCount = Reader.ReadUInt32();
            long texturesOffset = Reader.ReadUInt32();
            long texturesCount = Reader.ReadUInt32();
            long vertexesOffset = Reader.ReadUInt32();
            long vertexesCount = Reader.ReadUInt32();
            long visibilityOffset = Reader.ReadUInt32();
            long visibilityCount = Reader.ReadUInt32();
            long nodesOffset = Reader.ReadUInt32();
            long nodesCount = Reader.ReadUInt32();
            long texinfoOffset = Reader.ReadUInt32();
            long texinfoCount = Reader.ReadUInt32();
            long facesOffset = Reader.ReadUInt32();
            long facesCount = Reader.ReadUInt32();
            long lightingOffset = Reader.ReadUInt32();
            long lightingCount = Reader.ReadUInt32();
            long clipnodesOffset = Reader.ReadUInt32();
            long clipnodesCount = Reader.ReadUInt32();
            long leafsOffset = Reader.ReadUInt32();
            long leafsCount = Reader.ReadUInt32();
            long marksurfacesOffset = Reader.ReadUInt32();
            long marksurfacesCount = Reader.ReadUInt32();
            long edgesOffset = Reader.ReadUInt32();
            long edgesCount = Reader.ReadUInt32();
            long surfedgesOffset = Reader.ReadUInt32();
            long surfedgesCount = Reader.ReadUInt32();
            long modelsOffset = Reader.ReadUInt32();
            long modelsCount = Reader.ReadUInt32();

            //Entities
            Reader.Seek(entitiesOffset, SeekOrigin.Begin);
            Entities = Encoding.ASCII.GetString(Reader.ReadBytes((int)entitiesCount));

            //Planes
            Reader.Seek(planesOffset, SeekOrigin.Begin);
            Planes = new Planef[planesCount / 20];
            for (int i = 0; i < Planes.Length; ++i)
            {
                Planes[i] = new Planef(
                    Reader.ReadSingle(), Reader.ReadSingle(), Reader.ReadSingle(), Reader.ReadSingle());

                uint type = Reader.ReadUInt32();
            }

            //Textures
            {
                Reader.Seek(texturesOffset, SeekOrigin.Begin);
                long[] offsets = new long[Reader.ReadUInt32()];
                for (int i = 0; i < offsets.Length; ++i)
                {
                    offsets[i] = Reader.ReadUInt32();
                }

                Textures = new Resource[offsets.Length];
                for (int i = 0; i < offsets.Length; ++i)
                {
                    Reader.Seek(texturesOffset + offsets[i], SeekOrigin.Begin);

                    string name = Encoding.ASCII.GetString(Reader.ReadBytes(16));
                    int nullbyte = name.IndexOf('\0');
                    name = nullbyte == -1 ? name : name.Substring(0, nullbyte);

                    int width = Reader.ReadInt32();
                    int height = Reader.ReadInt32();

                    long[] dataoffsets = new long[4];
                    dataoffsets[0] = Reader.ReadUInt32();
                    dataoffsets[1] = Reader.ReadUInt32();
                    dataoffsets[2] = Reader.ReadUInt32();
                    dataoffsets[3] = Reader.ReadUInt32();

                    Resource resource = null;

                    if (dataoffsets.All(o => o == 0))
                    {
                        foreach (var wad in wads)
                        {
                            resource = wad[name];
                            if (resource != null)
                            {
                                break;
                            }
                        }
                    }
                    else
                    {
                        resource = new Resource(new Size3i(width, height, 1), 4, 1, Format.R8G8B8A8UNorm);

                        byte[][] images = new byte[4][];

                        for (int mipSlice = 0; mipSlice < 4; ++mipSlice)
                        {
                            Reader.Seek(texturesOffset + offsets[i] + dataoffsets[mipSlice], SeekOrigin.Begin);

                            images[mipSlice] = Reader.ReadBytes(width * height);
                            width >>= 1; height >>= 1;
                        }

                        Reader.Seek(2, SeekOrigin.Current);
                        byte[] pallet = Reader.ReadBytes(256 * 3);

                        for (int mipSlice = 0; mipSlice < 4; ++mipSlice)
                        {
                            byte[] data = resource[mipSlice, 0];
                            byte[] image = images[mipSlice];

                            for (int j = 0; j < image.Length; ++j)
                            {
                                int palletIndex = image[j] * 3;
                                int dataIndex = j * 3;

                                data[dataIndex + 0] = pallet[palletIndex + 0];
                                data[dataIndex + 1] = pallet[palletIndex + 1];
                                data[dataIndex + 2] = pallet[palletIndex + 2];
                            }
                        }
                    }

                    Textures[i] = resource;
                }
            }

            //Vertices
            Reader.Seek(vertexesOffset, SeekOrigin.Begin);
            Vertices = new Vector3f[vertexesCount / 12];
            for (int i = 0; i < Vertices.Length; ++i)
            {
                Vertices[i] = new Vector3f(Reader.ReadSingle(), Reader.ReadSingle(), Reader.ReadSingle());
            }

            //Visibility
            Reader.Seek(visibilityOffset, SeekOrigin.Begin);
            Visibility = Reader.ReadBytes((int)visibilityCount);

            //Nodes

            //Surfaces
            Reader.Seek(texinfoOffset, SeekOrigin.Begin);
            Surfaces = new Surface[texinfoCount / 24];
            for (int i = 0; i < Surfaces.Length; ++i)
            {
                Surfaces[i] = new Surface(
                    new Vector4f(Reader.ReadSingle(), Reader.ReadSingle(), Reader.ReadSingle(), Reader.ReadSingle()),
                    new Vector4f(Reader.ReadSingle(), Reader.ReadSingle(), Reader.ReadSingle(), Reader.ReadSingle()),
                    Reader.ReadInt32(), Reader.ReadInt32());
            }

            //Faces
            Reader.Seek(facesOffset, SeekOrigin.Begin);
            Faces = new Face[facesCount / 20];
            for (int i = 0; i < Faces.Length; ++i)
            {
                Faces[i] = new Face(
                    Reader.ReadUInt16(), Reader.ReadInt16(),
                    Reader.ReadInt32(), Reader.ReadInt16(), Reader.ReadInt16(),
                    Reader.ReadByte(), Reader.ReadByte(), Reader.ReadByte(), Reader.ReadByte(),
                    Reader.ReadInt32());
            }

            //Lighting
            Reader.Seek(facesOffset, SeekOrigin.Begin);
            Lighting = Reader.ReadBytes((int)lightingCount);

            //#define clipnodes     9
            //#define leafs        10
            //#define marksurfaces 11
            //#define edges        12
            //#define surfedges    13
            //#define models       14
            
            //Edges
            Reader.Seek(edgesOffset, SeekOrigin.Begin);
            Edges = new Edge[edgesCount / 4];
            for (int i = 0; i < Edges.Length; ++i)
            {
                Edges[i] = new Edge(Reader.ReadUInt16(), Reader.ReadUInt16());
            }

            //SurfaceEdges
            Reader.Seek(surfedgesOffset, SeekOrigin.Begin);
            SurfaceEdges = new int[surfedgesCount / 4];
            for (int i = 0; i < Planes.Length; ++i)
            {
                SurfaceEdges[i] = Reader.ReadInt32();
            }
        }
    }
}
