using System.Collections.Generic;
using System.Linq;

namespace Tl2SaveEdit.Data
{
    public class CharacterClass
    {
        public string MaleKey { get; }
        public string FemaleKey { get; }
        public string Name { get; }

        internal CharacterClass(string name, string maleKey, string femaleKey)
        {
            Name = name;
            MaleKey = maleKey;
            FemaleKey = femaleKey;
        }

        public override string ToString() => Name;

        public static (CharacterClass CharacterClass, bool IsMale) FindByKey(string key)
        {
            var characterClass = CharacterClasses.First(c => c.MaleKey == key || c.FemaleKey == key);
            var isMale = characterClass.MaleKey == key;
            return (characterClass, isMale);
        }

        public static readonly IReadOnlyList<CharacterClass> CharacterClasses = new []
        {
            new CharacterClass("Embermage", "Hum_Arbiter_M", "Hum_Arbiter_F"),
            new CharacterClass("Berserker", "Hum_Berserker_M", "Hum_Berserker_F"),
            new CharacterClass("Engineer", "Hum_Engineer_M", "Hum_Engineer_F"),
            new CharacterClass("Outlander", "Hum_Wanderer_M", "Hum_Wanderer_F")
        };
    }
}
