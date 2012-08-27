using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using Ibasa.Numerics;
using Ibasa.Numerics.Geometry;

namespace Ibasa.Spatial
{
    public class SparseVoxelOctree<T> where T : IEquatable<T>
    {
        public struct Node
        {
            Node[] Children;
            T Value;

            public Node(T value)
            {
                Children = null;
                Value = value;
            }

            private void Split()
            {
                if (Children == null)
                {
                    Children = new Node[8];
                    Children[0] = new Node(Value);
                    Children[1] = new Node(Value);
                    Children[2] = new Node(Value);
                    Children[3] = new Node(Value);
                    Children[4] = new Node(Value);
                    Children[5] = new Node(Value);
                    Children[6] = new Node(Value);
                    Children[7] = new Node(Value);
                }
            }

            private Boxl Subbounds(int index, Boxl bounds, Size3l size)
            {
                long x = (index & 1) == 0 ? 0 : size.Width;
                long y = (index & 2) == 0 ? 0 : size.Height;
                long z = (index & 4) == 0 ? 0 : size.Depth;

                return new Boxl(bounds.X + x, bounds.Y + y, bounds.Z + z, size);
            }

            public T Get(Boxl bounds, Point3l point)
            {
                if (Children == null)
                {
                    return Value;
                }
                else
                {
                    var center = bounds.Center;
                    int index = 0;
                    index |= point.X < center.X ? 0 : 1;
                    index |= point.Y < center.Y ? 0 : 2;
                    index |= point.Z < center.Z ? 0 : 4;

                    return Children[index].Get(Subbounds(index, bounds, bounds.Size / 2), point);
                }
            }

            public bool Set(Boxl bounds, Point3l point, T value)
            {
                if (value.Equals(Value) && (Children == null || bounds.Size == Size3i.Unit))
                {
                    return false;
                }
                else if (bounds.Size == Size3i.Unit)
                {
                    Value = value;
                    return true;
                }
                else
                {
                    Split();

                    var center = bounds.Center;
                    int index = 0;
                    index |= point.X < center.X ? 0 : 1;
                    index |= point.Y < center.Y ? 0 : 2;
                    index |= point.Z < center.Z ? 0 : 4;

                    bool sparse = Children[index].Set(Subbounds(index, bounds, bounds.Size / 2), point, value);

                    if (sparse)
                    {
                        return Sparsen();
                    }
                    return false;
                }
            }

            private bool Sparsen()
            {
                var value = Children[0].Value;
                bool all = true;
                all &= Children[1].Value.Equals(value);
                all &= Children[2].Value.Equals(value);
                all &= Children[3].Value.Equals(value);
                all &= Children[4].Value.Equals(value);
                all &= Children[5].Value.Equals(value);
                all &= Children[6].Value.Equals(value);
                all &= Children[7].Value.Equals(value);

                if (all)
                {
                    Value = value;
                    Children = null;
                }
                return all;
            }

            public bool Sparsen(Boxl bounds)
            {
                if (Children == null || bounds.Size == Size3i.Unit)
                    return true;

                Size3l size = bounds.Size / 2;

                bool sparsen = true;
                sparsen &= Children[0].Sparsen(Subbounds(0, bounds, size));
                sparsen &= Children[1].Sparsen(Subbounds(1, bounds, size));
                sparsen &= Children[2].Sparsen(Subbounds(2, bounds, size));
                sparsen &= Children[3].Sparsen(Subbounds(3, bounds, size));
                sparsen &= Children[4].Sparsen(Subbounds(4, bounds, size));
                sparsen &= Children[5].Sparsen(Subbounds(5, bounds, size));
                sparsen &= Children[6].Sparsen(Subbounds(6, bounds, size));
                sparsen &= Children[7].Sparsen(Subbounds(7, bounds, size));

                if (sparsen)
                {
                    return Sparsen();
                }

                return false;
            }
        }

        Node Root;
        public Boxl Bounds { get; private set; }

        public SparseVoxelOctree(Boxl bounds)
        {
            Contract.Requires(Functions.IsPowerOf2(bounds.Width));
            Contract.Requires(Functions.IsPowerOf2(bounds.Height));
            Contract.Requires(Functions.IsPowerOf2(bounds.Depth));
            Contract.Requires(bounds.Width == bounds.Height && bounds.Height == bounds.Depth);

            Bounds = bounds;
        }       

        public T this[Point3l point]
        {
            get
            {
                Contract.Requires(Box.Contains(Bounds, point));

                return Root.Get(Bounds, point);
            }
            set
            {
                Contract.Requires(Box.Contains(Bounds, point));

                Root.Set(Bounds, point, value);
            }
        }

        
    }
}
