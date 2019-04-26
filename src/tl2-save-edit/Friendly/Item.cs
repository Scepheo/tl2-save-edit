using Tl2SaveEdit.Data;

namespace Tl2SaveEdit
{
    public class Item
    {
        private byte MagicByte { get; set; }
        private long Id { get; set; }
        public string Name { get; set; }
        public string Prefix { get; set; }
        public string Suffix { get; set; }
        private byte[] Unknown1 { get; set; }
        private ModIdList ModIds { get; set; }
        private byte[] Unknown2 { get; set; }
        private int EnchantmentCount { get; set; }
        private int StashPosition { get; set; }
        private byte[] Unknown3 { get; set; }
        private int Level { get; set; }
        private int StackSize { get; set; }
        private int SocketCount { get; set; }
        private ItemList Socketables { get; set; }
        private byte[] Unknown4 { get; set; }
        private int WeaponDamage { get; set; }
        private int Armor { get; set; }
        private int ArmorType { get; set; }
        private byte[] Unknown5 { get; set; }
        private short Unknown6Count { get; set; }
        private byte[] Unknown6 { get; set; }
        private ModifierList Modifiers1 { get; set; }
        private ModifierList Modifiers2 { get; set; }
        private ModifierList Modifiers3 { get; set; }
        private ShortStringList Augments { get; set; }
        private int Unknown7Count { get; set; }
        private byte[] Unknown7 { get; set; }

        internal static Item FromDataItem(Data.Item dataItem)
        {
            var item = new Item();

            item.MagicByte = dataItem.MagicByte;
            item.Id = dataItem.Id;
            item.Name = dataItem.Name.Content;
            item.Prefix = dataItem.Prefix.Content;
            item.Suffix = dataItem.Suffix.Content;
            item.Unknown1 = dataItem.Unknown1;
            item.ModIds = dataItem.ModIds;
            item.Unknown2 = dataItem.Unknown2;
            item.EnchantmentCount = dataItem.EnchantmentCount;
            item.StashPosition = dataItem.StashPosition;
            item.Unknown3 = dataItem.Unknown3;
            item.Level = dataItem.Level;
            item.StackSize = dataItem.StackSize;
            item.SocketCount = dataItem.SocketCount;
            item.Socketables = dataItem.Socketables;
            item.Unknown4 = dataItem.Unknown4;
            item.WeaponDamage = dataItem.WeaponDamage;
            item.Armor = dataItem.Armor;
            item.ArmorType = dataItem.ArmorType;
            item.Unknown5 = dataItem.Unknown5;
            item.Unknown6Count = dataItem.Unknown6Count;
            item.Unknown6 = dataItem.Unknown6;
            item.Modifiers1 = dataItem.Modifiers1;
            item.Modifiers2 = dataItem.Modifiers2;
            item.Modifiers3 = dataItem.Modifiers3;
            item.Augments = dataItem.Augments;
            item.Unknown7Count = dataItem.Unknown7Count;
            item.Unknown7 = dataItem.Unknown7;

            return item;
        }

        internal Data.Item ToDataItem()
        {
            var dataItem = new Data.Item();

            dataItem.MagicByte = MagicByte;
            dataItem.Id = Id;
            dataItem.Name = new ShortString(Name);
            dataItem.Prefix = new ShortString(Prefix);
            dataItem.Suffix = new ShortString(Suffix);
            dataItem.Unknown1 = Unknown1;
            dataItem.ModIds = ModIds;
            dataItem.Unknown2 = Unknown2;
            dataItem.EnchantmentCount = EnchantmentCount;
            dataItem.StashPosition = StashPosition;
            dataItem.Unknown3 = Unknown3;
            dataItem.Level = Level;
            dataItem.StackSize = StackSize;
            dataItem.SocketCount = SocketCount;
            dataItem.Socketables = Socketables;
            dataItem.Unknown4 = Unknown4;
            dataItem.WeaponDamage = WeaponDamage;
            dataItem.Armor = Armor;
            dataItem.ArmorType = ArmorType;
            dataItem.Unknown5 = Unknown5;
            dataItem.Unknown6Count = Unknown6Count;
            dataItem.Unknown6 = Unknown6;
            dataItem.Modifiers1 = Modifiers1;
            dataItem.Modifiers2 = Modifiers2;
            dataItem.Modifiers3 = Modifiers3;
            dataItem.Augments = Augments;
            dataItem.Unknown7Count = Unknown7Count;
            dataItem.Unknown7 = Unknown7;

            return dataItem;
        }
    }
}
