using System;

namespace Tl2SaveEdit
{
    public static class Encryption
    {
        public static void Decrypt(byte[] data)
        {
            // Remove checksum
            var checksum = (uint)0;
            BitConverter.GetBytes(checksum).CopyTo(data, 5);

            // Decrypt
            var startIndex = 9;
            var endIndex = data.Length - 5;

            while (startIndex <= endIndex)
            {
                var start = data[startIndex];
                var end = data[endIndex];

                data[startIndex] = XorMask((byte)((start >> 4) | (end << 4)));
                data[endIndex] = XorMask((byte)((start << 4) | (end >> 4)));

                startIndex++;
                endIndex--;
            }

            // Write length
            BitConverter.GetBytes(data.Length).CopyTo(data, data.Length - sizeof(int));
        }

        public static void Encrypt(byte[] data)
        {
            // Write checksum
            var checksum = GetChecksum(data);
            BitConverter.GetBytes(checksum).CopyTo(data, 5);

            // Encrypt
            var startIndex = 9;
            var endIndex = data.Length - 5;

            while (startIndex <= endIndex)
            {
                var start = XorMask(data[startIndex]);
                var end = XorMask(data[endIndex]);

                data[startIndex] = (byte)((start << 4) | (end >> 4));
                data[endIndex] = (byte)((start >> 4) | (end << 4));

                startIndex++;
                endIndex--;
            }

            // Write length
            BitConverter.GetBytes(data.Length).CopyTo(data, data.Length - sizeof(int));
        }

        private static byte XorMask(byte value)
        {
            return value == 0x00 || value == 0xFF ? value : (byte)(value ^ 0xFF);
        }

        private static uint GetChecksum(byte[] data)
        {
            uint checksum = 0x14d3;

            for (var i = 9; i < data.Length; i++)
            {
                checksum += checksum << 5;
                checksum += data[i];
            }

            return checksum;
        }
    }
}
