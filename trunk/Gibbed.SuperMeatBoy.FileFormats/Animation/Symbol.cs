using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Gibbed.Helpers;

namespace Gibbed.SuperMeatBoy.FileFormats.Animation
{
    public class Symbol
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
            this.Id = input.ReadValueU32();
            this.Unknown1 = input.ReadValueF32();
            this.Unknown2 = input.ReadValueF32();
        }
    }
}
