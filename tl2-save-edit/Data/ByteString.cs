using System.IO;
using System.Text;

namespace Tl2SaveEdit.Data
{
    public class ByteString
    {
        public string Content { get; set; }

        public ByteString(string str)
        {
            Content = str;
        }

        public override string ToString()
        {
            return Content;
        }

        internal int GetSize()
        {
            return sizeof(byte) + Encoding.Unicode.GetByteCount(Content);
        }
    }

    internal static class ByteStringExtensions
    {
        public static ByteString ReadByteString(this BinaryReader reader)
        {
            var length = reader.ReadByte();
            var bytes = reader.ReadBytes(length * 2);
            var str = Encoding.Unicode.GetString(bytes);
            return new ByteString(str);
        }

        public static void WriteByteString(this BinaryWriter writer, ByteString ByteString)
        {
            var str = ByteString.ToString();
            var bytes = Encoding.Unicode.GetBytes(str);
            var size = (byte)(bytes.Length / 2);
            writer.Write(size);
            writer.Write(bytes);
        }
    }
}
