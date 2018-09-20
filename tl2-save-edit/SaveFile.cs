using Tl2SaveEdit.Data;

namespace Tl2SaveEdit
{
    public class SaveFile
    {
        public CharacterClass CharacterClass { get; set; }
        public bool IsMale { get; set; }
        public Difficulty Difficulty { get; set; }
        public bool Hardcore { get; set; }
        public int NewGameCycle { get; set; }
        public byte[] Unknown1 { get; set; }
        public int Unknown2Length { get; set; }
        public byte[] Unknown2 { get; set; }
        public ModList BoundMods { get; internal set; }
        public ModList RecentModHistory { get; internal set; }
        public ModList FullModHistory { get; internal set; }
        public byte[] Unknown3 { get; internal set; }
        public byte[] Block1 { get; internal set; }
        public byte[] Unknown4 { get; internal set; }
        public int Face { get; internal set; }
        public int Hairstyle { get; internal set; }
        public int HairColor { get; internal set; }
        public byte[] Unknown5 { get; internal set; }
        public int Cheater { get; internal set; }
        public byte[] Unknown6 { get; internal set; }
        public string CharacterName { get; internal set; }
        public byte[] Unknown7 { get; internal set; }
        public string PlayerNumber { get; internal set; }
        public byte[] Unknown8 { get; internal set; }
        public int Level { get; internal set; }
        public int Experience { get; internal set; }
        public int FameLevel { get; internal set; }
        public int Fame { get; internal set; }
        public float Health { get; internal set; }
        public int HealthBonus { get; internal set; }
        public byte[] Unknown9 { get; internal set; }
        public float Mana { get; internal set; }
        public int ManaBonus { get; internal set; }
        public byte[] Unknown10 { get; internal set; }
        public int UnallocatedSkillPoints { get; internal set; }
        public int UnallocatedAttributePoints { get; internal set; }
        public byte[] Unknown11 { get; internal set; }
        public SkillList Skills { get; internal set; }
        public SpellList Spells { get; internal set; }
        public byte[] Unknown12 { get; internal set; }
        public int Strength { get; internal set; }
        public int Dexterity { get; internal set; }
        public int Vitality { get; internal set; }
        public int Focus { get; internal set; }
        public int Gold { get; internal set; }
        public byte[] Unknown13 { get; internal set; }
        public byte[] Block2 { get; internal set; }
        public ModIdList ModIds { get; internal set; }
        public ItemList Items { get; internal set; }
        public byte[] Rest { get; internal set; }

        public static SaveFile Parse(byte[] data)
        {
            Encryption.Decrypt(data);
            return SaveFileReader.Read(data);
        }

        public byte[] Write()
        {
            var data = SaveFileWriter.Write(this);
            Encryption.Encrypt(data);
            return data;
        }
    }
}
