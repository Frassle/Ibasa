using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.MemoryMappedFiles;

namespace Ibasa.Valve.Package
{
    public sealed class Gcf : Ibasa.Packaging.Package
    {
        public uint GCFVersion { get; private set; }
        public uint ApplicationID { get; private set; }
        public uint ApplicationVersion { get; private set; }
        uint FileSize;		// Total size of GCF file in bytes.
        uint SectorSize;	    // represents how many bytes are in each sector in the cache.
        uint SectorCount;    // represents how many total sectors are stored in the cache.

        uint BlockCount; // represents the number of blocks in the cache.
        uint BlocksUsed; // represents the number of blocks that are used.
        uint LastUsedBlock; //is the index of the last used block.

        long BlockEntryOffset;

        struct BlockEntry
        {
            public uint EntryType;		// Flags for the block entry.  0x200F0000 == Not used.
            public uint FileDataOffset;		// The offset for the data contained in this block entry in the file.
            public uint FileDataSize;		// The length of the data in this block entry.
            public uint FirstDataBlockIndex;	// The index to the first data block of this block entry's data.
            public uint NextBlockEntryIndex;	// The next block entry in the series.  (N/A if == BlockCount.)
            public uint PreviousBlockEntryIndex;	// The previous block entry in the series.  (N/A if == BlockCount.)
            public uint DirectoryIndex;		// The index of the block entry in the directory.

            public static readonly int Size = System.Runtime.InteropServices.Marshal.SizeOf(typeof(BlockEntry));
        }

        uint FirstUnusedEntry;
        uint IsLongTerminator;

        long FragmentationMapEntryOffset;


        uint ItemCount;	// Number of items in the directory.	
        uint FileCount;	// Number of files in the directory.
        uint CompressionBlockSize; //defines how many bytes are used per checksum/compressed block for each file.
        uint DirectorySize;	// Size of lpGCFDirectoryEntries & lpGCFDirectoryNames & lpGCFDirectoryInfo1Entries & lpGCFDirectoryInfo2Entries & lpGCFDirectoryCopyEntries & lpGCFDirectoryLocalEntries in bytes.
        uint NameSize;		// Size of the directory names in bytes.
        uint HashTableKeyCount;	// Number of Info1 entires.
        uint CopyCount;	// Number of files to copy.
        uint LocalCount;	// Number of files to keep local.
        uint DepotInfo;
        uint Fingerprint;

        long DirectoryEntryOffset;

        struct DirectoryEntry
        {
            public uint NameOffset;	// Offset to the directory item name from the end of the directory items.
            public uint ItemSize;		// Size of the item.  (If file, file size.  If folder, num items.)
            public uint ChecksumIndex;	// Checksum index. (0xFFFFFFFF == None).
            public uint DirectoryType;	// Flags for the directory item.  (0x00000000 == Folder).
            public uint ParentIndex;	// Index of the parent directory item.  (0xFFFFFFFF == None).
            public uint NextIndex;	// Index of the next directory item.  (0x00000000 == None).
            public uint FirstIndex;	// Index of the first directory item.  (0x00000000 == None).

            public static readonly int Size = System.Runtime.InteropServices.Marshal.SizeOf(typeof(DirectoryEntry));
        }

        long DirectoryNameOffset;


        long DirectoryInfo1EntryOffset;

        struct DirectoryInfo1Entry
        {
            public uint Dummy0;

            public static readonly int Size = System.Runtime.InteropServices.Marshal.SizeOf(typeof(DirectoryInfo1Entry));
        }

        long DirectoryInfo2EntryOffset;

        struct DirectoryInfo2Entry
        {
            public uint Dummy0;

            public static readonly int Size = System.Runtime.InteropServices.Marshal.SizeOf(typeof(DirectoryInfo2Entry));
        }

        long DirectoryCopyEntryOffset;

        struct DirectoryCopyEntry
        {
            public uint DirectoryIndex;  // Index of the directory item.

            public static readonly int Size = System.Runtime.InteropServices.Marshal.SizeOf(typeof(DirectoryCopyEntry));
        }

        long DirectoryLocalEntryOffset;

        struct DirectoryLocalEntry
        {
            public uint DirectoryIndex;  // Index of the directory item.

