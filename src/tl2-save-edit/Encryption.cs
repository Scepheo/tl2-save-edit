using System;

namespace Tl2SaveEdit
{
    public static class Encryption
    {
        public static DecryptStream Decrypt(byte[] data)
        {
            return new DecryptStream(data);
        }

        public static byte[] Encrypt(byte[] data)
        {
            const int headerSize = 9;
            const int trailerSize = 4;

            // Create new array
            var result = new byte[data.Length + headerSize + trailerSize];

            // Version
            BitConverter.GetBytes(0x44).CopyTo(result, 0);

            // Magic byte
            result[4] = 0x01;

            // Checksum
            var checksum = GetChecksum(data);
            BitConverter.GetBytes(checksum).CopyTo(result, 5);

            // Encrypt
            var startIndex = headerSize;
            var endIndex = result.Length - trailerSize - 1;

            while (startIndex <= endIndex)
            {
                var start = XorMask(data[startIndex - headerSize]);
                var end = XorMask(data[endIndex - headerSize]);

                result[startIndex] = (byte)((start << 4) | (end >> 4));
                result[endIndex] = (byte)((start >> 4) | (end << 4));

                startIndex++;
                endIndex--;
            }

            // Write length
            BitConverter.GetBytes(result.Length).CopyTo(result, result.Length - trailerSize);

            return result;
        }

        private static byte XorMask(byte value)
        {
            return value == 0x00 || value == 0xFF ? value : (byte)(value ^ 0xFF);
        }

        private static uint GetChecksum(byte[] data)
        {
            uint checksum = 0x14d3;

            for (var i = 0; i < data.Length; i++)
            {
                checksum += checksum << 5;
                checksum += data[i];
            }

            return checksum;
        }
    }
}
