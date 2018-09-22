using System.IO;

namespace Tl2SaveEdit.Data
{
    public class HeroData
    {
        public byte[] Unknown3 { get; set; }
        public byte[] Block1 { get; set; }
        public byte[] Unknown4 { get; set; }
        public int Face { get; set; }
        public int Hairstyle { get; set; }
        public int HairColor { get; set; }
        public byte[] Unknown5 { get; set; }
        public int Cheater { get; set; }
        public byte[] Unknown6 { get; set; }
        public ShortString CharacterName { get; set; }
        public byte[] Unknown7 { get; set; }
        public ShortString PlayerNumber { get; set; }
        public byte[] Unknown8 { get; set; }
        public int Level { get; set; }
        public int Experience { get; set; }
        public int FameLevel { get; set; }
        public int Fame { get; set; }
        public float Health { get; set; }
        public int HealthBonus { get; set; }
        public byte[] Unknown9 { get; set; }
        public float Mana { get; set; }
        public int ManaBonus { get; set; }
        public byte[] Unknown10 { get; set; }
        public int UnallocatedSkillPoints { get; set; }
        public int UnallocatedAttributePoints { get; set; }
        public byte[] Unknown11 { get; set; }
        public SkillList Skills { get; set; }
        public SpellList Spells { get; set; }
        public byte[] Unknown12 { get; set; }
        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Vitality { get; set; }
        public int Focus { get; set; }
        public int Gold { get; set; }
        public byte[] Unknown13 { get; set; }
        public byte[] Block2 { get; set; }
        public ModIdList ModIds { get; set; }
        public ItemList Items { get; set; }
        public PassiveList Passives1 { get; set; }
        public PassiveList Passives2 { get; set; }
        public byte[] Unknown14 { get; set; }
        public ShortStringList Unknown15 { get; set; }
        public byte[] Unknown16 { get; set; }

        internal long GetSize()
        {
            return Unknown3.Length
                + Block1.Length
                + Unknown4.Length
                + 4 // Face
                + 4 // Hairstyle
                + 4 // Hair color
                + Unknown5.Length
                + 4 // Cheater
                + Unknown6.Length
                + CharacterName.GetSize()
                + Unknown7.Length
                + PlayerNumber.GetSize()
                + Unknown8.Length
                + 4 // Level
                + 4 // Experience
                + 4 // FameLevel
                + 4 // Fame
                + 4 // Health
                + 4 // Health bonus
                + Unknown9.Length
                + 4 // Mana
                + 4 // Mana bonus
                + Unknown10.Length
                + 4 // Unallocated skill points
                + 4 // Unallocated attribute points
                + Unknown11.Length
                + Skills.GetSize()
                + Spells.GetSize()
                + Unknown12.Length
                + 4 // Strength
                + 4 // Dexterity
                + 4 // Vitality
                + 4 // Focus
                + 4 // Gold
                + Unknown13.Length
                + Block2.Length
                + ModIds.GetSize()
                + Items.GetSize()
                + Passives1.GetSize()
                + Passives2.GetSize()
                + Unknown14.Length
                + Unknown15.GetSize()
                + Unknown16.Length;
        }
    }

    internal static class HeroDataExtensions
    {
        public static HeroData ReadHeroData(this BinaryReader reader)
        {
            var heroData = new HeroData();

            // Pointer to the end of the hero data - we don't need this, but we
            // still need to read it.
            var heroPointer = reader.ReadInt32();

            // Unknown - 11 bytes
            heroData.Unknown3 = reader.ReadBytes(11);

            // 0xFF block
            heroData.Block1 = reader.ReadBlock(8);

            // Unknown - 10 bytes
            heroData.Unknown4 = reader.ReadBytes(10);

            // Face
            heroData.Face = reader.ReadInt32();

            // Hairstyle
            heroData.Hairstyle = reader.ReadInt32();

            // Hair color
            heroData.HairColor = reader.ReadInt32();

            // Unknown - 43 bytes
            heroData.Unknown5 = reader.ReadBytes(43);

            // Cheater - it seems 67 and 78 mean no cheater, and 214 means yes
            // cheater. Considering it's an entire int and multiple values are
            // possible, it's probably safest to not store this as a bool yet.
            heroData.Cheater = reader.ReadInt32();

            // Unknown - 46 bytes
            heroData.Unknown6 = reader.ReadBytes(46);

            // Character name
            heroData.CharacterName = reader.ReadShortString();

            // Unknown - 2 bytes
            heroData.Unknown7 = reader.ReadBytes(2);

            // Player number (???)
            heroData.PlayerNumber = reader.ReadShortString();

            // Unknown - 84 bytes
            heroData.Unknown8 = reader.ReadBytes(84);

            // Level
            heroData.Level = reader.ReadInt32();

            // Experience
            heroData.Experience = reader.ReadInt32();

            // Fame level
            heroData.FameLevel = reader.ReadInt32();

            // Fame
            heroData.Fame = reader.ReadInt32();

            // Health
            heroData.Health = reader.ReadSingle();

            // Health bonus
            heroData.HealthBonus = reader.ReadInt32();

            // Unknown - 4 bytes
            heroData.Unknown9 = reader.ReadBytes(4);

            // Mana
            heroData.Mana = reader.ReadSingle();

            // Mana bonus
            heroData.ManaBonus = reader.ReadInt32();

            // Unknown - 20 bytes
            heroData.Unknown10 = reader.ReadBytes(20);

            // Unallocated skill points
            heroData.UnallocatedSkillPoints = reader.ReadInt32();

            // Unallocated attribute points
            heroData.UnallocatedAttributePoints = reader.ReadInt32();

            // Unknown - 48 bytes
            heroData.Unknown11 = reader.ReadBytes(48);

            // Skills
            heroData.Skills = reader.ReadSkillList();

            // Spells
            heroData.Spells = reader.ReadSpellList();

            // Unknown - 28 bytes
            heroData.Unknown12 = reader.ReadBytes(28);

            // Attributes
            heroData.Strength = reader.ReadInt32();
            heroData.Dexterity = reader.ReadInt32();
            heroData.Vitality = reader.ReadInt32();
            heroData.Focus = reader.ReadInt32();

            // Gold
            heroData.Gold = reader.ReadInt32();

            // Unknown - 4 bytes
            heroData.Unknown13 = reader.ReadBytes(4);

            // 0xFF block - 13 bytes
            heroData.Block2 = reader.ReadBlock(13);

            // Mod ids
            heroData.ModIds = reader.ReadModIdList();

            // Items
            heroData.Items = reader.ReadItemList();

            // These appear to be passives
            heroData.Passives1 = reader.ReadPassiveList();

            // More passives?
            heroData.Passives2 = reader.ReadPassiveList();

            // Always zero?
            heroData.Unknown14 = reader.ReadBytes(4);

            // List of strings
            heroData.Unknown15 = reader.ReadShortStringList();

            // No idea what this is, but it appears to always be the same:
            //   2   0   0   0
            // 204  24   3 142
            // 135  95   1 145
            //   0   0   0   0
            // 240 206 164 165
            //  75 144  61 111
            heroData.Unknown16 = reader.ReadBytes(24);

            return heroData;
        }

