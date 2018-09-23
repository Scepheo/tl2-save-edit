using System;

namespace Tl2SaveEdit
{
    public static class Encryption
    {
        public static byte[] Decrypt(byte[] data)
        {
            // Extract actual data
            var result = new byte[data.Length - 13];
            Array.Copy(data, 9, result, 0, data.Length - 13);

            // Decrypt
            var startIndex = 0;
            var endIndex = result.Length - 1;

            while (startIndex <= endIndex)
            {
                var start = result[startIndex];
                var end = result[endIndex];

                result[startIndex] = XorMask((byte)((start >> 4) | (end << 4)));
                result[endIndex] = XorMask((byte)((start << 4) | (end >> 4)));

                startIndex++;
                endIndex--;
            }

            return result;
        }

        public static byte[] Encrypt(byte[] data)
        {
            // Create new array
            var result = new byte[data.Length + 13];
            Array.Copy(data, 0, result, 9, data.Length);

            // Version
            BitConverter.GetBytes(0x44).CopyTo(result, 0);

            // Magic byte
            result[4] = 0x01;

            // Checksum
            var checksum = GetChecksum(data);
            BitConverter.GetBytes(checksum).CopyTo(result, 5);

            // Encrypt
            var startIndex = 9;
            var endIndex = result.Length - 5;

            while (startIndex <= endIndex)
            {
                var start = XorMask(result[startIndex]);
                var end = XorMask(result[endIndex]);

                result[startIndex] = (byte)((start << 4) | (end >> 4));
                result[endIndex] = (byte)((start >> 4) | (end << 4));

                startIndex++;
                endIndex--;
            }

            // Write length
            BitConverter.GetBytes(result.Length).CopyTo(result, result.Length - sizeof(int));

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
