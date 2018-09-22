using System.IO;
using System.Text;

namespace Tl2SaveEdit.Data
{
    public class ByteString
    {
        private string _str;

        public ByteString(string str)
        {
            _str = str;
        }

        public override string ToString()
        {
            return _str;
        }

        internal int GetSize()
        {
            return sizeof(byte) + Encoding.Unicode.GetByteCount(_str);
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
