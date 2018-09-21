using System;
using System.IO;

namespace Tl2SaveEdit.Data
{
    [Flags]
    public enum ModifierFlags
    {
        Unknown1           = 0x0000_0001,
        Unknown2           = 0x0000_0002,
        Exclusive          = 0x0000_0004,
        NotMagical         = 0x0000_0008,
        Saves              = 0x0000_0010,
        DisplayPositive    = 0x0000_0020,
        Unknown3           = 0x0000_0040,
        UseOwnerLevel      = 0x0000_0080,
        HasGraph           = 0x0000_0100,
        IsBonus            = 0x0000_0200,
        IsEnchantment      = 0x0000_0400,
        HasLinkName        = 0x0000_0800,
        HasParticles       = 0x0000_1000,
        HasUnitTheme       = 0x0000_2000,
        Unknown4           = 0x0000_4000,
        Unknown5           = 0x0000_8000,
        RemoveOnDeath      = 0x0001_0000,
        HasIcon            = 0x0002_0000,
        DisplayMaxModifier = 0x0004_0000,
        IsForWeapon        = 0x0008_0000,
        IsForArmor         = 0x0010_0000,
        IsDisabled         = 0x0020_0000,
    }

    internal static class ModifierFlagsExtensions
    {
        public static ModifierFlags ReadModifierFlags(this BinaryReader reader)
        {
            return (ModifierFlags)reader.ReadInt32();
        }

        public static void WriteModifierFlags(this BinaryWriter writer, ModifierFlags modifierFlags)
        {
            writer.Write((int)modifierFlags);
        }
    }
}
