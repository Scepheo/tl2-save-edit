using System.IO;
using System.Text;
using Tl2SaveEdit.Data;

namespace Tl2SaveEdit
{
    internal static class ReadExtensions
    {
        public static string ReadShortString(this BinaryReader reader)
        {
            var length = reader.ReadInt16();
            var bytes = reader.ReadBytes(length * 2);
            return Encoding.Unicode.GetString(bytes);
        }

        public static Mod[] ReadModList(this BinaryReader reader)
        {
            var length = reader.ReadInt32();
            var mods = new Mod[length];

            for (var i = 0; i < length; i++)
            {
                mods[i] = reader.ReadMod();
            }

            return mods;
        }

        public static Mod ReadMod(this BinaryReader reader)
        {
            var bytes = reader.ReadBytes(10);

            return new Mod
            {
                Data = bytes
            };
        }
    }
}
