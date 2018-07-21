using System.IO;
using System.Text;
using Tl2SaveEdit.Data;

namespace Tl2SaveEdit
{
    internal static class WriteExtensions
    {
        public static void WriteShortString(this BinaryWriter writer, string text)
        {
            writer.Write((short)text.Length);
            var bytes = Encoding.Unicode.GetBytes(text);
            writer.Write(bytes);
        }

        public static void WriteModList(this BinaryWriter writer, Mod[] mods)
        {
            writer.Write(mods.Length);

            foreach (var mod in mods)
            {
                writer.WriteMod(mod);
            }
        }

        public static void WriteMod(this BinaryWriter writer, Mod mod)
        {
            writer.Write(mod.Data);
        }
    }
}
