using System.IO;
using System.Linq;

namespace Tl2SaveEdit.Data
{
    public class ShortStringList
    {
        public ShortString[] ShortStrings { get; set; }

        internal int GetSize()
        {
            return 4 + ShortStrings.Sum(shortString => shortString.GetSize());
        }
    }

    internal static class ShortStringListExtensions
    {
        public static ShortStringList ReadShortStringList(this BinaryReader reader)
        {
            var count = reader.ReadInt32();
            var shortStrings = new ShortString[count];

            for (var i = 0; i < count; i++)
            {
                shortStrings[i] = reader.ReadShortString();
            }

            return new ShortStringList { ShortStrings = shortStrings };
        }

        public static void WriteShortStringList(this BinaryWriter writer, ShortStringList strings)
        {
            writer.Write(strings.ShortStrings.Length);

            foreach (var @string in strings.ShortStrings)
            {
                writer.WriteShortString(@string);
            }
        }
    }
}
