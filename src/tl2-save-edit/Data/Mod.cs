using System.IO;

namespace Tl2SaveEdit.Data
{
    public class Mod
    {
        // Unknown what this data is
        public byte[] Data { get; set; }
    }

    internal static class ModExtensions
    {
        public static Mod ReadMod(this BinaryReader reader)
        {
            var bytes = reader.ReadBytes(10);

            return new Mod
            {
                Data = bytes
            };
        }

        public static void WriteMod(this BinaryWriter writer, Mod mod)
        {
            writer.Write(mod.Data);
        }
    }
}
