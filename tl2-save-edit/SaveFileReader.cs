using System;
using System.IO;
using Tl2SaveEdit.Data;

namespace Tl2SaveEdit
{
    internal static class SaveFileReader
    {
        public static SaveFile Read(byte[] data)
        {
            using (var stream = new MemoryStream(data))
            {
                return Read(stream);
            }
        }

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

            // Read the version number
            var version = reader.ReadInt32();

            if (version != 0x44)
            {
                throw new InvalidOperationException($"Save file version 0x{version:2X} did not match expected version 0x44");
            }

            // Read a check byte that always seems to be 1
            var check = reader.ReadByte();

            if (check != 0x01)
            {
                throw new InvalidOperationException($"Check value 0x{check:2X} did not match expected check 0x01");
            }

            // Read checksum and ignore it
            var checksum = reader.ReadInt32();

            // Get the class string, and from that, the character's sex
            var classString = reader.ReadShortString();
            (saveFile.CharacterClass, saveFile.IsMale) = CharacterClass.FindByKey(classString);

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

            // Unknown - Always seems to be 0
            saveFile.Unknown3 = reader.ReadInt32();

            // Mod lists
            saveFile.BoundMods = reader.ReadModList();
            saveFile.RecentModHistory = reader.ReadModList();
            saveFile.FullModHistory = reader.ReadModList();

            // Pointer to the end of the hero data section - we can ignore this when reading
            var postHeroDataIndex = reader.ReadInt32();

            return saveFile;
        }
    }
}
