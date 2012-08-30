using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ibasa.Numerics.Geometry;
using System.Diagnostics.Contracts;
using Ibasa.Numerics;

namespace Ibasa.Spatial
{
    public class VoxelOctree
    {
        ushort[] Nodes;
        public Boxl Bounds { get; private set; }

        public VoxelOctree(Point3l point, long size)
        {
            Contract.Requires(Functions.IsPowerOf2(size));

            Bounds = new Boxl(point, size, size, size);

            int levels = (int)Functions.Log(size, 2) + 1;
            int nodes = (int)(Functions.Pow(8, levels) - 1) / 7;

            Nodes = new ushort[nodes];
            Nodes[0] = 0x8000;
        }

        bool Leaf(int node)
        {
            return (Nodes[node] & 0x8000) != 0;
        }
        int Value(int node)
        {
            return (Nodes[node] & 0x7FFF);
        }

        Point3l Subposition(int index, Point3l bounds, long size)
        {
            long x = (index & 1) == 0 ? 0 : size;
            long y = (index & 2) == 0 ? 0 : size;
            long z = (index & 4) == 0 ? 0 : size;

            return new Point3l(bounds.X + x, bounds.Y + y, bounds.Z + z);
        }

        int Get(Point3l point, Point3l position, long size, int node)
        {
            if (Leaf(node))
            {
                return Value(node);
            }
            else
            {
                size /= 2;

                var center = new Point3l(position.X + size, position.Y + size, position.Z + size);
                int index = 0;
                index |= point.X < center.X ? 0 : 1;
                index |= point.Y < center.Y ? 0 : 2;
                index |= point.Z < center.Z ? 0 : 4;

                node = 8 * node + index + 1;

                position = Subposition(index, position, size);

                return Get(point, position, size, node);
            }
        }

        public bool Set(Point3l point, int value, Point3l position, long size, int node)
        {
            if ((Value(node) == value) && (Leaf(node) || size == 1))
            {
                return false;
            }
            else if (size == 1)
            {
                Nodes[node] = (ushort)(value | 0x8000);
                return true;
            }
            else
            {
                size /= 2;

                Split(node, size);

                var center = new Point3l(position.X + size, position.Y + size, position.Z + size);
                int index = 0;
                index |= point.X < center.X ? 0 : 1;
                index |= point.Y < center.Y ? 0 : 2;
                index |= point.Z < center.Z ? 0 : 4;

                var child = 8 * node + index + 1;

                position = Subposition(index, position, size);

                bool sparse = Set(point, value, position, size, child);

                if (sparse)
                {
                    return Sparsen(node);
                }
                return false;
            }
        }

        private bool Sparsen(int node)
        {
            int index = node * 8;
            var value = Value(++index);
            bool all = true;
            all &= Value(++index) == value;
            all &= Value(++index) == value;
            all &= Value(++index) == value;
            all &= Value(++index) == value;
            all &= Value(++index) == value;
            all &= Value(++index) == value;
            all &= Value(++index) == value;

            if (all)
            {
                Nodes[node] = (ushort)(value | 0x8000);
            }
            return all;
        }
        
        private void Split(int node, long size)
        {
            if (Leaf(node))
            {
                int index = 8 * node;
                ushort value = (ushort)(Value(node) | (size == 1 ? 0x8000 : 0x0));
                Nodes[++index] = value;
                Nodes[++index] = value;
                Nodes[++index] = value;
                Nodes[++index] = value;
                Nodes[++index] = value;
                Nodes[++index] = value;
                Nodes[++index] = value;
                Nodes[++index] = value;
                Nodes[node] &= 0x7FFF;
            }
        }

        public int this[Point3l point]
        {
            get
            {
                Contract.Requires(Box.Contains(Bounds, point));

                return Get(point, Bounds.Location, Bounds.Width, 0);
            }
            set
            {
                Contract.Requires(Box.Contains(Bounds, point));

                Set(point, value, Bounds.Location, Bounds.Width, 0);
            }
        }

        void Write(Ibasa.IO.BinaryWriter writer, int index)
        {
            writer.WriteVariable((ushort)Nodes[index]);
            if (!Leaf(index))
            {
                index = 8 * index;
                Write(writer, ++index);
                Write(writer, ++index);
                Write(writer, ++index);
                Write(writer, ++index);
                Write(writer, ++index);
                Write(writer, ++index);
                Write(writer, ++index);
                Write(writer, ++index);
            }
        }

        public void Write(Ibasa.IO.BinaryWriter writer)
        {
            Write(writer, 0);
        }

        void Read(Ibasa.IO.BinaryReader reader, int index)
        {
            Nodes[index] = reader.ReadVariableUInt16();
            if (!Leaf(index))
            {
                index = 8 * index;
                Read(reader, ++index);
                Read(reader, ++index);
                Read(reader, ++index);
                Read(reader, ++index);
                Read(reader, ++index);
                Read(reader, ++index);
                Read(reader, ++index);
                Read(reader, ++index);
            }
        }

        public void Read(Ibasa.IO.BinaryReader reader)
        {
            Array.Clear(Nodes, 0, Nodes.Length);
            Read(reader, 0);
        }
    }
}
