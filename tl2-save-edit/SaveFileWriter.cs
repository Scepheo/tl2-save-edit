using System.IO;

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
                ? saveFile.CharacterClass.MaleKey
                : saveFile.CharacterClass.FemaleKey;
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

            // Pointer to the end of the hero data section
            var heroPointer = GetHeroPointer();
            writer.Write(heroPointer);

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
        }

        private static int GetHeroPointer()
        {
            // TODO
            return 0;
        }
    }
}
