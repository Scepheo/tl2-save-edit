namespace Tl2SaveEdit.Data
{
    public class SkillList
    {
        public Skill[] Skills { get; set; }

        public override string ToString()
        {
            return $"{Skills.Length} skills";
        }
    }
}
