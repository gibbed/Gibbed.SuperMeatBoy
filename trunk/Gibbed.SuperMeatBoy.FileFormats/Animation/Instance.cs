using System;
using System.IO;
using Gibbed.Helpers;

namespace Gibbed.SuperMeatBoy.FileFormats.Animation
{
    public class Instance
    {
        public uint Id;
        public float Unknown1;
        public float Unknown2;

        public void Serialize(Stream output, VersionTag version)
        {
            throw new NotImplementedException();
        }

        public void Deserialize(Stream input, VersionTag version)
        {
            var unk14 = input.ReadValueU32();
            
            var unk15 = input.ReadValueF32();
            var unk16 = input.ReadValueF32();
            var unk17 = input.ReadValueF32();
            var unk18 = input.ReadValueF32();
            var unk19 = input.ReadValueF32();
            var unk20 = input.ReadValueF32();
            
            var unk21 = input.ReadValueF32();
            var unk22 = input.ReadValueU32();

            var unk23 = input.ReadValueU16();
            var unk24 = input.ReadValueU16();
            var unk25 = input.ReadValueU16();

            var unk26 = input.ReadValueU16();
            var unk27 = input.ReadValueU16();

            var unk28 = input.ReadValueU16();
            var unk29 = input.ReadValueU16();
            var unk30 = input.ReadValueU16();

            var unk31 = input.ReadValueU32();
        }
    }
}
