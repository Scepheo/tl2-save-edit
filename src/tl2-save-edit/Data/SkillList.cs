using System.IO;
using System.Linq;

namespace Tl2SaveEdit.Data
{
    public class SkillList
    {
        public Skill[] Skills { get; set; }

        public override string ToString()
        {
            return $"{Skills.Length} skills";
        }

        internal int GetSize()
        {
            return 4 + Skills.Sum(skill => skill.GetSize());
        }
    }

    internal static class SkillListExtensions
    {
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

        public static void WriteSkillList(this BinaryWriter writer, SkillList skills)
        {
            writer.Write(skills.Skills.Length);

            foreach (var Skill in skills.Skills)
            {
                writer.WriteSkill(Skill);
            }
        }
    }
}