            public static readonly int Size = System.Runtime.InteropServices.Marshal.SizeOf(typeof(DirectoryLocalEntry));
        }

        long DirectoryMapEntryOffset;
        
       
        uint ChecksumSize;		// Size of LPGCFCHECKSUMHEADER & LPGCFCHECKSUMMAPHEADER & in bytes.
        uint ChecksumItemCount;		// Number of items.
        uint ChecksumCount;		// Number of checksums.

        long ChecksumMapEntryOffset;

        struct ChecksumMapEntry
        {
            public uint ChecksumCount;		// Number of checksums.
            public uint FirstChecksumIndex;	// Index of first checksum.

            public static readonly int Size = System.Runtime.InteropServices.Marshal.SizeOf(typeof(ChecksumMapEntry));
        }

        long ChecksumEntryOffset;

        uint FirstSectorOffset; // Offset to first data block.
        uint SectorsUsed;	// Number of data blocks that contain data.

        /*
         * GCF File Header
         * Blocks
         * Fragmentation Map
         * Block Entry Usage Map
         * Directory
         *      GCFDirHeader - GCF directory header
         *      GCFDirEntry - GCF directory entries
         *      GCF directory names
         *      GCFDirInfo1Entry - GCF directory info 1
         *      GCFDirInfo2Entry - GCF directory info 2
         *      GCFDirCopyEntry - GCF directory copy entries
         *      GCFDirLocalEntry - GCF directory local entries
         * Directory Map
         * Checksums
         * Data Blocks
         */

        Ibasa.IO.BinaryReader Reader;

