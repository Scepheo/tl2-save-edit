namespace Tl2SaveEdit.Data
{
    public class Item
    {
        public byte[] Unknown1 { get; internal set; }
        public string Name { get; internal set; }
        public string Prefix { get; internal set; }
        public string Suffix { get; internal set; }
        public byte[] Unknown2 { get; internal set; }
        public ModIdList ModIds { get; internal set; }
        public byte[] Unknown3 { get; internal set; }
        public int EnchantmentCount { get; internal set; }
        public int StashPosition { get; internal set; }
        public byte[] Unknown4 { get; internal set; }
        public int Level { get; internal set; }
        public byte[] Unknown5 { get; internal set; }
        public int SocketCount { get; internal set; }
        public ItemList Socketables { get; internal set; }
        public byte[] Unknown6 { get; internal set; }
        public int WeaponDamage { get; internal set; }
        public int Armor { get; internal set; }
        public int ArmorType { get; internal set; }
        public byte[] Unknown7 { get; internal set; }
        public short Unknown8Count { get; internal set; }
        public byte[] Unknown8 { get; internal set; }
        public Modifier[] Modifiers1 { get; internal set; }
        public Modifier[] Modifiers2 { get; internal set; }
        public Modifier[] Modifiers3 { get; internal set; }
        public string[] Modifiers4 { get; internal set; }
        public byte[][] Unknown9 { get; internal set; }
    }
}
