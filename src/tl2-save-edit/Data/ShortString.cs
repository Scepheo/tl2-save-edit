using System.IO;
using System.Text;

namespace Tl2SaveEdit.Data
{
    public class ShortString
    {
        public string Content { get; set; }

        public ShortString(string str)
        {
            Content = str;
        }

        public override string ToString()
        {
            return Content;
        }

        internal int GetSize()
        {
            return sizeof(short) + Encoding.Unicode.GetByteCount(Content);
        }
    }

    internal static class ShortStringExtensions
    {
        public static ShortString ReadShortString(this BinaryReader reader)
        {
            var length = reader.ReadInt16();
            var bytes = reader.ReadBytes(length * 2);
            var str = Encoding.Unicode.GetString(bytes);
            return new ShortString(str);
        }

        public static void WriteShortString(this BinaryWriter writer, ShortString shortString)
        {
            var str = shortString.ToString();
            var bytes = Encoding.Unicode.GetBytes(str);
            var length = (short)(bytes.Length / 2);
            writer.Write(length);
            writer.Write(bytes);
        }
    }
}