        public Gcf(string path, FileShare share) :
            this(File.Open(path, FileMode.Open, FileAccess.Read, share))
        {
        }
        public Gcf(Stream stream)
        {
            Reader = new Ibasa.IO.BinaryReader(stream, Encoding.ASCII);

            {
                Reader.Seek(0, SeekOrigin.Begin);
                uint checksum = Reader.ReadBytes(40).Aggregate(0U, (sum, value) => sum + value);
                Reader.Seek(0, SeekOrigin.Begin);

                if (Reader.ReadUInt32() != 0x00000001) // HeaderVersion Always 0x00000001
                    throw new InvalidDataException("Header version mismatch.");
                if (Reader.ReadUInt32() != 0x00000001) // CacheType Always 0x00000001
                    throw new InvalidDataException("Cache type mismatch. Maybe NCF?");

                GCFVersion = Reader.ReadUInt32(); //public uint GCFVersion;
                ApplicationID = Reader.ReadUInt32(); //public uint ApplicationID;
                ApplicationVersion = Reader.ReadUInt32(); //public uint ApplicationVersion;

                Reader.ReadUInt32();
                Reader.ReadUInt32();

                FileSize = Reader.ReadUInt32(); //public uint FileSize;		// Total size of GCF file in bytes.
                SectorSize = Reader.ReadUInt32(); //public uint BlockSize;	// Size of each data block in bytes.
                SectorCount = Reader.ReadUInt32(); //public uint BlockCount;	// Number of data blocks.
                
                if (checksum != Reader.ReadUInt32()) //public uint Checksum;		// Header checksum.
                    throw new InvalidDataException("Checksum mismatch.");
            }

            {
                long checksum = 0;
                checksum += (BlockCount = Reader.ReadUInt32()); //public int BlockCount;	// Number of data blocks.
                if (BlockCount != SectorCount)
                    throw new InvalidDataException("BlockCount does not match SectorCount.");

                checksum += (BlocksUsed = Reader.ReadUInt32()); //public uint BlocksUsed;	// Number of data blocks that point to data.
                checksum += (LastUsedBlock = Reader.ReadUInt32()); //public uint LastUsedBlock;	//is the index of the last used block.
                
                checksum += Reader.ReadUInt32(); //public uint Dummy0;
                checksum += Reader.ReadUInt32(); //public uint Dummy1;
                checksum += Reader.ReadUInt32(); //public uint Dummy2;
                checksum += Reader.ReadUInt32(); //public uint Dummy3;

                if (checksum != Reader.ReadUInt32()) //public uint Checksum;		// Header checksum.
                    throw new InvalidDataException("Checksum mismatch.");
            }

            BlockEntryOffset = Reader.BaseStream.Position;
            //Seek past block entries
            Reader.Seek(BlockCount * BlockEntry.Size, SeekOrigin.Current);

            { //FragmentationMapHeader
                long checksum = 0;
                uint sectorCount = Reader.ReadUInt32();
                checksum += sectorCount;
                if (SectorCount != sectorCount)
                    throw new InvalidDataException();

                checksum += (FirstUnusedEntry = Reader.ReadUInt32());
                checksum += (IsLongTerminator = Reader.ReadUInt32());

                if (checksum != Reader.ReadUInt32()) //public uint Checksum;		// Header checksum.
                    throw new InvalidDataException();
            }

            FragmentationMapEntryOffset = Reader.BaseStream.Position;
            //Seek past block entries
            Reader.Seek(BlockCount * 4, SeekOrigin.Current);

            { //BlockMapHeader
                if (GCFVersion <= 5)
                {
                    uint blockCount = Reader.ReadUInt32();
                    uint firstBlockEntryIndex = Reader.ReadUInt32();
                    uint lastBlockEntryIndex = Reader.ReadUInt32();
                    uint dummy0 = Reader.ReadUInt32();
                    uint checksum = Reader.ReadUInt32();

                    //struct BlockMapEntry
                    //{
                    //    public uint PreviousBlockEntryIndex;	// The previous block entry.  (N/A if == BlockCount.)
                    //    public uint NextBlockEntryIndex;	// The next block entry.  (N/A if == BlockCount.)
                    //}

                    Reader.Seek(blockCount * 8, SeekOrigin.Current);
                }
            }

            { //DirectoryHeader
                long checksum = 0;

                if (Reader.ReadUInt32() != 0x00000004) // HeaderVersion Always 0x00000004
                    throw new InvalidDataException("Header version mismatch.");
                if (Reader.ReadUInt32() != ApplicationID) // ApplicationID
                    throw new InvalidDataException("ApplicationID mismatch.");
                if (Reader.ReadUInt32() != ApplicationVersion) // ApplicationVersion
                    throw new InvalidDataException("ApplicationVersion mismatch.");

                ItemCount = Reader.ReadUInt32(); //public uint ItemCount;	// Number of items in the directory.	
                FileCount = Reader.ReadUInt32(); //public uint FileCount;	// Number of files in the directory.
                CompressionBlockSize = Reader.ReadUInt32(); //public uint CompressionBlockSize;   // defines how many bytes are used per checksum/compressed block for each file.
                DirectorySize = Reader.ReadUInt32(); //public uint DirectorySize;	// Size of lpGCFDirectoryEntries & lpGCFDirectoryNames & lpGCFDirectoryInfo1Entries & lpGCFDirectoryInfo2Entries & lpGCFDirectoryCopyEntries & lpGCFDirectoryLocalEntries in bytes.
                NameSize = Reader.ReadUInt32(); //public uint NameSize;		// Size of the directory names in bytes.
                HashTableKeyCount = Reader.ReadUInt32(); //public uint HashTableKeyCount;	// Number of Info1 entires.
                CopyCount = Reader.ReadUInt32(); //public uint CopyCount;	// Number of files to copy.
                LocalCount = Reader.ReadUInt32(); //public uint LocalCount;	// Number of files to keep local.
                DepotInfo = Reader.ReadUInt32();
                Fingerprint = Reader.ReadUInt32();

                if (checksum != Reader.ReadUInt32()) //public uint Checksum;		// Header checksum.
                { } //throw new InvalidDataException();
            }

            DirectoryEntryOffset = Reader.BaseStream.Position;
            Reader.Seek(ItemCount * DirectoryEntry.Size, SeekOrigin.Current);

            DirectoryNameOffset = Reader.BaseStream.Position;
            Reader.Seek(NameSize, SeekOrigin.Current);

            DirectoryInfo1EntryOffset = Reader.BaseStream.Position;
            Reader.Seek(HashTableKeyCount * DirectoryInfo1Entry.Size, SeekOrigin.Current);

            DirectoryInfo2EntryOffset = Reader.BaseStream.Position;
            Reader.Seek(ItemCount * DirectoryInfo2Entry.Size, SeekOrigin.Current);

            DirectoryCopyEntryOffset = Reader.BaseStream.Position;
            Reader.Seek(CopyCount * DirectoryCopyEntry.Size, SeekOrigin.Current);

            DirectoryLocalEntryOffset = Reader.BaseStream.Position;
            Reader.Seek(LocalCount * DirectoryLocalEntry.Size, SeekOrigin.Current);

            { //DirectoryMapHeader
                //public uint Dummy0;
                //public uint Dummy1;
                Reader.Seek(8, SeekOrigin.Current);
            }

            DirectoryMapEntryOffset = Reader.BaseStream.Position;
            Reader.Seek(ItemCount * 4, SeekOrigin.Current);

            { //ChecksumHeader ChecksumMapHeader
                if (Reader.ReadUInt32() != 0x00000001) // HeaderVersion Always 0x00000001
                    throw new InvalidDataException();

                ChecksumSize = Reader.ReadUInt32();	// Size of LPGCFCHECKSUMHEADER & LPGCFCHECKSUMMAPHEADER & in bytes.

                if (Reader.ReadUInt32() != 0x14893721) // FormatCode  Always 0x14893721
                    throw new InvalidDataException();
                if (Reader.ReadUInt32() != 0x00000001) // Dummy0  Always 0x00000001
                    throw new InvalidDataException();

                ChecksumItemCount = Reader.ReadUInt32(); // Number of file ID entries.
                ChecksumCount = Reader.ReadUInt32(); // Number of checksums.
            }

            ChecksumMapEntryOffset = Reader.BaseStream.Position;
            Reader.Seek(ChecksumItemCount * ChecksumMapEntry.Size, SeekOrigin.Current);

            ChecksumEntryOffset = Reader.BaseStream.Position;
            Reader.Seek(ChecksumCount * 4, SeekOrigin.Current);

            //Skip past signature
            Reader.Seek(0x80, SeekOrigin.Current);

            if (Reader.ReadUInt32() != ApplicationVersion) // ApplicationVersion
                throw new InvalidDataException("ApplicationVersions do not match.");

            { //DataHeader
                uint checksum = 0;
                uint sectorCount, sectorSize;

                checksum += (sectorCount = Reader.ReadUInt32());	// Number of data blocks.
                if (SectorCount != sectorCount)
                    throw new InvalidDataException();

                checksum += (sectorSize = Reader.ReadUInt32()); // Size of each data block in bytes.
                if (SectorSize != sectorSize)
                    throw new InvalidDataException();

                checksum += (FirstSectorOffset = Reader.ReadUInt32()); // Offset to first data block.
                checksum += (SectorsUsed = Reader.ReadUInt32()); // Number of data blocks that contain data.

                if (checksum != Reader.ReadUInt32()) //public uint Checksum;		// Header checksum.
                    throw new InvalidDataException();
            }

            for (uint index = 0; index < ItemCount; ++index)
            {
                DirectoryEntry entry = GetDirectoryEntry(index);

                if (entry.ParentIndex == 0xFFFFFFFF)
                {
                    RootDirectory = new GcfDirectoryInfo(this, index);
                    break;
                }
            }
        }

