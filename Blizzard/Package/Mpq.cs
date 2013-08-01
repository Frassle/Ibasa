using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ibasa.Blizzard.Package
{
    public sealed class Mpq : Ibasa.Packaging.Package
    {
        Ibasa.IO.BinaryReader Reader;

        // Size of the archive header
        UInt32 HeaderSize;
        // Size of MPQ archive
        // This field is deprecated in the Burning Crusade MoPaQ format, and the size of the archive
        // is calculated as the size from the beginning of the archive to the end of the hash table,
        // block table, or extended block table (whichever is largest).
        UInt32 ArchiveSize;
        // 0 = Format 1 (up to The Burning Crusade)
        // 1 = Format 2 (The Burning Crusade and newer)
        // 2 = Format 3 (WoW - Cataclysm beta or newer)
        // 3 = Format 4 (WoW - Cataclysm beta or newer)
        UInt16 FormatVersion;
        // Power of two exponent specifying the number of 512-byte disk sectors in each logical sector
        // in the archive. The size of each logical sector in the archive is 512 * 2^wBlockSize.
        UInt16 BlockSize;
        // Offset to the beginning of the hash table, relative to the beginning of the archive.
        UInt32 HashTablePos;
        // Offset to the beginning of the block table, relative to the beginning of the archive.
        UInt32 BlockTablePos;
        // Number of entries in the hash table. Must be a power of two, and must be less than 2^16 for
        // the original MoPaQ format, or less than 2^20 for the Burning Crusade format.
        UInt32 HashTableSize;
        // Number of entries in the block table
        UInt32 BlockTableSize;

        // Offset to the beginning of array of 16-bit high parts of file offsets.
        UInt64 HiBlockTablePos64;
        // High 16 bits of the hash table offset for large archives.
        UInt16 HashTablePosHi;
        // High 16 bits of the block table offset for large archives.
        UInt16 BlockTablePosHi;

        // 64-bit version of the archive size
        UInt64 ArchiveSize64;
        // 64-bit position of the BET table
        UInt64 BetTablePos64;
        // 64-bit position of the HET table
        UInt64 HetTablePos64;

        // Compressed size of the hash table
        UInt64 hashtablesize64;
        // Compressed size of the block table
        UInt64 blocktablesize64;
        // Compressed size of the hi-block table
        UInt64 hiblocktablesize64;
        // Compressed size of the HET block
        UInt64 hettablesize64;
        // Compressed size of the BET block
        UInt64 bettablesize64;
        // Size of raw data chunk to calculate MD5.
        // MD5 of each data chunk follows the raw file data.
        UInt32 dawchunksize;
        // Array of MD5's
        // MD5 of the block table before decryption
        byte[] md5_blocktable;
        // MD5 of the hash table before decryption
        byte[] md5_hashtable;
        // MD5 of the hi-block table
        byte[] md5_hiblocktable;
        // MD5 of the BET table before decryption
        byte[] md5_bettable;
        // MD5 of the HET table before decryption
        byte[] md5_hettable;
        // MD5 of the MPQ header from signature to (including) MD5_HetTable
        byte[] md5_mpqheader;


        public Mpq(string path) :
            this(File.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read))
        {

        }

        public Mpq(Stream stream)
        {
            Reader = new Ibasa.IO.BinaryReader(stream, Encoding.ASCII);

            long offset = 0;
            bool founda, foundb;
            {
                Ibasa.Media.FourCC mpqa = new Media.FourCC("MPQ\x1A");
                Ibasa.Media.FourCC mpqb = new Media.FourCC("MPQ\x1B");

                //find offset
                var fourcc = Reader.Read<Ibasa.Media.FourCC>();
                founda = fourcc == mpqa;
                foundb = fourcc == mpqb;

                while (!founda && !foundb)
                {
                    offset += 512;
                    fourcc = Reader.Read<Ibasa.Media.FourCC>();
                    founda = fourcc == mpqa;
                    foundb = fourcc == mpqb;
                }
            }

            if (foundb)
            {
                var userdatasize = Reader.ReadUInt32();
                offset += Reader.ReadUInt32();
                var userdataheader = Reader.ReadUInt32();
            }

            Reader.Seek(offset, SeekOrigin.Begin);

            if (Reader.Read<Media.FourCC>() != new Media.FourCC("MPQ\x1A"))
                throw new Exception("WTF");

            HeaderSize = Reader.ReadUInt32();
            ArchiveSize = Reader.ReadUInt32();
            FormatVersion = Reader.ReadUInt16();
            BlockSize = Reader.ReadUInt16();
            HashTablePos = Reader.ReadUInt32();
            BlockTablePos = Reader.ReadUInt32();
            HashTableSize = Reader.ReadUInt32();
            BlockTableSize = Reader.ReadUInt32();

            if (FormatVersion >= 2)
            {
                HiBlockTablePos64 = Reader.ReadUInt64();
                HashTablePosHi = Reader.ReadUInt16();
                BlockTablePosHi = Reader.ReadUInt16();
            }
            if (FormatVersion >= 3)
            {
                ArchiveSize64 = Reader.ReadUInt64();
                BetTablePos64 = Reader.ReadUInt64();
                HetTablePos64 = Reader.ReadUInt64();
            }
            if (FormatVersion >= 4)
            {
                hashtablesize64 = Reader.ReadUInt64();
                blocktablesize64 = Reader.ReadUInt64();
                hiblocktablesize64 = Reader.ReadUInt64();
                hettablesize64 = Reader.ReadUInt64();
                bettablesize64 = Reader.ReadUInt64();
                dawchunksize = Reader.ReadUInt32();
                md5_blocktable = Reader.ReadBytes(16);
                md5_hashtable = Reader.ReadBytes(16);
                md5_hiblocktable = Reader.ReadBytes(16);
                md5_bettable = Reader.ReadBytes(16);
                md5_hettable = Reader.ReadBytes(16);
                md5_mpqheader = Reader.ReadBytes(16);
            }

            if (HetTablePos64 != 0)
            {
                ParseHET();
            }
            if (BetTablePos64 != 0)
            {
                ParseBET();
            }
        }
        
        // Size of the contained table
        UInt32 HetDataSize;
        // Size of the entire hash table, including the header (in bytes)
        UInt32 HetTableSize;
        // Maximum number of files in the MPQ
        UInt32 HetMaxFileCount;
        // Size of the hash table (in bytes)
        UInt32 HetHashTableSize;
        // Effective size of the hash entry (in bits)
        UInt32 HetHashEntrySize;
        // Total size of file index (in bits)
        UInt32 HetTotalIndexSize;
        // Extra bits in the file index
        UInt32 HetIndexSizeExtra;
        // Effective size of the file index (in bits)
        UInt32 HetIndexSize;
        // Size of the block index subtable (in bytes)
        UInt32 HetBlockTableSize;

        // HET hash table. Each entry is 8 bits.
        long HetHashTableOffset;

        public byte HetHashTable(uint index)
        {
            Reader.Position = HetHashTableOffset + index;
            return Reader.ReadByte();
        }

        private void ParseHET()
        {
            Reader.Seek((long)HetTablePos64, SeekOrigin.Begin);

            if (Reader.Read<Media.FourCC>() != new Media.FourCC("HET\x1A"))
                throw new Exception("WTF");

            if (Reader.ReadUInt32() != 1)
                throw new Exception("WTF");

            HetDataSize = Reader.ReadUInt32();
            HetTableSize = Reader.ReadUInt32();
            HetMaxFileCount = Reader.ReadUInt16();
            HetHashTableSize = Reader.ReadUInt16();
            HetHashEntrySize = Reader.ReadUInt32();
            HetTotalIndexSize = Reader.ReadUInt32();
            HetIndexSizeExtra = Reader.ReadUInt32();
            HetIndexSize = Reader.ReadUInt32();
            HetBlockTableSize = Reader.ReadUInt32();
            HetHashTableOffset = Reader.Position;
        }

        // Size of the contained table
        UInt32 BetDataSize;
        // Size of the entire hash table, including the header (in bytes)
        UInt32 BetTableSize;
        // Number of files in the BET table
        UInt32 BetFileCount;
        // Size of one table entry (in bits)
        UInt32 BetTableEntrySize; 
        // Bit index of the file position (within the entry record)
        UInt32 BetBitIndex_FilePos;
        // Bit index of the file size (within the entry record)
        UInt32 BetBitIndex_FileSize;
        // Bit index of the compressed size (within the entry record)
        UInt32 BetBitIndex_CmpSize;
        // Bit index of the flag index (within the entry record)
        UInt32 BetBitIndex_FlagIndex;
        // Bit index of the ??? (within the entry record)
        UInt32 BetBitIndex_Unknown;
        // Bit size of file position (in the entry record)
        UInt32 BetBitCount_FilePos; 
        // Bit size of file size (in the entry record)
        UInt32 BetBitCount_FileSize;
        // Bit size of compressed file size (in the entry record)
        UInt32 BetBitCount_CmpSize; 
        // Bit size of flags index (in the entry record)
        UInt32 BetBitCount_FlagIndex; 
        // Bit size of ??? (in the entry record)
        UInt32 BetBitCount_Unknown; 
        // Total size of the BET hash
        UInt32 BetTotalBetHashSize; 
        // Extra bits in the BET hash
        UInt32 BetBetHashSizeExtra; 
        // Effective size of BET hash (in bits)
        UInt32 BetBetHashSize; 
        // Size of BET hashes array, in bytes
        UInt32 BetBetHashArraySize; 
        // Number of flags in the following array
        UInt32 BetFlagCount; 
    
        // Followed by array of file flags. Each entry is 32-bit size and its meaning is the same like
        long BetFlagsArrayOffset;

        public UInt32 BetFlagsArray(uint index)
        {
            Reader.Position = BetFlagsArrayOffset + (index * 4);
            return Reader.ReadUInt32();
        }
    
        // File table. Size of each entry is taken from dwTableEntrySize.
        // Size of the table is (dwTableEntrySize * dwMaxFileCount), round up to 8.

        // Array of BET hashes. Table size is taken from dwMaxFileCount from HET table

        private void ParseBET()
        {
            Reader.Seek((long)BetTablePos64, SeekOrigin.Begin);

            if (Reader.Read<Media.FourCC>() != new Media.FourCC("BET\x1A"))
                throw new Exception("WTF");

            if (Reader.ReadUInt32() != 1)
                throw new Exception("WTF");

            BetDataSize = Reader.ReadUInt32();

            BetTableSize = Reader.ReadUInt32();
            BetFileCount = Reader.ReadUInt32();
            var unknown = Reader.ReadUInt32();
            BetTableEntrySize = Reader.ReadUInt32(); 
            BetBitIndex_FilePos = Reader.ReadUInt32();
            BetBitIndex_FileSize = Reader.ReadUInt32();
            BetBitIndex_CmpSize = Reader.ReadUInt32();
            BetBitIndex_FlagIndex = Reader.ReadUInt32();
            BetBitIndex_Unknown = Reader.ReadUInt32();
            BetBitCount_FilePos = Reader.ReadUInt32(); 
            BetBitCount_FileSize = Reader.ReadUInt32();
            BetBitCount_CmpSize = Reader.ReadUInt32(); 
            BetBitCount_FlagIndex = Reader.ReadUInt32(); 
            BetBitCount_Unknown = Reader.ReadUInt32(); 
            BetTotalBetHashSize = Reader.ReadUInt32(); 
            BetBetHashSizeExtra = Reader.ReadUInt32(); 
            BetBetHashSize = Reader.ReadUInt32(); 
            BetBetHashArraySize = Reader.ReadUInt32(); 
            BetFlagCount = Reader.ReadUInt32();
            BetFlagsArrayOffset = Reader.Position;
        }

        void SplitHash(ulong hash, out byte het, out ulong bet)
        {
            het = (byte)(hash >> (int)(HetHashEntrySize - 8));
            bet = hash & (ulong.MaxValue >> 8);
        }

        public override Packaging.DirectoryInfo Root
        {
            get { throw new NotImplementedException(); }
        }

        public override Packaging.DirectoryInfo GetDirectoryInfo(string path)
        {
            throw new NotImplementedException();
        }

        public override Packaging.FileInfo GetFileInfo(string path)
        {
            throw new NotImplementedException();
        }
    }
}
