using System.IO;

namespace Tl2SaveEdit.Data
{
    public class Spell
    {
        public ShortString Name { get; set; }
        public int Level { get; set; }

        public override string ToString()
        {
            return $"{Name} {Level}";
        }

        internal int GetSize()
        {
            return Name.GetSize() + 4;
        }
    }

    internal static class SpellExtensions
    {
        public static Spell ReadSpell(this BinaryReader reader)
        {
            var name = reader.ReadShortString();
            var level = reader.ReadInt32();

            return new Spell
            {
                Name = name,
                Level = level,
            };
        }

        public static void WriteSpell(this BinaryWriter writer, Spell spell)
        {
            writer.WriteShortString(spell.Name);
            writer.Write(spell.Level);
        }
    }
}