        BlockEntry GetBlockEntry(uint index)
        {
            Reader.Seek(BlockEntryOffset + index * BlockEntry.Size, SeekOrigin.Begin);

            BlockEntry entry;

            entry.EntryType = Reader.ReadUInt32(); //public long EntryType;		// Flags for the block entry.  0x200F0000 == Not used.
            entry.FileDataOffset = Reader.ReadUInt32(); //public long FileDataOffset;		// The offset for the data contained in this block entry in the file.
            entry.FileDataSize = Reader.ReadUInt32(); //public long FileDataSize;		// The length of the data in this block entry.
            entry.FirstDataBlockIndex = Reader.ReadUInt32(); //public long FirstDataBlockIndex;	// The index to the first data block of this block entry's data.
            entry.NextBlockEntryIndex = Reader.ReadUInt32(); //public long NextBlockEntryIndex;	// The next block entry in the series.  (N/A if == BlockCount.)
            entry.PreviousBlockEntryIndex = Reader.ReadUInt32(); //public long PreviousBlockEntryIndex;	// The previous block entry in the series.  (N/A if == BlockCount.)
            entry.DirectoryIndex = Reader.ReadUInt32(); //public long DirectoryIndex;		// The index of the block entry in the directory.

            return entry;
        }

        uint GetFragmentationMapEntry(uint index)
        {
            Reader.Seek(FragmentationMapEntryOffset + index * 4, SeekOrigin.Begin);

            return Reader.ReadUInt32(); // The index of the next data block.
        }

