using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ibasa.Numerics.Geometry;
using Ibasa.Numerics;
using System.Threading.Tasks;

namespace Ibasa.Spatial
{
    public sealed class VirtualVoxelGrid<T> where T : struct
    {
        struct Page
        {
            public Page(VoxelGrid<T> voxels, long timestamp)
            {
                Voxels = voxels;
                Timestamp = timestamp;
            }

            public VoxelGrid<T> Voxels;
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
                return chunk.Result[point];
            }
            set
            {
                var chunk = GetChunk(point);
                chunk.Result[point] = value;
            }
        }

        public Task<VoxelGrid<T>> GetChunk(Point3l point)
        {
            Point3l chunk = new Point3l(
                Functions.Divide(point.X, PageSize.Width) * PageSize.Width,
                Functions.Divide(point.Y, PageSize.Height) * PageSize.Height,
                Functions.Divide(point.Z, PageSize.Depth) * PageSize.Depth);

            var pagein = PageIn(chunk);
            return pagein.ContinueWith<VoxelGrid<T>>(task =>
                {
                    Page page;
                    Pages.TryGetValue(chunk, out page);
                    page.Timestamp = Timestamp++;
                    return page.Voxels;
                });
        }

        private Task PageOut(Point3l chunk)
        {
            return Task.Factory.StartNew(() =>
                {
                    Page page;
                    if (Pages.TryGetValue(chunk, out page))
                    {
                        Ibasa.IO.BinaryWriter writer = new IO.BinaryWriter(System.IO.File.OpenWrite(chunk.ToString() + ".voxels"));
                        var voxels = page.Voxels;

                        for (long z = voxels.Bounds.Front; z < voxels.Bounds.Back; ++z)
                        {
                            for (long x = voxels.Bounds.Left; x < voxels.Bounds.Right; ++x)
                            {
                                for (long y = voxels.Bounds.Bottom; y < voxels.Bounds.Top; ++y)
                                {
                                    writer.Write<T>(voxels[new Point3l(chunk.X + x, chunk.Y + y, chunk.Z + z)]);
                                }
                            }
                        }

                        writer.Close();

                        Pages.Remove(chunk);
                    }
                });
        }

        private Task PageIn(Point3l chunk)
        {
            return Task.Factory.StartNew(() =>
                {
                    Page page;
                    if (!Pages.TryGetValue(chunk, out page))
                    {
                        if (Pages.Count > Residency)
                        {
                            PageOut(Pages.OrderBy(pair => pair.Value.Timestamp).First().Key).Wait();
                        }
                        else
                        {
                            var voxels = new VoxelGrid<T>(new Boxl(chunk, PageSize));

                            try
                            {
                                Ibasa.IO.BinaryReader reader = new IO.BinaryReader(System.IO.File.OpenRead(chunk.ToString() + ".voxels"));

                                for (long z = voxels.Bounds.Front; z < voxels.Bounds.Back; ++z)
                                {
                                    for (long x = voxels.Bounds.Left; x < voxels.Bounds.Right; ++x)
                                    {
                                        for (long y = voxels.Bounds.Bottom; y < voxels.Bounds.Top; ++y)
                                        {
                                            voxels[new Point3l(chunk.X + x, chunk.Y + y, chunk.Z + z)] = reader.Read<T>();
                                        }
                                    }
                                }

                                reader.Close();
                            }
                            catch (System.IO.FileNotFoundException)
                            {
                            }

                            Pages.Add(chunk, new Page(voxels, Timestamp++));
                        }
                    }
                });
        }
    }
}
