using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Gibbed.Helpers;

namespace Gibbed.SuperMeatBoy.FileFormats
{
    public class AnimationFile
    {
        public Animation.VersionTag Version;
        public List<Animation.Clip> Clips = new List<Animation.Clip>();
        public List<Animation.Symbol> Symbols = new List<Animation.Symbol>();
        public List<string> Sounds = new List<string>();

        public void Serialize(Stream output)
        {
            throw new NotImplementedException();
        }

        public void Deserialize(Stream input)
        {
            uint magic = input.ReadValueU32();
            if (magic == 0x46313030 /* F100 */ ||
                magic == 0x46313031 /* F101 */)
            {
                this.Version = (Animation.VersionTag)magic;
            }
            else
            {
                this.Version = Animation.VersionTag.Unknown;
                input.Seek(-4, SeekOrigin.Current);
            }

            ushort stringTableLength = input.ReadValueU16();
            ushort clipCount = input.ReadValueU16();

            uint unk3 = input.ReadValueU32();
            uint textureCount = this.Version == Animation.VersionTag.F101 ?
                input.ReadValueU32() : 0;

            byte[] stringTable = new byte[stringTableLength];
            input.Read(stringTable, 0, stringTable.Length);

            this.Clips.Clear();
            for (uint i = 0; i < clipCount; i++)
            {
                var clip = new Animation.Clip();
                clip.Deserialize(input, this.Version);
                this.Clips.Add(clip);
            }

            uint symbolCount = input.ReadValueU32();
            this.Symbols.Clear();
            for (uint i = 0; i < symbolCount; i++)
            {
                var symbol = new Animation.Symbol();
                symbol.Deserialize(input, this.Version);
                this.Symbols.Add(symbol);
            }

            this.Sounds.Clear();
            if (this.Version != Animation.VersionTag.Unknown)
            {
                uint soundCount = input.ReadValueU32();
                for (uint i = 0; i < soundCount; i++)
                {
                    this.Sounds.Add(input.ReadStringZ(Encoding.ASCII));
                }
            }
        }
    }
}
