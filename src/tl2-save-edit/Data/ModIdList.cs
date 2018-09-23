using System.IO;

namespace Tl2SaveEdit.Data
{
    public class ModIdList
    {
        public long[] Ids { get; set; }

        public override string ToString()
        {
            return $"{Ids.Length} mod ids";
        }

        internal int GetSize()
        {
            return 4 + Ids.Length * 8;
        }
    }

    internal static class ModIdListExtensions
    {
        public static ModIdList ReadModIdList(this BinaryReader reader)
        {
            var length = reader.ReadInt32();
            var modIds = new long[length];

            for (var i = 0; i < length; i++)
            {
                modIds[i] = reader.ReadInt64();
            }

            return new ModIdList
            {
                Ids = modIds,
            };
        }

        public static void WriteModIdList(this BinaryWriter writer, ModIdList modIds)
        {
            writer.Write(modIds.Ids.Length);

            foreach (var id in modIds.Ids)
            {
                writer.Write(id);
            }
        }
    }
}
