using System.IO;

namespace Tl2SaveEdit.Data
{
    public class Item
    {
        public byte MagicByte { get; set; }
        public long Id { get; set; }
        public ShortString Name { get; set; }
        public ShortString Prefix { get; set; }
        public ShortString Suffix { get; set; }
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
        public ModifierList Modifiers1 { get; set; }
        public ModifierList Modifiers2 { get; set; }
        public ModifierList Modifiers3 { get; set; }
        public ShortStringList Augments { get; set; }
        public int Unknown7Count { get; set; }
        public byte[] Unknown7 { get; set; }

        internal int GetSize()
        {
            return 1 // Magic byte
                + 8 // Id
                + Name.GetSize()
                + Prefix.GetSize()
                + Suffix.GetSize()
                + Unknown1.Length
                + ModIds.GetSize()
                + Unknown2.Length
                + 4 // Enchantment count
                + 4 // Stash position
                + Unknown3.Length
                + 4 // Level
                + 4 // Stack size
                + 4 // Socket count
                + Socketables.GetSize()
                + Unknown4.Length
                + 4 // Weapon damage
                + 4 // Armor
                + 4 // Armor type
                + Unknown5.Length
                + 2 // Unknown 6 count
                + Unknown6.Length
                + Modifiers1.GetSize()
                + Modifiers2.GetSize()
                + Modifiers3.GetSize()
                + Augments.GetSize()
                + 4 // Unknown 7 count
                + Unknown7.Length;
        }
    }

    internal static class ItemExtensions
    {
        public static Item ReadItem(this BinaryReader reader)
        {
            var item = new Item();

            item.MagicByte = reader.ReadByte();
            item.Id = reader.ReadInt64();
            item.Name = reader.ReadShortString();
            item.Prefix = reader.ReadShortString();
            item.Suffix = reader.ReadShortString();
            item.Unknown1 = reader.ReadBytes(24);
            item.ModIds = reader.ReadModIdList();
            item.Unknown2 = reader.ReadBytes(29);
            item.EnchantmentCount = reader.ReadInt32();
            item.StashPosition = reader.ReadInt32();
            item.Unknown3 = reader.ReadBytes(95);
            item.Level = reader.ReadInt32();
            item.StackSize = reader.ReadInt32();
            item.SocketCount = reader.ReadInt32();
            item.Socketables = reader.ReadItemList();
            item.Unknown4 = reader.ReadBytes(4);
            item.WeaponDamage = reader.ReadInt32();
            item.Armor = reader.ReadInt32();
            item.ArmorType = reader.ReadInt32();
            item.Unknown5 = reader.ReadBytes(12);
            item.Unknown6Count = reader.ReadInt16();
            item.Unknown6 = reader.ReadBytes(item.Unknown6Count * 12);
            item.Modifiers1 = reader.ReadModifierList();
            item.Modifiers2 = reader.ReadModifierList();
            item.Modifiers3 = reader.ReadModifierList();
            item.Augments = reader.ReadShortStringList();
            item.Unknown7Count = reader.ReadInt32();
            item.Unknown7 = reader.ReadBytes(item.Unknown7Count * 12);

            return item;
        }

        public static void WriteItem(this BinaryWriter writer, Item item)
        {
            writer.Write(item.MagicByte);
            writer.Write(item.Id);
            writer.WriteShortString(item.Name);
            writer.WriteShortString(item.Prefix);
            writer.WriteShortString(item.Suffix);
            writer.Write(item.Unknown1);
            writer.WriteModIdList(item.ModIds);
            writer.Write(item.Unknown2);
            writer.Write(item.EnchantmentCount);
            writer.Write(item.StashPosition);
            writer.Write(item.Unknown3);
            writer.Write(item.Level);
            writer.Write(item.StackSize);
            writer.Write(item.SocketCount);
            writer.WriteItemList(item.Socketables);
            writer.Write(item.Unknown4);
            writer.Write(item.WeaponDamage);
            writer.Write(item.Armor);
            writer.Write(item.ArmorType);
            writer.Write(item.Unknown5);
            writer.Write(item.Unknown6Count);
            writer.Write(item.Unknown6);
            writer.WriteModifierList(item.Modifiers1);
            writer.WriteModifierList(item.Modifiers2);
            writer.WriteModifierList(item.Modifiers3);
            writer.WriteShortStringList(item.Augments);
            writer.Write(item.Unknown7Count);
            writer.Write(item.Unknown7);
        }
    }
}
