using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ibasa.Numerics.Geometry;
using Ibasa.Numerics;
using System.Threading.Tasks;

namespace Ibasa.Spatial
{
    public sealed class VirtualVoxelOctree
    {
        struct Page
        {
            public Page(VoxelOctree voxels, long timestamp)
            {
                Voxels = voxels;
                Timestamp = timestamp;
            }

            public VoxelOctree Voxels;
            public long Timestamp;
        }

        public int PageSize { get; private set; }
        public int Residency { get; set; }

        Dictionary<Point3l, Page> Pages = new Dictionary<Point3l, Page>();
        long Timestamp = 0;

        public VirtualVoxelOctree()
        {
            PageSize = 16;
            Residency = 64;
        }

        public int this[Point3l point]
        {
            get
            {
                var chunk = GetChunk(point).Result;                
                return chunk[point];
            }
            set
            {
                var chunk = GetChunk(point).Result;
                chunk[point] = value;
            }
        }

        public Task<VoxelOctree> GetChunk(Point3l point)
        {
            Point3l chunk = new Point3l(
                Functions.Divide(point.X, PageSize) * PageSize,
                Functions.Divide(point.Y, PageSize) * PageSize,
                Functions.Divide(point.Z, PageSize) * PageSize);

            var pagein = PageIn(chunk);
            return pagein.ContinueWith<VoxelOctree>(task =>
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
                                    writer.Write(voxels[new Point3l(chunk.X + x, chunk.Y + y, chunk.Z + z)]);
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
                            var voxels = new VoxelOctree(chunk, PageSize);
                            var path = chunk.ToString() + ".voxels";
                            if (System.IO.File.Exists(path))
                            {
                                Ibasa.IO.BinaryReader reader = new IO.BinaryReader(System.IO.File.OpenRead(path));

                                for (long z = voxels.Bounds.Front; z < voxels.Bounds.Back; ++z)
                                {
                                    for (long x = voxels.Bounds.Left; x < voxels.Bounds.Right; ++x)
                                    {
                                        for (long y = voxels.Bounds.Bottom; y < voxels.Bounds.Top; ++y)
                                        {
                                            voxels[new Point3l(chunk.X + x, chunk.Y + y, chunk.Z + z)] = reader.ReadUInt16();
                                        }
                                    }
                                }

                                reader.Close();
                            }
                            Pages.Add(chunk, new Page(voxels, Timestamp++));
                        }
                    }
                });
        }
    }
}
