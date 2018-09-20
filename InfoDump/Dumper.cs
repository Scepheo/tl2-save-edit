using System;
using System.CodeDom.Compiler;
using System.Linq;
using System.Text;
using Tl2SaveEdit.Data;

namespace InfoDump
{
    internal static class Dumper
    {
        public static void DumpProperties(IndentedTextWriter writer, object obj)
        {
            foreach (var property in obj.GetType().GetProperties())
            {
                writer.WriteLine(property.Name);
                writer.Indent++;
                var value = property.GetValue(obj);
                Dump(writer, value);
                writer.Indent--;
            }
        }

        public static void Dump(IndentedTextWriter writer, object obj)
        {
            switch (obj)
            {
                case ItemList itemList:
                    Dump(writer, itemList);
                    break;
                case byte[] byteArray:
                    Dump(writer, byteArray);
                    break;
                default:
                    writer.WriteLine(obj.ToString());
                    return;
            }
        }

        private static void Dump(IndentedTextWriter writer, ItemList itemList)
        {
            for (var i = 0; i < itemList.Items.Length; i++)
            {
                writer.WriteLine($"Item {i}:");
                writer.Indent++;
                DumpProperties(writer, itemList.Items[i]);
                writer.Indent--;
            }
        }

        private static void Dump(IndentedTextWriter writer, byte[] bytes)
        {
            writer.WriteLine($"{"position",8} {"int8",4} {"int16",6} {"int32",11} {"int64",20}");

            for (var i = 0; i < bytes.Length; i++)
            {
                var position = i;
                var int8 = bytes[i];
                var int16 = i + 1 < bytes.Length ? BitConverter.ToInt16(bytes, i) : new short?();
                var int32 = i + 3 < bytes.Length ? BitConverter.ToInt32(bytes, i) : new int?();
                var int64 = i + 7 < bytes.Length ? BitConverter.ToInt64(bytes, i) : new long?();
                writer.WriteLine($"{position,8} {int8,4} {int16,6} {int32,11} {int64,20}");

                if (i + 1 + int8 * 2 < bytes.Length)
                {
                    var text = Encoding.Unicode.GetString(bytes, i + 1, int8 * 2);

                    if (IsProbablyAString(text))
                    {
                        writer.WriteLine($"int8 string  = {text}");
                    }
                }

                if (int16.HasValue && int16.Value > 0 && i + 1 + int16.Value * 2 < bytes.Length)
                {
                    var text = Encoding.Unicode.GetString(bytes, i + 2, int16.Value * 2);

                    if (IsProbablyAString(text))
                    {
                        writer.WriteLine($"int16 string = {text}");
                    }
                }
            }
        }

        private static bool IsProbablyAString(string text)
        {
            return text.Length > 0 && text.All(c => c < 256);
        }
    }
}
