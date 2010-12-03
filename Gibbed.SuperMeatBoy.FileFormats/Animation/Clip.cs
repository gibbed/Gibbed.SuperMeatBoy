using System;
using System.Collections.Generic;
using System.IO;
using Gibbed.Helpers;

namespace Gibbed.SuperMeatBoy.FileFormats.Animation
{
    public class Clip
    {
        public float Unknown1;
        public float Unknown2;
        public bool Unknown3;

        public void Serialize(Stream output, VersionTag version)
        {
            throw new NotImplementedException();
        }

        public void Deserialize(Stream input, VersionTag version)
        {
            var flags = input.ReadValueU32();
            this.Unknown3 = (flags & 1) == 1;

            this.Unknown1 = input.ReadValueF32();
            this.Unknown2 = input.ReadValueF32();

            uint[] counts = new uint[flags >> 1];
            for (uint i = 0; i < counts.Length; i++)
            {
                counts[i] = input.ReadValueU32();
            }

            foreach (var count in counts)
            {
                for (uint i = 0; i < count; i++)
                {
                    var unk8 = input.ReadValueU32();
                    var instanceCount = input.ReadValueU32();
                    var unk10 = input.ReadValueF32();

                    if (version != VersionTag.Unknown)
                    {
                        var unk11 = input.ReadValueS32();
                        var unk12 = input.ReadValueS32();
                        var unk13 = input.ReadValueS32();

                        if (unk11 == 5 || unk12 == 5 || unk13 == 5)
                        {
                        }
                    }

                    var instances = new List<Instance>();
                    for (uint l = 0; l < instanceCount; l++)
                    {
                        var instance = new Instance();
                        instance.Deserialize(input, version);
                        instances.Add(instance);
                    }
                }
            }
        }
    }
}
