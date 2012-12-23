using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.MemoryMappedFiles;

namespace Ibasa.Valve
{
    public sealed class Gcf : Ibasa.Packaging.Package
    {
        const long FileHeaderOffset = 0;
        const long FileHeaderSize = 44;

        const long BlockEntryHeaderOffset = FileHeaderOffset + FileHeaderSize;
        const long BlockEntryHeaderSize = 20;

        const long BlockEntryOffset = BlockEntryHeaderOffset + BlockEntryHeaderSize;
        const long BlockEntrySize = 38;

        long FragmentationMapHeaderOffset { get { return BlockEntryOffset + BlockEntrySize * FileBlockCount; } }
        const long FragmentationMapHeaderSize = 16;

        long FragmentationMapOffset { get { return FragmentationMapHeaderOffset + FragmentationMapHeaderSize;}}
        const long FragmentationMapSize = 4;

        MemoryMappedFile File;
        MemoryMappedViewAccessor FileHeader;
        MemoryMappedViewAccessor BlockEntryHeader;
        int BlockEntryIndex;
        MemoryMappedViewAccessor BlockEntry;
        MemoryMappedViewAccessor FragmentationMapHeader;
        int FragmentationMapIndex;
        MemoryMappedViewAccessor FragmentationMap;

        public Gcf(string path, bool isVolatile) :
            this(new FileStream(path, FileMode.Open, FileAccess.Read, isVolatile ? FileShare.ReadWrite : FileShare.Read))
        { }
        public Gcf(FileStream fileStream)
        {
            File = MemoryMappedFile.CreateFromFile(fileStream, System.IO.Path.GetFileNameWithoutExtension(fileStream.Name), 0,
                MemoryMappedFileAccess.Read, null, HandleInheritability.None, true);

            FileHeader = File.CreateViewAccessor(FileHeaderOffset, FileHeaderSize, MemoryMappedFileAccess.Read);
            BlockEntryHeader = File.CreateViewAccessor(BlockEntryHeaderOffset, BlockEntryHeaderSize, MemoryMappedFileAccess.Read);

            BlockEntryIndex = 0;
            BlockEntry = File.CreateViewAccessor(BlockEntryOffset, BlockEntrySize, MemoryMappedFileAccess.Read);

            FragmentationMapHeader = File.CreateViewAccessor(
                FragmentationMapHeaderOffset, FragmentationMapHeaderSize, MemoryMappedFileAccess.Read);

            FragmentationMapIndex = 0;
            FragmentationMap = File.CreateViewAccessor(FragmentationMapOffset, FragmentationMapSize, MemoryMappedFileAccess.Read);
        }

        #region tagGCFHEADER
        public int CacheId { get { return FileHeader.ReadInt32(3 * 4); } }
        public int GCFVersion { get { return FileHeader.ReadInt32(4 * 4); } }
        public int FileSize { get { return FileHeader.ReadInt32(7 * 4); } }
        public int BlockSize { get { return FileHeader.ReadInt32(8 * 4); } }
        public int FileBlockCount { get { return FileHeader.ReadInt32(9 * 4); } }
        #endregion

        #region tagGCFBLOCKENTRYHEADER
        public int BlockEntryBlockCount { get { return BlockEntryHeader.ReadInt32(0 * 4); } }
        public int BlocksUsed { get { return BlockEntryHeader.ReadInt32(1 * 4); } }
        #endregion

        #region tagGCFBLOCKENTRY
        private void SetBlockEntry(int index)
        {
            if (BlockEntryIndex != index)
            {
                BlockEntry.Dispose();
                BlockEntry = File.CreateViewAccessor(
                    BlockEntryOffset + (BlockEntrySize * index), BlockEntrySize, MemoryMappedFileAccess.Read);
            }
        }