        DirectoryEntry GetDirectoryEntry(uint index)
        {
            Reader.Seek(DirectoryEntryOffset + index * DirectoryEntry.Size, SeekOrigin.Begin);

            DirectoryEntry entry;

            entry.NameOffset = Reader.ReadUInt32(); //public uint NameOffset;	// Offset to the directory item name from the end of the directory items.
            entry.ItemSize = Reader.ReadUInt32(); //public uint ItemSize;		// Size of the item.  (If file, file size.  If folder, num items.)
            entry.ChecksumIndex = Reader.ReadUInt32(); //public uint ChecksumIndex;	// Checksum index. (0xFFFFFFFF == None).
            entry.DirectoryType = Reader.ReadUInt32(); //public uint DirectoryType;	// Flags for the directory item.  (0x00000000 == Folder).
            entry.ParentIndex = Reader.ReadUInt32(); // Index of the parent directory item.  (0xFFFFFFFF == None).
            entry.NextIndex = Reader.ReadUInt32(); // Index of the next directory item.  (0x00000000 == None).
            entry.FirstIndex = Reader.ReadUInt32(); // Index of the first directory item.  (0x00000000 == None).

            return entry;
        }

        string GetDirectoryName(uint offset)
        {
            Reader.Seek(DirectoryNameOffset + offset, SeekOrigin.Begin);

            StringBuilder builder = new StringBuilder();

            char c;
            while((c = Reader.ReadChar()) != '\0')
            {
                builder.Append(c);
            }

            return builder.ToString();
        }

        uint GetDirectoryMapEntry(uint index)
        {
            Reader.Seek(DirectoryMapEntryOffset + index * 4, SeekOrigin.Begin);

            return Reader.ReadUInt32(); // FirstBlockIndex;	// Index of the first data block. (N/A if == BlockCount.)
        }

        int GetDataBlock(uint blockIndex, uint blockOffset, byte[] buffer, int index, int count)
        {
            Reader.Seek(FirstSectorOffset + blockIndex * SectorSize + blockOffset, SeekOrigin.Begin);
            return Reader.Read(buffer, index, Math.Min(count, (int)(SectorSize - blockOffset)));
        }

        #region Packaging
        private sealed class GcfDirectoryInfo : Ibasa.Packaging.DirectoryInfo
        {
            private readonly Gcf Gcf;
            private readonly uint Index;
            private readonly string Path;
            
            private readonly uint ParentIndex;

            public GcfDirectoryInfo(Gcf gcf, uint index)
            {
                Gcf = gcf;
                Index = index;

                if (Index == 0xFFFFFFFF)
                {
                    ParentIndex = 0xFFFFFFFF;
                    Path = null;
                    return;
                }

                List<string> path = new List<string>();

                DirectoryEntry entry, parent;
                entry = parent = Gcf.GetDirectoryEntry(Index);

                do
                {
                    path.Add(gcf.GetDirectoryName(parent.NameOffset));
                    if(parent.ParentIndex != 0xFFFFFFFF)
                        parent = gcf.GetDirectoryEntry(parent.ParentIndex);
                } while (parent.ParentIndex != 0xFFFFFFFF);

                path.Reverse();
                Path = string.Join(Gcf.Path.DirectorySeparatorChars[0].ToString(), path);

                ParentIndex = entry.ParentIndex;
            }

            public GcfDirectoryInfo(Gcf gcf, string path)
            {
                Gcf = gcf;
                Path = path;

                path = Gcf.Path.GetFullPath(path);

                string[] names = path.Split(Gcf.Path.DirectorySeparatorChars);

                Packaging.DirectoryInfo dir = Gcf.Root;

                for (int i = 0; i < names.Length; ++i)
                {
                    var children = dir.GetDirectories(names[i]);

                    if (children.Length != 1)
                    {
                        dir = null;
                        break;
                    }
                    
                    dir = children[0];
                }

                GcfDirectoryInfo gcfdir = dir as GcfDirectoryInfo;

                if (gcfdir != null)
                {
                    Index = gcfdir.Index;
                    ParentIndex = gcfdir.ParentIndex;
                }
                else
                {
                    Index = 0xFFFFFFFF;
                    ParentIndex = 0xFFFFFFFF;
                }
            }

