﻿using System.IO;
using System.Text;

namespace Tl2SaveEdit.Data
{
    public class ShortString
    {
        private string _str;

        public ShortString(string str)
        {
            _str = str;
        }

        public override string ToString()
        {
            return _str;
        }

        internal int GetSize()
        {
            return sizeof(short) + Encoding.Unicode.GetByteCount(_str);
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
            var length = (short)bytes.Length / 2;
            writer.Write(length);
            writer.Write(bytes);
        }
    }
}