using System.IO;
using System.Text;
using Tl2SaveEdit.Data;

namespace Tl2SaveEdit
{
    internal static class WriteExtensions
    {
        public static void WriteShortString(this BinaryWriter writer, string text)
        {
            writer.Write((short)text.Length);
            var bytes = Encoding.Unicode.GetBytes(text);
            writer.Write(bytes);
        }

        public static void WriteModList(this BinaryWriter writer, ModList mods)
        {
            writer.Write(mods.Mods.Length);

            foreach (var mod in mods.Mods)
            {
                writer.WriteMod(mod);
            }
        }

        public static void WriteMod(this BinaryWriter writer, Mod mod)
        {
            writer.Write(mod.Data);
        }

        public static void WriteSkillList(this BinaryWriter writer, SkillList skills)
        {
            writer.Write(skills.Skills.Length);

            foreach (var Skill in skills.Skills)
            {
                writer.WriteSkill(Skill);
            }
        }

        public static void WriteSkill(this BinaryWriter writer, Skill skill)
        {
            writer.Write(skill.Id);
            writer.Write(skill.Level);
        }

        public static void WriteSpellList(this BinaryWriter writer, SpellList spells)
        {
            foreach (var Spell in spells.Spells)
            {
                writer.WriteSpell(Spell);
            }
        }

        public static void WriteSpell(this BinaryWriter writer, Spell spell)
        {
            writer.WriteShortString(spell.Name);
            writer.Write(spell.Level);
        }

        public static void WriteModIdList(this BinaryWriter writer, ModIdList modIds)
        {
            writer.Write(modIds.Ids.Length);
            
            foreach (var id in modIds.Ids)
            {
                writer.Write(id);
            }
        }
    }
}
