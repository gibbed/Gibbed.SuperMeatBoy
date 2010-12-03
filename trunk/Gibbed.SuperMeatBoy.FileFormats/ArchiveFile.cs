using System;
using System.Collections.Generic;
using System.IO;
using Gibbed.Helpers;

namespace Gibbed.SuperMeatBoy.FileFormats
{
    public class ArchiveFile
    {
        public List<Entry> Entries = new List<Entry>();

        public void Serialize(Stream output)
        {
            throw new NotImplementedException();
        }

        public void Deserialize(Stream input)
        {
            var directoryCount = input.ReadValueS32();
            if (input.Position + (directoryCount * 8) > input.Length)
            {
                throw new EndOfStreamException();
            }

            // don't need to load this information for our uses :)
            /*
            for (int i = 0; i < directoryCount; i++)
            {
                var directoryId = input.ReadValueU32();
                var firstFileIndex = input.ReadValueU32();
            }
            */
            input.Seek(directoryCount * 8, SeekOrigin.Current);

            var fileCount = input.ReadValueS32();
            if (input.Position + (fileCount * 12) > input.Length)
            {
                throw new EndOfStreamException();
            }

            byte[] fileData = new byte[fileCount * 12];
            input.Read(fileData, 0, fileData.Length);

            var directoryNameSize = input.ReadValueU32();
            var fileNameSize = input.ReadValueU32();

            input.Seek(directoryNameSize, SeekOrigin.Current); // don't need to load directory names
            var fileNames = input.ReadToMemoryStream(fileNameSize);

            this.Entries.Clear();
            for (int i = 0; i < fileCount; i++)
            {
                if (fileNames.Position >= fileNames.Length)
                {
                    throw new EndOfStreamException();
                }

                this.Entries.Add(new Entry()
                    {
                        Path = fileNames.ReadStringZ().Replace('/', '\\'),
                        Offset = BitConverter.ToUInt32(fileData, (i * 12) + 0),
                        Size = BitConverter.ToUInt32(fileData, (i * 12) + 4),
                    });
            }
        }

        public class Entry
        {
            public string Path;
            public uint Offset;
            public uint Size;
        }
    }
}
