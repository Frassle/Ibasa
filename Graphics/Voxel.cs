using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ibasa.Numerics.Geometry;

namespace Ibasa.Graphics
{

    public class Voxel
    {
        public struct Vertex
        {
            public Vertex(Vector4f position, Vector4f normal, Vector4b color)
            {
                Position = position;
                Normal = normal;
                Color = color;
            }

            public Vector4f Position;
            public Vector4f Normal;
            public Vector4b Color;
        }

        int VoxelIndex(int x, int y, int z)
        {
            return x + y * width + z * width * height;
        }

        int Surround(int x, int y, int z)
        {
            int index = 0;

            if (x != 0)
                index |= voxels[VoxelIndex(x - 1, y, z)].XYZ == Vector3b.Zero ? 0 : 1;
            if (x != width - 1)
                index |= voxels[VoxelIndex(x + 1, y, z)].XYZ == Vector3b.Zero ? 0 : 2;
            if (y != 0)
                index |= voxels[VoxelIndex(x, y - 1, z)].XYZ == Vector3b.Zero ? 0 : 4;
            if (y != height - 1)
                index |= voxels[VoxelIndex(x, y + 1, z)].XYZ == Vector3b.Zero ? 0 : 8;
            if (z != 0)
                index |= voxels[VoxelIndex(x, y, z - 1)].XYZ == Vector3b.Zero ? 0 : 16;
            if (z != depth - 1)
                index |= voxels[VoxelIndex(x, y, z + 1)].XYZ == Vector3b.Zero ? 0 : 32;

            return index;
        }

        int width, height, depth;
        Vector4b[] voxels;

        public Voxel(Vector4b[][] colors, int width, int height)
        {
            this.width = width;
            this.height = colors.Length;
            this.depth = height;

            List<Vertex> vertices = new List<Vertex>();

            voxels = new Vector4b[this.depth * this.width * this.height];
            for (int z = 0; z < this.depth; ++z)
            {
                for (int y = 0; y < this.height; ++y)
                {
                    for (int x = 0; x < this.width; ++x)
                    {
                        voxels[x + y * this.width + z * this.width * this.height] = colors[y][x + z * this.width];
                    }
                }
            }
        }

        public static Vertex[] Mesh(Vector4b[][] colors, int width, int height)
        {
            var voxel = new Voxel(colors, width, height);

            return voxel.Mesh();
        }

        public Vertex[] Mesh()
        {
            var vertices = new List<Vertex>();

            for (int z = 0; z < depth; ++z)
            {
                for (int y = 0; y < height; ++y)
                {
                    for (int x = 0; x < width; ++x)
                    {
                        if (voxels[VoxelIndex(x, y, z)].XYZ != Vector3b.Zero)
                        {
                            List<Vertex> cube = GetCube(Surround(x, y, z));

                            foreach (var vertex in cube)
                            {
                                vertices.Add(
                                    new Vertex(vertex.Position + new Vector4f(x, y, z, 0), vertex.Normal, voxels[VoxelIndex(x, y, z)]));
                            }
                        }
                    }
                }
            }

            return vertices.ToArray();
        }

        private static List<Vertex>[] Cubes;

        public static int MaxVertices;
        public static List<Vertex> GetCube(int index)
        {
            return Cubes[index];
        }

        static Voxel()
        {
            UpdateCubes();
        }

        private static void AddTriangle(List<Vertex> vertices, Vector3f a, Vector3f b, Vector3f c)
        {
            var normal = Vector.Cross(b - a, c - a);

            vertices.Add(new Vertex(new Vector4f(a, 1), new Vector4f(normal, 0), Vector4b.Zero));
            vertices.Add(new Vertex(new Vector4f(b, 1), new Vector4f(normal, 0), Vector4b.Zero));
            vertices.Add(new Vertex(new Vector4f(c, 1), new Vector4f(normal, 0), Vector4b.Zero));
        }

        private static void UpdateCubes()
        {
            Cubes = new List<Vertex>[64];
            MaxVertices = 0;
            for (int i = 0; i < Cubes.Length; ++i)
            {
                Cubes[i] = new List<Vertex>();

                bool lx = (i & 1) == 0;
                bool hx = (i & 2) == 0;
                bool ly = (i & 4) == 0;
                bool hy = (i & 8) == 0;
                bool lz = (i & 16) == 0;
                bool hz = (i & 32) == 0;

                if (lx)
                {
                    AddTriangle(Cubes[i],
                        new Vector3f(0, 0, 0), new Vector3f(0, 1, 1), new Vector3f(0, 0, 1));

                    AddTriangle(Cubes[i],
                        new Vector3f(0, 0, 0), new Vector3f(0, 1, 0), new Vector3f(0, 1, 1));
                }

                if (hx)
                {
                    AddTriangle(Cubes[i],
                        new Vector3f(1, 0, 0), new Vector3f(1, 0, 1), new Vector3f(1, 1, 1));

                    AddTriangle(Cubes[i],
                        new Vector3f(1, 0, 0), new Vector3f(1, 1, 1), new Vector3f(1, 1, 0));
                }

                if (ly)
                {
                    AddTriangle(Cubes[i],
                        new Vector3f(0, 0, 0), new Vector3f(1, 0, 1), new Vector3f(1, 0, 0));

                    AddTriangle(Cubes[i],
                        new Vector3f(0, 0, 0), new Vector3f(0, 0, 1), new Vector3f(1, 0, 1));
                }

                if (hy)
                {
                    AddTriangle(Cubes[i],
                        new Vector3f(0, 1, 0), new Vector3f(1, 1, 0), new Vector3f(1, 1, 1));

                    AddTriangle(Cubes[i],
                        new Vector3f(0, 1, 0), new Vector3f(1, 1, 1), new Vector3f(0, 1, 1));
                }

                if (lz)
                {
                    AddTriangle(Cubes[i],
                        new Vector3f(0, 0, 0), new Vector3f(1, 0, 0), new Vector3f(1, 1, 0));

                    AddTriangle(Cubes[i],
                        new Vector3f(0, 0, 0), new Vector3f(1, 1, 0), new Vector3f(0, 1, 0));
                }

                if (hz)
                {
                    AddTriangle(Cubes[i],
                        new Vector3f(0, 0, 1), new Vector3f(1, 1, 1), new Vector3f(1, 0, 1));

                    AddTriangle(Cubes[i],
                        new Vector3f(0, 0, 1), new Vector3f(0, 1, 1), new Vector3f(1, 1, 1));
                }

                MaxVertices = Math.Max(Cubes[i].Count, MaxVertices);
            }
        }
    }
}
