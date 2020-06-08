using System.IO;

namespace Tl2SaveEdit.Data
{
    public class Mod
    {
        /// <summary>
        /// Possibly a FileTime value
        /// </summary>
        public long Unknown1 { get; }

        /// <summary>
        /// Unknown value
        /// </summary>
        public short Unknown2 { get; }

        public Mod(long unknown1, short unknown2)
        {
            Unknown1 = unknown1;
            Unknown2 = unknown2;
        }
    }

    internal static class ModExtensions
    {
        public static Mod ReadMod(this BinaryReader reader)
        {
            var unknown1 = reader.ReadInt64();
            var unknown2 = reader.ReadInt16();
            return new Mod(unknown1, unknown2);
        }

        public static void WriteMod(this BinaryWriter writer, Mod mod)
        {
            writer.Write(mod.Unknown1);
            writer.Write(mod.Unknown2);
        }
    }
}
