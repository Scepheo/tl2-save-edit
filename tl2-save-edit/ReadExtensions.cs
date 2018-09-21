using System;
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

            item.MagicByte = reader.ReadByte();
            item.Id = reader.ReadInt64();
            item.Name = reader.ReadShortString();
            item.Prefix = reader.ReadShortString();
            item.Suffix = reader.ReadShortString();
            item.Unknown1 = reader.ReadBytes(24);
            item.ModIds = reader.ReadModIdList();
            item.Unknown2 = reader.ReadBytes(29);
            item.EnchantmentCount = reader.ReadInt32();
            item.StashPosition = reader.ReadInt32();
            item.Unknown3 = reader.ReadBytes(95);
            item.Level = reader.ReadInt32();
            item.StackSize = reader.ReadInt32();
            item.SocketCount = reader.ReadInt32();
            item.Socketables = reader.ReadItemList();
            item.Unknown4 = reader.ReadBytes(4);
            item.WeaponDamage = reader.ReadInt32();
            item.Armor = reader.ReadInt32();
            item.ArmorType = reader.ReadInt32();
            item.Unknown5 = reader.ReadBytes(12);
            item.Unknown6Count = reader.ReadInt16();
            item.Unknown6 = reader.ReadBytes(item.Unknown6Count * 12);
            item.Modifiers1 = reader.ReadModifierArray();
            item.Modifiers2 = reader.ReadModifierArray();
            item.Modifiers3 = reader.ReadModifierArray();
            item.Augments = reader.ReadShortStringArray();
            item.Unknown7Count = reader.ReadInt32();
            item.Unknown7 = reader.ReadBytes(item.Unknown7Count * 12);

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

            modifier.Flags = (ModifierFlags)reader.ReadInt32();
            modifier.Name = reader.ReadShortString();

            if (modifier.Flags.HasFlag(ModifierFlags.HasGraph))
            {
                modifier.Graph = reader.ReadShortString();
            }

            if (modifier.Flags.HasFlag(ModifierFlags.HasParticles))
            {
                modifier.Particles = reader.ReadShortString();
            }

            if (modifier.Flags.HasFlag(ModifierFlags.HasUnitTheme))
            {
                modifier.UnitThemeId = reader.ReadInt64();
            }

            var propertyCount = reader.ReadByte();
            modifier.Properties = new float[propertyCount];

            for (var i = 0; i < propertyCount; i++)
            {
                modifier.Properties[i] = reader.ReadSingle();
            }

            var statNameCount = reader.ReadInt16();
            modifier.StatNames = new StatName[statNameCount];

            for (var i = 0; i < statNameCount; i++)
            {
                modifier.StatNames[i] = reader.ReadStatName();
            }

            modifier.EffectType = reader.ReadInt32();
            modifier.DamageType = (ModifierDamageType)reader.ReadInt32();
            modifier.Activation = (ModifierActivation)reader.ReadInt32();
            modifier.Level = reader.ReadInt32();
            modifier.Duration = reader.ReadSingle();
            modifier.Unknown1 = reader.ReadBytes(4);
            modifier.DisplayValue = reader.ReadSingle();
            modifier.Source = (ModifierSource)reader.ReadInt32();

            if (modifier.Flags.HasFlag(ModifierFlags.HasIcon))
            {
                modifier.Icon = reader.ReadByteString();
            }

            return modifier;
        }

        public static StatName ReadStatName(this BinaryReader reader)
        {
            var id = reader.ReadInt64();
            var percentage = reader.ReadSingle();

            return new StatName
            {
                Id = id,
                Percentage = percentage,
            };
        }

        public static Passive[] ReadPassives(this BinaryReader reader)
        {
            var count = reader.ReadInt32();
            var passives = new Passive[count];

            for (var i = 0; i < count; i++)
            {
                passives[i] = reader.ReadPassive();
            }

            return passives;
        }

        public static Passive ReadPassive(this BinaryReader reader)
        {
            var passive = new Passive();

            // Embermage
            ////////////

            // Berserker
            ////////////
            // MELEE_DAMAGE_BONUS   = 80 = 0101_0000
            // Size = 51

            // Outlander
            ////////////
            // WANDERER_PASSIVE_??  = 85 = 0101_0101
            // Size = 63
            // WANDERER_CHARGE_RATE = 81 = 0101_0001
            // Size = 51

            // Engineer
            ///////////
            //                      = 81 = 0101_0001
            // Size = 43
            // MELEE_DAMAGE_BONUS   = 80 = 0101_0000
            // Size = 51
            passive.Flags = reader.ReadInt32();
            passive.Name = reader.ReadShortString();

            int size;

            if (passive.Name == "MELEE_DAMAGE_BONUS")
            {
                size = 51;
            }
            else if (passive.Name == "")
            {
                size = 43;
            }
            else if (passive.Name == "WANDERER_CHARGE_RATE")
            {
                size = 51;
            }
            else if (passive.Name.StartsWith("WANDERER_PASSIVE_"))
            {
                size = 63;
            }
            else
            {
                throw new InvalidOperationException($"Unknown passive {passive.Name}");
            }

            passive.Unknown1 = reader.ReadBytes(size);

            return passive;
        }
    }
}
