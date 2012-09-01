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
        struct Page
        {
            public Page(VoxelChunk<T> voxels, long timestamp)
            {
                Voxels = voxels;
                Timestamp = timestamp;
            }

            public VoxelChunk<T> Voxels;
            public long Timestamp;
        }

        public Size3l PageSize { get; private set; }
        public int Residency { get; set; }

        Dictionary<Point3l, Page> Pages = new Dictionary<Point3l, Page>();
        long Timestamp = 0;

        public VirtualVoxelGrid()
        {
            PageSize = new Size3l(16, 16, 16);
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

            Page page;
            if (Pages.TryGetValue(chunk, out page))
            {
                return page.Voxels;
            }
            else
            {
                page = new Page(new VoxelChunk<T>(new Boxl(chunk, PageSize)), Timestamp);
                Pages.Add(chunk, page);
                return page.Voxels;
            }
        }
    }
}
