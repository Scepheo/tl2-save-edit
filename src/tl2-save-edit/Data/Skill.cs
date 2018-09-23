using System.IO;

namespace Tl2SaveEdit.Data
{
    public class Skill
    {
        public long Id { get; set; }
        public int Level { get; set; }

        internal int GetSize()
        {
            return 8 // Id
                + 4; // Level
        }
    }

    internal static class SkillExtensions
    {
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

        public static void WriteSkill(this BinaryWriter writer, Skill skill)
        {
            writer.Write(skill.Id);
            writer.Write(skill.Level);
        }
    }
}
