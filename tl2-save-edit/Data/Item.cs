namespace Tl2SaveEdit.Data
{
    public class Item
    {
        public byte MagicByte { get; set; }
        public long Id { get; set; }
        public string Name { get; set; }
        public string Prefix { get; set; }
        public string Suffix { get; set; }
        public byte[] Unknown1 { get; set; }
        public ModIdList ModIds { get; set; }
        public byte[] Unknown2 { get; set; }
        public int EnchantmentCount { get; set; }
        public int StashPosition { get; set; }
        public byte[] Unknown3 { get; set; }
        public int Level { get; set; }
        public int StackSize { get; set; }
        public int SocketCount { get; set; }
        public ItemList Socketables { get; set; }
        public byte[] Unknown4 { get; set; }
        public int WeaponDamage { get; set; }
        public int Armor { get; set; }
        public int ArmorType { get; set; }
        public byte[] Unknown5 { get; set; }
        public short Unknown6Count { get; set; }
        public byte[] Unknown6 { get; set; }
        public Modifier[] Modifiers1 { get; set; }
        public Modifier[] Modifiers2 { get; set; }
        public Modifier[] Modifiers3 { get; set; }
        public string[] Augments { get; set; }
        public int Unknown7Count { get; set; }
        public byte[] Unknown7 { get; set; }
    }
}
