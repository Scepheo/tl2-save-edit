using System.IO;
using System.Linq;

namespace Tl2SaveEdit.Data
{
    public class PassiveList
    {
        public Passive[] Passives { get; set; }

        internal int GetSize()
        {
            return 4 + Passives.Sum(Passive => Passive.GetSize());
        }
    }

    internal static class PassiveListExtensions
    {
        public static PassiveList ReadPassiveList(this BinaryReader reader)
        {
            var length = reader.ReadInt32();
            var Passives = new Passive[length];

            for (var i = 0; i < length; i++)
            {
                Passives[i] = reader.ReadPassive();
            }

            return new PassiveList
            {
                Passives = Passives,
            };
        }

        public static void WritePassiveList(this BinaryWriter writer, PassiveList PassiveList)
        {
            writer.Write(PassiveList.Passives.Length);

            foreach (var Passive in PassiveList.Passives)
            {
                writer.WritePassive(Passive);
            }
        }
    }
}
