using System.IO;
using Gibbed.SuperMeatBoy.FileFormats;

namespace Gibbed.SuperMeatBoy.Test
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            using (var input = File.Open(
                @"savegame.dat", FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                var test = new SaveFile();
                test.Deserialize(input);
            }
        }
    }
}
