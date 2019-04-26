using System.Collections.Generic;
using System.Linq;
using Tl2SaveEdit.Data;

namespace Tl2SaveEdit
{
    public class Spell
    {
        public string Name { get; set; }
        public int Level { get; set; }

        internal static Spell FromDataSpell(Data.Spell dataSpell)
        {
            var spell = new Spell();

            spell.Name = dataSpell.Name.Content;
            spell.Level = dataSpell.Level;

            return spell;
        }

        internal Data.Spell ToDataSpell()
        {
            var dataSpell = new Data.Spell();

            dataSpell.Name = new ShortString(Name);
            dataSpell.Level = Level;

            return dataSpell;
        }

        internal static List<Spell> FromSpellList(SpellList spellList)
        {
            var spells = spellList.Spells.Select(FromDataSpell).ToList();
            return spells;
        }

        internal static SpellList ToSpellList(List<Spell> spells)
        {
            var dataSpells = spells.Select(spell => spell.ToDataSpell()).ToArray();

            var spellList = new SpellList()
            {
                Spells = dataSpells,
            };

            return spellList;

        }
    }
}
