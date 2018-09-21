using System.IO;
using System.Linq;

namespace Tl2SaveEdit.Data
{
    public class ModifierList
    {
        public Modifier[] Modifiers { get; set; }

        internal int GetSize()
        {
            return 4 + Modifiers.Sum(modifier => modifier.GetSize());
        }
    }

    internal static class ModifierListExtensions
    {
        public static ModifierList ReadModifierList(this BinaryReader reader)
        {
            var length = reader.ReadInt32();
            var modifiers = new Modifier[length];

            for (var i = 0; i < length; i++)
            {
                modifiers[i] = reader.ReadModifier();
            }

            return new ModifierList
            {
                Modifiers = modifiers,
            };
        }

        public static void WriteModifierList(this BinaryWriter writer, ModifierList modifierList)
        {
            writer.Write(modifierList.Modifiers.Length);

            foreach (var modifier in modifierList.Modifiers)
            {
                writer.WriteModifier(modifier);
            }
        }
    }
}
