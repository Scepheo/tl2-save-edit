using System.IO;
using Tl2SaveEdit.Data;

namespace Tl2SaveEdit
{
    internal static class SaveFileWriter
    {
        public static byte[] Write(SaveFile saveFile)
        {
            byte[] result;

            using (var stream = new MemoryStream())
            {
                Write(saveFile, stream);
                result = stream.ToArray();
            }

            return result;
        }

        public static void Write(SaveFile saveFile, Stream stream)
        {
            using (var writer = new BinaryWriter(stream))
            {
                Write(saveFile, writer);
            }
        }

        public static void Write(SaveFile saveFile, BinaryWriter writer)
        {
            // Write version
            writer.Write(0x44);

            // Write check
            writer.Write((byte)0x01);

            // The checksum will be written later by the encryption
            writer.Write(0x00);

            // Get class key from class + sex and write it
            var classString = saveFile.IsMale
                ? new ShortString(saveFile.CharacterClass.MaleKey)
                : new ShortString(saveFile.CharacterClass.FemaleKey);
            writer.WriteShortString(classString);

            // Difficulty
            writer.Write((int)saveFile.Difficulty);

            // Hardcore
            var hardCore = saveFile.Hardcore ? (byte)0x01 : (byte)0x00b;
            writer.Write(hardCore);

            // New Game+ cycle
            writer.Write(saveFile.NewGameCycle);

            // Unknown - five bytes
            writer.Write(saveFile.Unknown1);

            // Unknown - A counter, followed by 12 times that amount of bytes
            writer.Write(saveFile.Unknown2Length);
            writer.Write(saveFile.Unknown2);

            // Mod lists
            writer.WriteModList(saveFile.BoundMods);
            writer.WriteModList(saveFile.RecentModHistory);
            writer.WriteModList(saveFile.FullModHistory);

            // Hero data
            writer.WriteHeroData(saveFile.HeroData);

            // Rest
            writer.Write(saveFile.Rest);
        }
    }
}
