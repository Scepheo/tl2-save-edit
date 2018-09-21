using System.IO;
using System.Linq;

namespace Tl2SaveEdit.Data
{
    public class SpellList
    {
        public Spell[] Spells { get; } = new Spell[4];

        public override string ToString()
        {
            return string.Join<Spell>(", ", Spells);
        }

        internal int GetSize()
        {
            return 4 + Spells.Sum(spell => spell.GetSize());
        }
    }

    internal static class SpellListExtensions
    {
        public static SpellList ReadSpellList(this BinaryReader reader)
        {
            var spellList = new SpellList();

            for (var i = 0; i < spellList.Spells.Length; i++)
            {
                spellList.Spells[i] = reader.ReadSpell();
            }

            return spellList;
        }

        public static void WriteSpellList(this BinaryWriter writer, SpellList spells)
        {
            foreach (var Spell in spells.Spells)
            {
                writer.WriteSpell(Spell);
            }
        }
    }
}
