using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ibasa.Numerics.Geometry;
using Ibasa.Numerics;
using System.Threading.Tasks;

namespace Ibasa.Spatial
{
    public abstract class VirtualVoxelVolume<T> where T : IEquatable<T>
    {
        class Page
        {
            public Page(CompressedVoxelVolume<T> voxels, ulong timestamp)
            {
                Voxels = voxels;
                Timestamp = timestamp;
            }

            public CompressedVoxelVolume<T> Voxels;
            public ulong Timestamp;
        }

        public Size3l PageSize { get; private set; }
        public int Capacity { get; private set; }
        public int UncompressedCapacity { get { return UncompressedCache.Capacity; } private set { UncompressedCache.Capacity = value; } }

        Dictionary<Point3l, Page> Pages = new Dictionary<Point3l, Page>();
        ulong Timestamp = 0;
        Page LastPage;
        Point3l LastChunk = new Point3l(long.MaxValue, long.MaxValue, long.MaxValue);

        Ibasa.Collections.Cache<Page> UncompressedCache;

        public VirtualVoxelVolume(int pageSize = 16, int uncompressedCapacity = 32, int capacity = 1024)
        {
            PageSize = new Size3l(pageSize, pageSize, pageSize);
            UncompressedCache = new Collections.Cache<Page>(uncompressedCapacity, EvictPage);
            Capacity = capacity;
        }

        static bool EvictPage(Page current, Page candidate)
        {
            return candidate.Timestamp < current.Timestamp;
        }

        public T this[Point3l point]
        {
            get
            {
                return Get(point);
            }
            set
            {
                Set(point, value);
            }
        }

        public T Get(Point3l point)
        {
            var chunk = GetUncompressedChunk(point);
            return chunk[point];
        }

        public void Set(Point3l point, T value)
        {
            var chunk = GetUncompressedChunk(point);
            chunk[point] = value;
        }

        void DecompressPage(Page page)
        {
            if (!page.Voxels.IsUncompressed)
            {
                var evict = UncompressedCache.Add(page);

                T[] data;
                if (evict == null)
                {
                    data = evict.Voxels.Flush();
                }
                else
                {
                    data = new T[PageSize.Width * PageSize.Height * PageSize.Depth];
                }
                page.Voxels.Decompress(data);
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
                if (Pages.Count == Capacity)
                {
                    var enumerator = Pages.GetEnumerator();
                    if (enumerator.MoveNext())
                    {
                        KeyValuePair<Point3l, Page> evict = enumerator.Current;

                        while (enumerator.MoveNext())
                        {
                            if (enumerator.Current.Value.Timestamp < evict.Value.Timestamp)
                            {
                                evict = enumerator.Current;
                            }
                        }

                        DataOverflow(evict.Value.Voxels);
                        Pages.Remove(evict.Key);
                    }
                }

                page = new Page(new CompressedVoxelVolume<T>(new Boxl(chunk, PageSize)), Timestamp++);
                Pages.Add(chunk, page);
                DecompressPage(page);
                DataRequired(page.Voxels);
            }

            LastPage = page;
            return page;
        }

        public CompressedVoxelVolume<T> GetChunk(Point3l point)
        {
            var page = GetPage(point);
            return page.Voxels;
        }

        public CompressedVoxelVolume<T> GetUncompressedChunk(Point3l point)
        {
            var page = GetPage(point);
            DecompressPage(page);
            return page.Voxels;
        }

        protected abstract void DataRequired(CompressedVoxelVolume<T> chunk);
        protected abstract void DataOverflow(CompressedVoxelVolume<T> chunk);
    }
}
