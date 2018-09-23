using System.IO;

namespace Tl2SaveEdit.Data
{
    public class ModList
    {
        public Mod[] Mods { get; set; }

        public override string ToString()
        {
            return $"{Mods.Length} mods";
        }
    }

    internal static class ModListExtensions
    {
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

        public static void WriteModList(this BinaryWriter writer, ModList mods)
        {
            writer.Write(mods.Mods.Length);

            foreach (var mod in mods.Mods)
            {
                writer.WriteMod(mod);
            }
        }
    }
}
