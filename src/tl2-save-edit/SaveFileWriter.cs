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
            // Class string
            writer.WriteShortString(saveFile.ClassString);

            // Difficulty
            writer.Write((int)saveFile.Difficulty);

            // Hardcore
            var hardCore = saveFile.Hardcore ? (byte)0x01 : (byte)0x00;
            writer.Write(hardCore);

            // New Game+ cycle
            writer.Write(saveFile.NewGameCycle);

            // Unknown - a byte
            writer.Write(saveFile.Unknown1);

            // Unknown - an int
            writer.Write(saveFile.Unknown2);

            // Unknown - A counter, followed by 12 times that amount of bytes
            writer.Write(saveFile.Unknown3Length);
            writer.Write(saveFile.Unknown3);

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
