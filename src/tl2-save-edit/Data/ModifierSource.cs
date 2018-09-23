using System.IO;

namespace Tl2SaveEdit.Data
{
    public enum ModifierSource
    {
        OnCastCaster = 0,
        OnCastReceiver = 1,
        OnUpdateCaster = 2,
        OnUpdateSelf = 3,
    }

    internal static class ModifierSourceExtensions
    {
        public static ModifierSource ReadModifierSource(this BinaryReader reader)
        {
            return (ModifierSource)reader.ReadInt32();
        }

        public static void WriteModifierSource(this BinaryWriter writer, ModifierSource modifierSource)
        {
            writer.Write((int)modifierSource);
        }
    }
}
