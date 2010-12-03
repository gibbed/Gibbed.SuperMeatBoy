using System;
using System.Collections.Generic;
using System.IO;
using Gibbed.Helpers;

namespace Gibbed.SuperMeatBoy.FileFormats
{
    public class CoordFile
    {
        public List<Entry> Entries =
            new List<Entry>();

        public void Serialize(Stream output)
        {
            foreach (var entry in this.Entries)
            {
                entry.Serialize(output);
            }
        }

        public void Deserialize(Stream input)
        {
            input.Seek(0, SeekOrigin.Begin);

            if ((input.Length) % 20 != 0)
            {
                throw new FormatException();
            }

            this.Entries.Clear();
            for (int i = 0; i < input.Length / 20; i++)
            {
                var entry = new Entry();
                entry.Deserialize(input);
                this.Entries.Add(entry);
            }
        }

        public class Entry
        {
            public float X1;
            public float Y1;
            public float X2;
            public float Y2;
            public float Rotation;

            public void Serialize(Stream output)
            {
                output.WriteValueF32(this.X1);
                output.WriteValueF32(this.Y1);
                output.WriteValueF32(this.X2);
                output.WriteValueF32(this.Y2);
                output.WriteValueF32(this.Rotation);
            }

            public void Deserialize(Stream input)
            {
                this.X1 = input.ReadValueF32();
                this.Y1 = input.ReadValueF32();
                this.X2 = input.ReadValueF32();
                this.Y2 = input.ReadValueF32();
                this.Rotation = input.ReadValueF32();
            }
        }
    }
}