        public static void WriteHeroData(this BinaryWriter writer, HeroData saveFile)
        {
            // Pointer to the end of the hero data section
            writer.Write(saveFile.GetSize());

            // Unknown - 11 bytes
            writer.Write(saveFile.Unknown3);

            // 0xFF block
            writer.Write(saveFile.Block1);

            // Unknown - 10 bytes
            writer.Write(saveFile.Unknown4);

            // Face
            writer.Write(saveFile.Face);

            // Hairstyle
            writer.Write(saveFile.Hairstyle);

            // Hair color
            writer.Write(saveFile.HairColor);

            // Unknown - 43 bytes
            writer.Write(saveFile.Unknown5);

            // Cheater - it seems 67 and 78 mean no cheater, and 214 means yes
            // cheater. Considering it's an entire int and multiple values are
            // possible, it's probably safest to not store this as a bool yet.
            writer.Write(saveFile.Cheater);

            // Unknown - 46 bytes
            writer.Write(saveFile.Unknown6);

            // Character name
            writer.WriteShortString(saveFile.CharacterName);

            // Unknown - 2 bytes
            writer.Write(saveFile.Unknown7);

            // Player number (???)
            writer.WriteShortString(saveFile.PlayerNumber);

            // Unknown - 84 bytes
            writer.Write(saveFile.Unknown8);

            // Level
            writer.Write(saveFile.Level);

            // Experience
            writer.Write(saveFile.Experience);

            // Fame level
            writer.Write(saveFile.FameLevel);

            // Fame
            writer.Write(saveFile.Fame);

            // Health
            writer.Write(saveFile.Health);

            // Health bonus
            writer.Write(saveFile.HealthBonus);

            // Unknown - 4 bytes
            writer.Write(saveFile.Unknown9);

            // Mana
            writer.Write(saveFile.Mana);

            // Mana bonus
            writer.Write(saveFile.ManaBonus);

            // Unknown - 20 bytes
            writer.Write(saveFile.Unknown10);

            // Unallocated skill points
            writer.Write(saveFile.UnallocatedSkillPoints);

            // Unallocated attribute points
            writer.Write(saveFile.UnallocatedAttributePoints);

            // Unknown - 48 bytes
            writer.Write(saveFile.Unknown11);

            // Skills
            writer.WriteSkillList(saveFile.Skills);

            // Spells
            writer.WriteSpellList(saveFile.Spells);

            // Unknown - 28 bytes
            writer.Write(saveFile.Unknown12);

            // Attributes
            writer.Write(saveFile.Strength);
            writer.Write(saveFile.Dexterity);
            writer.Write(saveFile.Vitality);
            writer.Write(saveFile.Focus);

            // Gold
            writer.Write(saveFile.Gold);

            // Unknown - 4 bytes
            writer.Write(saveFile.Unknown13);

            // 0xFF block - 13 bytes
            writer.Write(saveFile.Block2);

            // Mod ids
            writer.WriteModIdList(saveFile.ModIds);

            // Items
            writer.WriteItemList(saveFile.Items);

            // These appear to be passives
            writer.WritePassiveList(saveFile.Passives1);

            // More passives?
            writer.WritePassiveList(saveFile.Passives2);

            // Always zero?
            writer.Write(saveFile.Unknown14);

            // List of strings
            writer.WriteShortStringList(saveFile.Unknown15);

            // No idea what this is, but it appears to always be the same:
            //   2   0   0   0
            // 204  24   3 142
            // 135  95   1 145
            //   0   0   0   0
            // 240 206 164 165
            //  75 144  61 111
            writer.Write(saveFile.Unknown16);
        }
    }
}
