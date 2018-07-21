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
        public int Unknown3 { get; set; }
        public Mod[] BoundMods { get; internal set; }
        public Mod[] RecentModHistory { get; internal set; }
        public Mod[] FullModHistory { get; internal set; }

        public static SaveFile Parse(byte[] data)
        {
            var decrypted = Encryption.Decrypt(data);
            return SaveFileReader.Read(data);
        }

        public byte[] Write()
        {
            var data = SaveFileWriter.Write(this);
            return Encryption.Encrypt(data);
        }
    }
}
