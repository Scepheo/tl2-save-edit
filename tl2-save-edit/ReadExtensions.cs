using System.IO;
using System.Text;
using Tl2SaveEdit.Data;

namespace Tl2SaveEdit
{
    internal static class ReadExtensions
    {
        public static string ReadShortString(this BinaryReader reader)
        {
            var length = reader.ReadInt16();
            var bytes = reader.ReadBytes(length * 2);
            return Encoding.Unicode.GetString(bytes);
        }

        public static string ReadByteString(this BinaryReader reader)
        {
            var length = reader.ReadByte();
            var bytes = reader.ReadBytes(length * 2);
            return Encoding.Unicode.GetString(bytes);
        }

        public static string[] ReadShortStringArray(this BinaryReader reader)
        {
            var count = reader.ReadInt32();
            var strings = new string[count];

            for (var i = 0; i < count; i++)
            {
                strings[i] = reader.ReadShortString();
            }

            return strings;
        }

        public static byte[][] ReadUnknownArray(this BinaryReader reader, int size)
        {
            var count = reader.ReadInt32();
            var bytes = new byte[count][];

            for (var i = 0; i < count; i++)
            {
                bytes[i] = reader.ReadBytes(size);
            }

            return bytes;
        }

        public static ModList ReadModList(this BinaryReader reader)
        {
            var length = reader.ReadInt32();
            var mods = new Mod[length];

            for (var i = 0; i < length; i++)
            {
                mods[i] = reader.ReadMod();
            }

            return new ModList { Mods = mods };
        }

        public static Mod ReadMod(this BinaryReader reader)
        {
            var bytes = reader.ReadBytes(10);

            return new Mod
            {
                Data = bytes
            };
        }

        public static SkillList ReadSkillList(this BinaryReader reader)
        {
            var length = reader.ReadInt32();
            var skills = new Skill[length];

            for (var i = 0; i < length; i++)
            {
                skills[i] = reader.ReadSkill();
            }

            return new SkillList { Skills = skills };
        }

        public static Skill ReadSkill(this BinaryReader reader)
        {
            var id = reader.ReadInt64();
            var level = reader.ReadInt32();

            return new Skill
            {
                Id = id,
                Level = level,
            };
        }

        public static byte[] ReadBlock(this BinaryReader reader, int length)
        {
            var startPosition = reader.BaseStream.Position;
            var bytes = reader.ReadBytes(length);

            for (var i = 0; i < bytes.Length; i++)
            {
                var @byte = bytes[i];

                if (@byte != 0xFF)
                {
                    var position = startPosition + i;
                    var message = $"Expected 0xFF at {position} but found 0x{@byte:X2} instead";
                    throw new ParseException(message);
                }
            }

            return bytes;
        }

        public static SpellList ReadSpellList(this BinaryReader reader)
        {
            var spellList = new SpellList();

            for (var i = 0; i < spellList.Spells.Length; i++)
            {
                spellList.Spells[i] = reader.ReadSpell();
            }

            return spellList;
        }

        public static Spell ReadSpell(this BinaryReader reader)
        {
            var name = reader.ReadShortString();
            var level = reader.ReadInt32();

            return new Spell
            {
                Name = name,
                Level = level,
            };
        }

        public static ModIdList ReadModIdList(this BinaryReader reader)
        {
            var length = reader.ReadInt32();
            var modIds = new long[length];

            for (var i = 0; i < length; i++)
            {
                modIds[i] = reader.ReadInt64();
            }

            return new ModIdList
            {
                Ids = modIds,
            };
        }

        public static ItemList ReadItemList(this BinaryReader reader)
        {
            var length = reader.ReadInt32();
            var items = new Item[length];

            for (var i = 0; i < length; i++)
            {
                items[i] = reader.ReadItem();
            }

            return new ItemList
            {
                Items = items,
            };
        }

        public static Item ReadItem(this BinaryReader reader)
        {
            var item = new Item();

            item.Unknown1 = reader.ReadBytes(9);
            item.Name = reader.ReadShortString();
            item.Prefix = reader.ReadShortString();
            item.Suffix = reader.ReadShortString();
            item.Unknown2 = reader.ReadBytes(24);
            item.ModIds = reader.ReadModIdList();
            item.Unknown3 = reader.ReadBytes(1);
            item.Block1 = reader.ReadBlock(24);
            item.Unknown4 = reader.ReadBytes(4);
            item.EnchantmentCount = reader.ReadInt32();
            item.StashPosition = reader.ReadInt32();
            item.Unknown5 = reader.ReadBytes(95);
            item.Level = reader.ReadInt32();
            item.Unknown6 = reader.ReadBytes(4);
            item.SocketCount = reader.ReadInt32();
            item.Socketables = reader.ReadItemList();
            item.Unknown7 = reader.ReadBytes(4);
            item.WeaponDamage = reader.ReadInt32();
            item.Armor = reader.ReadInt32();
            item.ArmorType = reader.ReadInt32();
            item.Unknown8 = reader.ReadBytes(12);
            item.Unknown9Count = reader.ReadInt16();
            item.Unknown9 = reader.ReadBytes(item.Unknown9Count * 12);
            item.Modifiers1 = reader.ReadModifierArray();
            item.Modifiers2 = reader.ReadModifierArray();
            item.Modifiers3 = reader.ReadModifierArray();
            item.Modifiers4 = reader.ReadShortStringArray();
            item.Unknown10 = reader.ReadUnknownArray(12);

            return item;
        }

        public static Modifier[] ReadModifierArray(this BinaryReader reader)
        {
            var count = reader.ReadInt32();
            var modifiers = new Modifier[count];

            for (var i = 0; i < count; i++)
            {
                modifiers[i] = reader.ReadModifier();
            }

            return modifiers;
        }

        public static Modifier ReadModifier(this BinaryReader reader)
        {
            var modifier = new Modifier();

            modifier.Type = (ModifierType)reader.ReadInt32();
            modifier.Name = reader.ReadShortString();

            if (modifier.Type.HasFlag(ModifierType.ExtraName))
            {
                modifier.ExtraName = reader.ReadShortString();
            }

            if (modifier.Type.HasFlag(ModifierType.ExtraExtraName))
            {
                modifier.ExtraExtraName = reader.ReadShortString();
            }

            if (modifier.Type.HasFlag(ModifierType.Id))
            {
                modifier.Id = reader.ReadInt64();
            }

            var parameterCount = reader.ReadByte();
            modifier.Parameters = new float[parameterCount];

            for (var i = 0; i < parameterCount; i++)
            {
                modifier.Parameters[i] = reader.ReadSingle();
            }

            modifier.Unknown1 = reader.ReadBytes(2);
            modifier.StatParameter1 = reader.ReadInt32();
            modifier.StatParameter2 = reader.ReadInt32();
            modifier.Unknown2 = reader.ReadBytes(8);
            modifier.StatParameter3 = reader.ReadSingle();
            modifier.Unknown3 = reader.ReadBytes(4);
            modifier.ValueParameter = reader.ReadSingle();
            modifier.Unknown4 = reader.ReadBytes(4);

            if (modifier.Type.HasFlag(ModifierType.EndName))
            {
                modifier.EndName = reader.ReadByteString();
            }

            return modifier;
        }
    }
}
