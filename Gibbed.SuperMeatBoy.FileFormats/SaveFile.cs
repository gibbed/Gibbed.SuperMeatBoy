using System;
using System.IO;
using Gibbed.Helpers;

namespace Gibbed.SuperMeatBoy.FileFormats
{
    public class SaveFile
    {
        public uint UnlockedCharacterFlags;
        public uint Unknown2;
        public uint Unknown3;
        public uint Unknown4;
        public UnknownRecord[] UnknownRecords = new UnknownRecord[10];
        public LevelRecord[] LevelRecords = new LevelRecord[600];

        public void Serialize(Stream output)
        {
            throw new NotImplementedException();
        }

        public void Deserialize(Stream input)
        {
            this.UnlockedCharacterFlags = input.ReadValueU32();
            this.Unknown2 = input.ReadValueU32();
            this.Unknown3 = input.ReadValueU32();
            this.Unknown4 = input.ReadValueU32();

            for (int i = 0; i < this.UnknownRecords.Length; i++)
            {
                this.UnknownRecords[i].Unknown0 = input.ReadValueU8();
                this.UnknownRecords[i].Unknown1 = input.ReadValueU8();
                this.UnknownRecords[i].Unknown2 = input.ReadValueU8();
                this.UnknownRecords[i].Unknown3 = input.ReadValueU8();
                this.UnknownRecords[i].Unknown4 = input.ReadValueU16();
                this.UnknownRecords[i].Unknown6 = input.ReadValueU8();
                this.UnknownRecords[i].Unknown7 = input.ReadValueU32();
            }

            for (int i = 0; i < this.LevelRecords.Length; i++)
            {
                this.LevelRecords[i].Time = input.ReadValueF32();
                this.LevelRecords[i].Unknown4 = input.ReadValueU32();
                this.LevelRecords[i].Unknown8 = input.ReadValueU32();
            }
        }

        public struct UnknownRecord
        {
            public byte Unknown0;
            public byte Unknown1;
            public byte Unknown2;
            public byte Unknown3;
            public ushort Unknown4;
            public byte Unknown6;
            public uint Unknown7;
        }

        public struct LevelRecord
        {
            public float Time;
            public uint Unknown4;
            public uint Unknown8;
        }
    }
}
