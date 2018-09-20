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
        public ModList BoundMods { get; set; }
        public ModList RecentModHistory { get; set; }
        public ModList FullModHistory { get; set; }
        public byte[] Unknown3 { get; set; }
        public byte[] Block1 { get; set; }
        public byte[] Unknown4 { get; set; }
        public int Face { get; set; }
        public int Hairstyle { get; set; }
        public int HairColor { get; set; }
        public byte[] Unknown5 { get; set; }
        public int Cheater { get; set; }
        public byte[] Unknown6 { get; set; }
        public string CharacterName { get; set; }
        public byte[] Unknown7 { get; set; }
        public string PlayerNumber { get; set; }
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
        public byte[] Rest { get; set; }

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
