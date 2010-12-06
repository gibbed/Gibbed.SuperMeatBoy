using System;
using System.IO;
using Gibbed.Helpers;

namespace Gibbed.SuperMeatBoy.FileFormats
{
    public class SaveFile
    {
        public uint UnlockedCharacterFlags;
        public uint UnlockedChapterFlags;
        public uint TotalDeaths;
        /* Something to do with the last played chapter?
         * It's value is 600, same as the number of level records,
         * but it doesn't affect how many level records are read. */
        public uint Unknown4;
        
        public ChapterRecord[] Chapters = new ChapterRecord[10];
        public LevelRecord[] Levels = new LevelRecord[600];

        public void Serialize(Stream output)
        {
            throw new NotImplementedException();
        }

        public void Deserialize(Stream input)
        {
            this.UnlockedCharacterFlags = input.ReadValueU32();
            this.UnlockedChapterFlags = input.ReadValueU32();
            this.TotalDeaths = input.ReadValueU32();
            this.Unknown4 = input.ReadValueU32();

            for (int i = 0; i < this.Chapters.Length; i++)
            {
                this.Chapters[i].CompletedLevels = input.ReadValueU8();
                this.Chapters[i].Unknown1 = input.ReadValueU8();
                this.Chapters[i].ObtainedBandages = input.ReadValueU8();
                this.Chapters[i].Unknown3 = input.ReadValueU8();
                this.Chapters[i].Unknown4 = input.ReadValueU16();
                this.Chapters[i].Unknown6 = input.ReadValueU8();
                this.Chapters[i].Unknown7 = input.ReadValueU8();
                this.Chapters[i].Unknown8 = input.ReadValueU32();
            }

            for (int i = 0; i < this.Levels.Length; i++)
            {
                this.Levels[i].Time = input.ReadValueF32();

                //this.LevelRecords[i].Flags = (LevelRecordFlags)input.ReadValueU32();
                uint flags = input.ReadValueU32();
                if ((flags & ~(1 | 2 | 8)) != 0)
                {
                    throw new InvalidOperationException("unknown level record flag");
                }
                this.Levels[i].Flags = (LevelRecordFlags)flags;

                this.Levels[i].Unknown8 = input.ReadValueU32();
            }
        }

        public struct ChapterRecord
        {
            public byte CompletedLevels;
            public byte Unknown1;
            public byte ObtainedBandages;
            public byte Unknown3;
            public ushort Unknown4;
            public byte Unknown6;
            public byte Unknown7;
            public uint Unknown8;
        }

        public struct LevelRecord
        {
            public float Time;
            public LevelRecordFlags Flags;
            public uint Unknown8;
        }

        [Flags]
        public enum LevelRecordFlags : uint
        {
            ObtainedBandage = 1,
            Completed = 2,
            UnlockedWarp = 8,
        }
    }
}
