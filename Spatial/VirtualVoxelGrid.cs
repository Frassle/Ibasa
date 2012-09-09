using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ibasa.Numerics.Geometry;
using Ibasa.Numerics;
using System.Threading.Tasks;

namespace Ibasa.Spatial
{
    public sealed class VirtualVoxelGrid<T> where T : IEquatable<T>
    {
        class Page
        {
            public Page(VoxelChunk<T> voxels, ulong timestamp)
            {
                Voxels = voxels;
                Timestamp = timestamp;
            }

            public VoxelChunk<T> Voxels;
            public ulong Timestamp;
        }

        public Size3l PageSize { get; private set; }
        public int Residency { get; set; }

        Dictionary<Point3l, Page> Pages = new Dictionary<Point3l, Page>();
        ulong Timestamp = 0;
        Page LastPage;
        Point3l LastChunk = new Point3l(long.MaxValue, long.MaxValue, long.MaxValue);

        public VirtualVoxelGrid(int pageSize = 16)
        {
            PageSize = new Size3l(pageSize, pageSize, pageSize);
            Residency = 16;
        }

        public T this[Point3l point]
        {
            get
            {
                var chunk = GetChunk(point);
                return chunk[point];
            }
            set
            {
                var chunk = GetChunk(point);
                chunk[point] = value;
            }
        }

        public VoxelChunk<T> GetChunk(Point3l point)
        {
            Point3l chunk = new Point3l(
                Functions.Divide(point.X, PageSize.Width) * PageSize.Width,
                Functions.Divide(point.Y, PageSize.Height) * PageSize.Height,
                Functions.Divide(point.Z, PageSize.Depth) * PageSize.Depth);

            if (LastChunk == chunk)
            {
                return LastPage.Voxels;
            }
            LastChunk = chunk;

            Page page;
            if (Pages.TryGetValue(chunk, out page))
            {
                page.Timestamp++;

                LastPage = page;
                return page.Voxels;
            }
            else
            {
                page = new Page(new VoxelChunk<T>(new Boxl(chunk, PageSize)), Timestamp++);
                Pages.Add(chunk, page);

                if (Pages.Count >= 8 * 8 * 8)
                {
                    Point3l key = Point3l.Zero;
                    var timestamp = ulong.MaxValue;
                    foreach (var pair in Pages)
                    {
                        if (pair.Value.Timestamp <= timestamp)
                        {
                            timestamp = pair.Value.Timestamp;
                            key = pair.Key;
                        }
                    }
                    Pages.Remove(key);
                }

                LastPage = page;
                return page.Voxels;
            }
        }
    }
}