            public override Packaging.DirectoryInfo Root
            {
                get { return Gcf.Root; }
            }

            public override Packaging.DirectoryInfo Parent
            {
                get
                {
                    return ParentIndex == 0xFFFFFFFF ? null :
                        new GcfDirectoryInfo(Gcf, ParentIndex);
                }
            }

            public override void Create()
            {
                throw new NotSupportedException("GCFs are readonly.");
            }

            public override Packaging.DirectoryInfo CreateSubdirectory(string path)
            {
                throw new NotSupportedException("GCFs are readonly.");
            }

            public override void Delete(bool recursive)
            {
                throw new NotSupportedException("GCFs are readonly.");
            }

            public override string Name 
            {
                get { return Gcf.Path.GetFileName(Path); }
            }

            public override string FullName
            {
                get { return Path; }
            }

            public override string Extension
            {
                get { return Gcf.Path.GetExtension(Path); }
            }

            public override bool Exists
            {
                get { return Index != 0xFFFFFFFF; }
            }

            public override IEnumerator<Packaging.FileSystemInfo> GetEnumerator()
            {
                DirectoryEntry entry = Gcf.GetDirectoryEntry(Index);

                uint index = entry.FirstIndex;

                while (index != 0)
                {
                    entry = Gcf.GetDirectoryEntry(index);

                    if (entry.DirectoryType == 0)
                        yield return new GcfDirectoryInfo(Gcf, index);
                    else
                        yield return new GcfFileInfo(Gcf, index);

                    index = entry.NextIndex;
                }                
            }
        }

        private sealed class GcfFileInfo : Ibasa.Packaging.FileInfo
        {
            private readonly Gcf Gcf;
            private readonly uint Index;
            private readonly string Path;

            private readonly uint ParentIndex;
            private readonly uint ItemSize;

            public GcfFileInfo(Gcf gcf, uint index)
            {
                Gcf = gcf;
                Index = index;

                if (Index == 0xFFFFFFFF)
                {
                    ParentIndex = 0xFFFFFFFF;
                    Path = null;
                    ItemSize = 0;
                    return;
                }

                List<string> path = new List<string>();

                DirectoryEntry entry, parent;
                entry = parent = Gcf.GetDirectoryEntry(Index);

                do
                {
                    path.Add(gcf.GetDirectoryName(parent.NameOffset));
                    if (parent.ParentIndex != 0xFFFFFFFF)
                        parent = gcf.GetDirectoryEntry(parent.ParentIndex);
                } while (parent.ParentIndex != 0xFFFFFFFF);

                path.Reverse();
                Path = string.Join(Gcf.Path.DirectorySeparatorChars[0].ToString(), path);

                ParentIndex = entry.ParentIndex;
                ItemSize = entry.ItemSize;
            }

            public GcfFileInfo(Gcf gcf, string path)
            {
                Gcf = gcf;
                Path = path;

                path = Gcf.Path.GetFullPath(path);

                string[] names = path.Split(Gcf.Path.DirectorySeparatorChars);

                Packaging.DirectoryInfo dir = Gcf.Root;

                for (int i = 0; i < names.Length - 1; ++i)
                {
                    var children = dir.GetDirectories(names[i]);

                    if (children.Length != 1)
                    {
                        dir = null;
                        break;
                    }

                    dir = children[0];
                }

                if (dir != null)
                {
                    var children = dir.GetFiles(names[names.Length - 1]);

                    if (children.Length != 1)
                    {
                        Index = 0xFFFFFFFF;
                        ParentIndex = 0xFFFFFFFF;
                        ItemSize = 0;
                    }
                    else
                    {
                        GcfFileInfo gcffile = children[0] as GcfFileInfo;

                        Index = gcffile.Index;
                        ParentIndex = gcffile.ParentIndex;
                        ItemSize = gcffile.ItemSize;
                    }
                }
                else
                {
                    Index = 0xFFFFFFFF;
                    ParentIndex = 0xFFFFFFFF;
                    ItemSize = 0;
                }
            }

