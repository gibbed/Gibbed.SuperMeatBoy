using System;
using System.Collections.Generic;
using System.IO;
using Gibbed.Helpers;
using Gibbed.SuperMeatBoy.FileFormats;
using NDesk.Options;

namespace Gibbed.SuperMeatBoy.Unpack
{
    public class Program
    {
        public static void Main(string[] args)
        {
            bool verbose = false;
            bool showHelp = false;

            OptionSet options = new OptionSet()
            {
                {
                    "h|help",
                    "show this message and exit", 
                    v => showHelp = v != null
                },
            };

            List<string> extras;

            try
            {
                extras = options.Parse(args);
            }
            catch (OptionException e)
            {
                Console.Write("{0}: ", GetExecutableName());
                Console.WriteLine(e.Message);
                Console.WriteLine("Try `{0} --help' for more information.", GetExecutableName());
                return;
            }

            if (extras.Count < 1 || extras.Count > 2 || showHelp == true)
            {
                Console.WriteLine("Usage: {0} [OPTIONS]+ input_archive [output_directory]", GetExecutableName());
                Console.WriteLine("Unpack specified archive.");
                Console.WriteLine();
                Console.WriteLine("Options:");
                options.WriteOptionDescriptions(Console.Out);
                return;
            }

            string inputPath = extras[0];
            string outputPath = extras.Count > 1 ? extras[1] : Path.ChangeExtension(inputPath, null);

            using (var input = File.Open(
                inputPath,
                FileMode.Open,
                FileAccess.Read,
                FileShare.ReadWrite))
            {
                var archive = new ArchiveFile();
                archive.Deserialize(input);

                Directory.CreateDirectory(outputPath);

                byte[] buffer = new byte[0x4000];

                long counter = 0;
                foreach (var entry in archive.Entries)
                {
                    string entryPath = Path.Combine(outputPath, entry.Path);
                    Directory.CreateDirectory(Path.GetDirectoryName(entryPath));

                    using (Stream output = File.Open(entryPath, FileMode.Create, FileAccess.Write, FileShare.Read))
                    {
                        input.Seek(entry.Offset, SeekOrigin.Begin);
                        output.WriteFromStream(input, entry.Size);
                    }

                    counter++;
                }
            }
        }

        private static string GetExecutableName()
        {
            return Path.GetFileName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);
        }
    }
}
