using System.IO;
using Tl2SaveEdit.Data;

namespace Tl2SaveEdit
{
    internal static class SaveFileReader
    {
        public static SaveFile Read(Stream stream)
        {
            using (var reader = new BinaryReader(stream))
            {
                return Read(reader);
            }
        }

        public static SaveFile Read(BinaryReader reader)
        {
            var saveFile = new SaveFile();

            // Version, magic byte and checksum have already been stripped

            // Get the class string
            saveFile.ClassString = reader.ReadShortString();

            // Difficulty
            saveFile.Difficulty = (Difficulty)reader.ReadInt32();

            // Hardcore mode
            saveFile.Hardcore = reader.ReadByte() == 1;

            // New Game+ cycle
            saveFile.NewGameCycle = reader.ReadInt32();

            // Unknown - Five bytes
            saveFile.Unknown1 = reader.ReadBytes(5);

            // Unknown - A counter, followed by 12 times that amount of bytes
            saveFile.Unknown2Length = reader.ReadInt32();
            saveFile.Unknown2 = reader.ReadBytes(saveFile.Unknown2Length * 12);

            // Mod lists
            saveFile.BoundMods = reader.ReadModList();
            saveFile.RecentModHistory = reader.ReadModList();
            saveFile.FullModHistory = reader.ReadModList();

            // Hero data
            saveFile.HeroData = reader.ReadHeroData();

            // Read the rest
            var remaining = reader.BaseStream.Length - reader.BaseStream.Position;
            saveFile.Rest = reader.ReadBytes(checked((int)remaining));

            return saveFile;
        }
    }
}