            public override long Length
            {
                get { return ItemSize; }
            }

            public override Packaging.DirectoryInfo Directory
            {
                get { return new GcfDirectoryInfo(Gcf, ParentIndex); }
            }

            public override Stream Create()
            {
                throw new NotSupportedException("GCFs are readonly.");
            }

            public override Stream Open(FileMode mode, FileAccess access, FileShare share)
            {
                if (mode != FileMode.Open || access != FileAccess.Read)
                    throw new ArgumentException("GCFs are readonly.");

                return new GcfStream(Gcf, Index);
            }

            public override string Name { get { return Gcf.Path.GetFileName(Path); } }

            public override string FullName { get { return Path; } }

            public override string Extension { get { return Gcf.Path.GetExtension(Path); } }

            public override bool Exists
            {
                get { return Index != 0xFFFFFFFF; }
            }

            public override void Delete()
            {
                throw new NotSupportedException("GCFs are readonly.");
            }
        }

        private sealed class GcfStream : System.IO.Stream
        {
            private readonly Gcf Gcf;
            private readonly uint Index;

            private readonly long Size;
            private readonly List<uint> FAT;
            
            public GcfStream(Gcf gcf, uint index)
            {
                Gcf = gcf;
                Index = index;

                Size = Gcf.GetDirectoryEntry(Index).ItemSize;
                Position = 0;

                FAT = new List<uint>();

                var block = Gcf.GetBlockEntry(Gcf.GetDirectoryMapEntry(Index));
                uint data = block.FirstDataBlockIndex;
                
                while(data != (Gcf.IsLongTerminator == 0 ? 0x0000ffff : 0xffffffff))
                {
                    FAT.Add(data);
                    data = Gcf.GetFragmentationMapEntry(data);
                }
            }

            public override bool CanRead
            {
                get { return true; }
            }

            public override bool CanSeek
            {
                get { return true; }
            }

            public override bool CanWrite
            {
                get { return false; }
            }

            public override void Flush()
            {
                
            }

            public override long Length
            {
                get { return Size; }
            }

            public override long Position
            {
                get;
                set;
            }

            public override int Read(byte[] buffer, int offset, int count)
            {
                if (Position == Length)
                    return 0;

                int sectorIndex = (int)(Position / Gcf.SectorSize);
                uint sectorOffset = (uint)(Position % Gcf.SectorSize);                

                count = Gcf.GetDataBlock(FAT[sectorIndex], sectorOffset, buffer, offset, count);
                Position = Math.Min(Length, Position + count);
                return count;
            }

            public override long Seek(long offset, SeekOrigin origin)
            {
                switch (origin)
                {
                    case SeekOrigin.Begin:
                        Position = offset; break;
                    case SeekOrigin.Current:
                        Position += offset; break;
                    case SeekOrigin.End:
                        Position = Length + offset; break;
                }

                return Position;
            }

            public override void SetLength(long value)
            {
                throw new NotSupportedException("GCFs are readonly.");
            }

            public override void Write(byte[] buffer, int offset, int count)
            {
                throw new NotSupportedException("GCFs are readonly.");
            }
        }

        private sealed class GcfPath : Ibasa.Packaging.Path
        {
            public override char[] DirectorySeparatorChars
            {
                get { return new char[] { '\\' }; }
            }

            public override string GetPathRoot(string path)
            {
                if (path == null)
                    return null;

                return string.Empty;
            }

            public override bool IsPathRooted(string path)
            {
                if (path == null)
                    return false;

                if (path.Length == 0)
                    return false;

                return DirectorySeparatorChars.Any(c => c != path[0]);
            }
        }

        private static GcfPath PathInstance = new GcfPath();
        private GcfDirectoryInfo RootDirectory;

        public override Packaging.DirectoryInfo Root
        {
            get { return RootDirectory; }
        }
        
        public override Packaging.Path Path
        {
            get { return PathInstance; }
        }

        public override Packaging.DirectoryInfo GetDirectoryInfo(string path)
        {
            return new GcfDirectoryInfo(this, path);
        }

        public override Packaging.FileInfo GetFileInfo(string path)
        {
            return new GcfFileInfo(this, path);
        }
        #endregion       
    }
}
