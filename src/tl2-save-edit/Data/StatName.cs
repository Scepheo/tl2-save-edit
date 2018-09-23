using System.IO;

namespace Tl2SaveEdit.Data
{
    public class StatName
    {
        public long Id { get; set; }
        public float Percentage { get; set; }

        internal int GetSize()
        {
            return 8 // Id
                + 4; // Percentage
        }
    }

    internal static class StatNameExtensions
    {
        public static StatName ReadStatName(this BinaryReader reader)
        {
            var id = reader.ReadInt64();
            var percentage = reader.ReadSingle();

            return new StatName
            {
                Id = id,
                Percentage = percentage,
            };
        }

        public static void WriteStatName(this BinaryWriter writer, StatName statName)
        {
            writer.Write(statName.Id);
            writer.Write(statName.Percentage);
        }
    }
}