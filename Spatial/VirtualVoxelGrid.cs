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
        public int Residency { get; private set; }

        Dictionary<Point3l, Page> Pages = new Dictionary<Point3l, Page>();
        ulong Timestamp = 0;
        Page LastPage;
        Point3l LastChunk = new Point3l(long.MaxValue, long.MaxValue, long.MaxValue);

        List<Page> UncompressedCache;

        public VirtualVoxelGrid(int pageSize = 16)
        {
            PageSize = new Size3l(pageSize, pageSize, pageSize);
            Residency = 32;
            UncompressedCache = new List<Page>();
        }

        public T this[Point3l point]
        {
            get
            {
                var chunk = GetUncompressedChunk(point);
                return chunk[point];
            }
            set
            {
                var chunk = GetUncompressedChunk(point);
                chunk[point] = value;
            }
        }

        Page GetPage(Point3l point)
        {
            Point3l chunk = new Point3l(
                Functions.Divide(point.X, PageSize.Width) * PageSize.Width,
                Functions.Divide(point.Y, PageSize.Height) * PageSize.Height,
                Functions.Divide(point.Z, PageSize.Depth) * PageSize.Depth);

            if (LastChunk == chunk)
            {
                return LastPage;
            }
            LastChunk = chunk;

            Page page;
            if (Pages.TryGetValue(chunk, out page))
            {
                page.Timestamp = Timestamp++;
            }
            else
            {
                page = new Page(new VoxelChunk<T>(new Boxl(chunk, PageSize)), Timestamp++);
                Pages.Add(chunk, page);
            }

            LastPage = page;
            return page;
        }

        public VoxelChunk<T> GetChunk(Point3l point)
        {
            var page = GetPage(point);
            return page.Voxels;
        }

        public VoxelChunk<T> GetUncompressedChunk(Point3l point)
        {
            var page = GetPage(point);

            if (!page.Voxels.IsUncompressed)
            {
                if (UncompressedCache.Count == Residency)
                {
                    int index = -1;
                    var timestamp = ulong.MaxValue;
                    for(int i=0; i < UncompressedCache.Count; ++i)
                    {
                        if (UncompressedCache[i].Timestamp <= timestamp)
                        {
                            index = i;
                        }
                    }

                    var data = UncompressedCache[index].Voxels.Flush();
                    page.Voxels.Uncompress(data);
                    UncompressedCache[index] = page;
                }
                else
                {
                    page.Voxels.Uncompress(new T[PageSize.Width * PageSize.Height * PageSize.Depth]);
                    UncompressedCache.Add(page);
                }
            }

            return page.Voxels;
        }
    }
}
