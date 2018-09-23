using System.IO;

namespace Tl2SaveEdit
{
    internal static class ReadExtensions
    {
        public static byte[] ReadBlock(this BinaryReader reader, int length)
        {
            var startPosition = reader.BaseStream.Position;
            var bytes = reader.ReadBytes(length);

            for (var i = 0; i < bytes.Length; i++)
            {
                var @byte = bytes[i];

                if (@byte != 0xFF)
                {
                    var position = startPosition + i;
                    var message = $"Expected 0xFF at {position} but found 0x{@byte:X2} instead";
                    throw new ParseException(message);
                }
            }

            return bytes;
        }
    }
}
