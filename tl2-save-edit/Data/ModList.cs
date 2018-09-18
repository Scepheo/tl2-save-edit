namespace Tl2SaveEdit.Data
{
    public class ModList
    {
        public Mod[] Mods { get; set; }

        public override string ToString()
        {
            return $"{Mods.Length} mods";
        }
    }
}
