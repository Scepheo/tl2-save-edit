using System.IO;

namespace Tl2SaveEdit.Data
{
    public enum ModifierActivation
    {
        Passive = 0,
        Dynamic = 1,
        Transfer = 2,
    }

    internal static class ModifierActivationExtensions
    {
        public static ModifierActivation ReadModifierActivation(this BinaryReader reader)
        {
            return (ModifierActivation)reader.ReadInt32();
        }

        public static void WriteModifierActivation(this BinaryWriter writer, ModifierActivation modifierActivation)
        {
            writer.Write((int)modifierActivation);
        }
    }
}
