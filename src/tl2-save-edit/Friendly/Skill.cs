using System.Collections.Generic;
using System.Linq;
using Tl2SaveEdit.Data;

namespace Tl2SaveEdit
{
    public class Skill
    {
        private long Id { get; set; }
        private int Level { get; set; }

        internal static Skill FromDataSkill(Data.Skill dataSkill)
        {
            var Skill = new Skill();

            Skill.Id = dataSkill.Id;
            Skill.Level = dataSkill.Level;

            return Skill;
        }

        internal Data.Skill ToDataSkill()
        {
            var dataSkill = new Data.Skill();

            dataSkill.Id = Id;
            dataSkill.Level = Level;

            return dataSkill;
        }

        internal static List<Skill> FromSkillList(SkillList SkillList)
        {
            var Skills = SkillList.Skills.Select(FromDataSkill).ToList();
            return Skills;
        }

        internal static SkillList ToSkillList(List<Skill> Skills)
        {
            var dataSkills = Skills.Select(Skill => Skill.ToDataSkill()).ToArray();

            var SkillList = new SkillList()
            {
                Skills = dataSkills,
            };

            return SkillList;

        }
    }
}
