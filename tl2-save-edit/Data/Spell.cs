namespace Tl2SaveEdit.Data
{
    public class Spell
    {
        public string Name { get; set; }
        public int Level { get; set; }

        public override string ToString()
        {
            return $"{Name} {Level}";
        }
    }
}