        public int EntryType(int index)
        {
            SetBlockEntry(index);
            return BlockEntry.ReadInt32(0 * 4);
        }
        public long FileDataOffset(int index)
        {
            SetBlockEntry(index);
            return BlockEntry.ReadUInt32(1 * 4);
        }
        public long FileDataSize(int index)
        {
            SetBlockEntry(index);
            return BlockEntry.ReadUInt32(2 * 4);
        }
        public int FirstDataBlockIndex(int index)
        {
            SetBlockEntry(index);
            return BlockEntry.ReadInt32(3 * 4);
        }
        public int NextBlockEntryIndex(int index)
        {
            SetBlockEntry(index);
            return BlockEntry.ReadInt32(4 * 4);
        }
        public int PreviousBlockEntryIndex(int index)
        {
            SetBlockEntry(index);
            return BlockEntry.ReadInt32(5 * 4);
        }
        public int DirectoryIndex(int index)
        {
            SetBlockEntry(index);
            return BlockEntry.ReadInt32(6 * 4);
        }
        #endregion

        #region tagGCFFRAGMAPHEADER
        public int FragmentationMapBlockCount { get { return FragmentationMapHeader.ReadInt32(0 * 4); } }
        #endregion  
        
        #region tagGCFFRAGMAP
        private void SetFragmentationMap(int index)
        {
            if (FragmentationMapIndex != index)
            {
                FragmentationMap.Dispose();
                FragmentationMap = File.CreateViewAccessor(
                    FragmentationMapOffset + (FragmentationMapSize * index), FragmentationMapSize, MemoryMappedFileAccess.Read);
            }
        }

        public int NextDataBlockIndex(int index)
        {
            SetFragmentationMap(index);
            return FragmentationMap.ReadInt32(0 * 4);
        }
        #endregion

        #region Packaging
        private sealed class GcfDirectoryInfo : Ibasa.Packaging.DirectoryInfo
        {
            public override Packaging.DirectoryInfo Root
            {
                get { throw new NotImplementedException(); }
            }

            public override Packaging.DirectoryInfo Parent
            {
                get { throw new NotImplementedException(); }
            }

            public override void Create()
            {
                throw new NotImplementedException();
            }

            public override Packaging.DirectoryInfo CreateSubdirectory(string path)
            {
                throw new NotImplementedException();
            }

            public override void Delete(bool recursive)
            {
                throw new NotImplementedException();
            }

            public override IEnumerable<Packaging.DirectoryInfo> EnumerateDirectories(string searchPattern, SearchOption searchOption)
            {
                throw new NotImplementedException();
            }

            public override IEnumerable<Packaging.FileInfo> EnumerateFiles(string searchPattern, SearchOption searchOption)
            {
                throw new NotImplementedException();
            }

            public override string Name
            {
                get { throw new NotImplementedException(); }
            }

            public override string FullName
            {
                get { throw new NotImplementedException(); }
            }

            public override string Extension
            {
                get { throw new NotImplementedException(); }
            }

            public override bool Exists
            {
                get { throw new NotImplementedException(); }
            }
        }

        private sealed class GcfFileInfo : Ibasa.Packaging.FileInfo
        {
            public override long Length
            {
                get { throw new NotImplementedException(); }
            }

            public override Packaging.DirectoryInfo Directory
            {
                get { throw new NotImplementedException(); }
            }

            public override Stream Create()
            {
                throw new NotImplementedException();
            }

            public override Stream Open(FileMode mode, FileAccess access, FileShare share)
            {
                throw new NotImplementedException();
            }

            public override string Name
            {
                get { throw new NotImplementedException(); }
            }

            public override string FullName
            {
                get { throw new NotImplementedException(); }
            }

            public override string Extension
            {
                get { throw new NotImplementedException(); }
            }

            public override bool Exists
            {
                get { throw new NotImplementedException(); }
            }

            public override void Delete()
            {
                throw new NotImplementedException();
            }
        }

        public override Packaging.DirectoryInfo Root
        {
            get { throw new NotImplementedException(); }
        }

        public override Packaging.DirectoryInfo CreateDirectoryInfo(string path)
        {
            throw new NotImplementedException();
        }

        public override Packaging.FileInfo CreateFileInfo(string fileName)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
