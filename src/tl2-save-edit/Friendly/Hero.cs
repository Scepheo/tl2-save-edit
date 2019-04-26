using System.Collections.Generic;
using Tl2SaveEdit.Data;

namespace Tl2SaveEdit
{
    public class Hero
    {
        private byte[] Unknown1 { get; set; }
        private byte[] Block1 { get; set; }
        private byte[] Unknown2 { get; set; }
        private int Face { get; set; }
        private int Hairstyle { get; set; }
        private int HairColor { get; set; }
        private byte[] Unknown3 { get; set; }

        public bool Cheater { get; set; }

        private byte[] Unknown4 { get; set; }

        public string Name { get; set; }

        private byte[] Unknown5 { get; set; }
        private ShortString PlayerNumber { get; set; }
        private byte[] Unknown6 { get; set; }

        public int Level { get; set; }
        public int Experience { get; set; }
        public int FameLevel { get; set; }
        public int Fame { get; set; }
        public float Health { get; set; }
        public int HealthBonus { get; set; }

        private byte[] Unknown7 { get; set; }
        public float Mana { get; set; }
        public int ManaBonus { get; set; }

        private byte[] Unknown8 { get; set; }

        public float PlayTime { get; set; }

        private byte[] Unknown9 { get; set; }

        public int UnallocatedSkillPoints { get; set; }
        public int UnallocatedAttributePoints { get; set; }

        private byte[] Unknown10 { get; set; }

        public List<Skill> Skills { get; set; }
        public List<Spell> Spells { get; set; }

        private byte[] Unknown11 { get; set; }

        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Vitality { get; set; }
        public int Focus { get; set; }
        public int Gold { get; set; }

        private byte[] Unknown12 { get; set; }
        private byte[] Block2 { get; set; }
        private ModIdList ModIds { get; set; }

        public Inventory Inventory { get; set; }

        private PassiveList Passives1 { get; set; }
        private PassiveList Passives2 { get; set; }
        private byte[] Unknown13 { get; set; }
        private ShortStringList Unknown14 { get; set; }
        private byte[] Unknown15 { get; set; }

        private Hero() { }

        internal static Hero FromHeroData(HeroData heroData)
        {
            var hero = new Hero();

            hero.Unknown1 = heroData.Unknown1;
            hero.Block1 = heroData.Block1;
            hero.Unknown2 = heroData.Unknown2;
            hero.Face = heroData.Face;
            hero.Hairstyle = heroData.Hairstyle;
            hero.HairColor = heroData.HairColor;
            hero.Unknown3 = heroData.Unknown3;

            hero.Cheater = heroData.Cheater == 1;

            hero.Unknown4 = heroData.Unknown4;

            hero.Name = heroData.CharacterName.Content;

            hero.Unknown5 = heroData.Unknown5;
            hero.PlayerNumber = heroData.PlayerNumber;
            hero.Unknown6 = heroData.Unknown6;

            hero.Level = heroData.Level;
            hero.Experience = heroData.Experience;
            hero.FameLevel = heroData.FameLevel;
            hero.Fame = heroData.Fame;
            hero.Health = heroData.Health;
            hero.HealthBonus = heroData.HealthBonus;

            hero.Unknown7 = heroData.Unknown7;
            hero.Mana = heroData.Mana;
            hero.ManaBonus = heroData.ManaBonus;

            hero.Unknown8 = heroData.Unknown8;

            hero.PlayTime = heroData.PlayTime;

            hero.Unknown9 = heroData.Unknown9;

            hero.UnallocatedSkillPoints = heroData.UnallocatedSkillPoints;
            hero.UnallocatedAttributePoints = heroData.UnallocatedAttributePoints;

            hero.Unknown10 = heroData.Unknown10;

            hero.Skills = Tl2SaveEdit.Skill.FromSkillList(heroData.Skills);
            hero.Spells = Tl2SaveEdit.Spell.FromSpellList(heroData.Spells);

            hero.Unknown11 = heroData.Unknown11;

            hero.Strength = heroData.Strength;
            hero.Dexterity = heroData.Dexterity;
            hero.Vitality = heroData.Vitality;
            hero.Focus = heroData.Focus;
            hero.Gold = heroData.Gold;

            hero.Unknown12 = heroData.Unknown12;
            hero.Block2 = heroData.Block2;
            hero.ModIds = heroData.ModIds;

            hero.Inventory = Inventory.FromItemList(heroData.Items);

            hero.Passives1 = heroData.Passives1;
            hero.Passives2 = heroData.Passives2;
            hero.Unknown13 = heroData.Unknown13;
            hero.Unknown14 = heroData.Unknown14;
            hero.Unknown15 = heroData.Unknown15;

            return hero;
        }

        internal HeroData ToHeroData()
        {
            var heroData = new HeroData();

            heroData.Unknown1 = Unknown1;
            heroData.Block1 = Block1;
            heroData.Unknown2 = Unknown2;
            heroData.Face = Face;
            heroData.Hairstyle = Hairstyle;
            heroData.HairColor = HairColor;
            heroData.Unknown3 = Unknown3;

            heroData.Cheater = Cheater ? 1 : 0;

            heroData.Unknown4 = Unknown4;

            heroData.CharacterName = new ShortString(Name);

            heroData.Unknown5 = Unknown5;
            heroData.PlayerNumber = PlayerNumber;
            heroData.Unknown6 = Unknown6;

            heroData.Level = Level;
            heroData.Experience = Experience;
            heroData.FameLevel = FameLevel;
            heroData.Fame = Fame;
            heroData.Health = Health;
            heroData.HealthBonus = HealthBonus;

            heroData.Unknown7 = Unknown7;
            heroData.Mana = Mana;
            heroData.ManaBonus = ManaBonus;

            heroData.Unknown8 = Unknown8;

            heroData.PlayTime = PlayTime;

            heroData.Unknown9 = Unknown9;

            heroData.UnallocatedSkillPoints = UnallocatedSkillPoints;
            heroData.UnallocatedAttributePoints = UnallocatedAttributePoints;

            heroData.Unknown10 = Unknown10;

            heroData.Skills = Skill.ToSkillList(Skills);
            heroData.Spells = Spell.ToSpellList(Spells);

            heroData.Unknown11 = Unknown11;

            heroData.Strength = Strength;
            heroData.Dexterity = Dexterity;
            heroData.Vitality = Vitality;
            heroData.Focus = Focus;
            heroData.Gold = Gold;

            heroData.Unknown12 = Unknown12;
            heroData.Block2 = Block2;
            heroData.ModIds = ModIds;

            heroData.Items = Inventory.ToItemList();

            heroData.Passives1 = Passives1;
            heroData.Passives2 = Passives2;
            heroData.Unknown13 = Unknown13;
            heroData.Unknown14 = Unknown14;
            heroData.Unknown15 = Unknown15;

            return heroData;
        }
    }
}
