using System;
using System.Diagnostics;
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

            // Mod lists
            saveFile.BoundMods = reader.ReadModList();
            saveFile.RecentModHistory = reader.ReadModList();
            saveFile.FullModHistory = reader.ReadModList();

            // Pointer to the end of the hero data section - we can ignore this
            // when reading
            Debug.WriteLine($"Hero pointer position: {reader.BaseStream.Position}");
            var heroPointer = reader.ReadInt32();
            Debug.WriteLine($"Hero pointer: {heroPointer}");

            // Unknown - 11 bytes
            saveFile.Unknown3 = reader.ReadBytes(11);

            // 0xFF block
            saveFile.Block1 = reader.ReadBlock(8);

            // Unknown - 10 bytes
            saveFile.Unknown4 = reader.ReadBytes(10);

            // Face
            saveFile.Face = reader.ReadInt32();

            // Hairstyle
            saveFile.Hairstyle = reader.ReadInt32();

            // Hair color
            saveFile.HairColor = reader.ReadInt32();

            // Unknown - 43 bytes
            saveFile.Unknown5 = reader.ReadBytes(43);

            // Cheater - it seems 67 and 78 mean no cheater, and 214 means yes
            // cheater. Considering it's an entire int and multiple values are
            // possible, it's probably safest to not store this as a bool yet.
            saveFile.Cheater = reader.ReadInt32();

            // Unknown - 46 bytes
            saveFile.Unknown6 = reader.ReadBytes(46);

            // Character name
            saveFile.CharacterName = reader.ReadShortString();

            // Unknown - 2 bytes
            saveFile.Unknown7 = reader.ReadBytes(2);

            // Player number (???)
            saveFile.PlayerNumber = reader.ReadShortString();

            // Unknown - 84 bytes
            saveFile.Unknown8 = reader.ReadBytes(84);

            // Level
            saveFile.Level = reader.ReadInt32();

            // Experience
            saveFile.Experience = reader.ReadInt32();

            // Fame level
            saveFile.FameLevel = reader.ReadInt32();

            // Fame
            saveFile.Fame = reader.ReadInt32();

            // Health
            saveFile.Health = reader.ReadSingle();

            // Health bonus
            saveFile.HealthBonus = reader.ReadInt32();

            // Unknown - 4 bytes
            saveFile.Unknown9 = reader.ReadBytes(4);

            // Mana
            saveFile.Mana = reader.ReadSingle();

            // Mana bonus
            saveFile.ManaBonus = reader.ReadInt32();

            // Unknown - 20 bytes
            saveFile.Unknown10 = reader.ReadBytes(20);

            // Unallocated skill points
            saveFile.UnallocatedSkillPoints = reader.ReadInt32();

            // Unallocated attribute points
            saveFile.UnallocatedAttributePoints = reader.ReadInt32();

            // Unknown - 48 bytes
            saveFile.Unknown11 = reader.ReadBytes(48);

            // Skills
            saveFile.Skills = reader.ReadSkillList();

            // Spells
            saveFile.Spells = reader.ReadSpellList();

            // Unknown - 28 bytes
            saveFile.Unknown12 = reader.ReadBytes(28);

            // Attributes
            saveFile.Strength = reader.ReadInt32();
            saveFile.Dexterity = reader.ReadInt32();
            saveFile.Vitality = reader.ReadInt32();
            saveFile.Focus = reader.ReadInt32();

            // Gold
            saveFile.Gold = reader.ReadInt32();

            // Unknown - 4 bytes
            saveFile.Unknown13 = reader.ReadBytes(4);

            // 0xFF block - 13 bytes
            saveFile.Block2 = reader.ReadBlock(13);

            // Mod ids
            saveFile.ModIds = reader.ReadModIdList();

            // Items
            saveFile.Items = reader.ReadItemList();

            Debug.WriteLine($"Current position: {reader.BaseStream.Position}");

            saveFile.Rest = reader.ReadBytes(2048);

            return saveFile;
        }
    }
}
