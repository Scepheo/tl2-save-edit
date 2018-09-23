using System.IO;

namespace Tl2SaveEdit.Data
{
    public enum ModifierDamageType
    {
        Physical = 0,
        Magical = 1,
        Fire = 2,
        Ice = 3,
        Electric = 4,
        Poison = 5,
        All = 6,
    }

    internal static class ModifierDamageTypeExtensions
    {
        public static ModifierDamageType ReadModifierDamageType(this BinaryReader reader)
        {
            return (ModifierDamageType)reader.ReadInt32();
        }

        public static void WriteModifierDamageType(this BinaryWriter writer, ModifierDamageType modifierDamageType)
        {
            writer.Write((int)modifierDamageType);
        }
    }
}
