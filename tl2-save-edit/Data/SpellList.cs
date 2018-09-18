namespace Tl2SaveEdit.Data
{
    public class SpellList
    {
        public Spell[] Spells { get; } = new Spell[4];

        public override string ToString()
        {
            return string.Join<Spell>(", ", Spells);
        }
    }
}
