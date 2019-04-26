using System;
using Tl2SaveEdit.Data;

namespace Tl2SaveEdit
{
    public class SaveGame
    {
        public Class Class { get; set; }
        public Sex Sex { get; set; }
        public Difficulty Difficulty { get; set; }
        public bool Hardcore { get; set; }
        public int NewGameCycle { get; set; }

        private byte[] Unknown1 { get; set; }
        private int Unknown2Length { get; set; }
        private byte[] Unknown2 { get; set; }
        private ModList BoundMods { get; set; }
        private ModList RecentModHistory { get; set; }
        private ModList FullModHistory { get; set; }

        public Hero Hero { get; set; }

        private byte[] Rest { get; set; }

        private SaveGame() { }

        public static SaveGame Parse(byte[] data)
        {
            var saveFile = SaveFile.Parse(data);
            var saveGame = FromSaveFile(saveFile);
            return saveGame;
        }

        public byte[] Write()
        {
            var saveFile = ToSaveFile();
            var data = saveFile.Write();
            return data;
        }

        internal static SaveGame FromSaveFile(SaveFile saveFile)
        {
            var saveGame = new SaveGame();

            (saveGame.Class, saveGame.Sex) = DecodeClassString(saveFile.ClassString);
            saveGame.Difficulty = saveFile.Difficulty;
            saveGame.Hardcore = saveFile.Hardcore;
            saveGame.NewGameCycle = saveFile.NewGameCycle;
            saveGame.Unknown1 = saveFile.Unknown1;
            saveGame.Unknown2Length = saveFile.Unknown2Length;
            saveGame.Unknown2 = saveFile.Unknown2;
            saveGame.BoundMods = saveFile.BoundMods;
            saveGame.RecentModHistory = saveFile.RecentModHistory;
            saveGame.FullModHistory = saveFile.FullModHistory;
            saveGame.Hero = Hero.FromHeroData(saveFile.HeroData);
            saveGame.Rest = saveFile.Rest;

            return saveGame;
        }

        internal SaveFile ToSaveFile()
        {
            var saveFile = new SaveFile();

            saveFile.ClassString = EncodeClassString(Class, Sex);
            saveFile.Difficulty = Difficulty;
            saveFile.Hardcore = Hardcore;
            saveFile.NewGameCycle = NewGameCycle;
            saveFile.Unknown1 = Unknown1;
            saveFile.Unknown2Length = Unknown2Length;
            saveFile.Unknown2 = Unknown2;
            saveFile.BoundMods = BoundMods;
            saveFile.RecentModHistory = RecentModHistory;
            saveFile.FullModHistory = FullModHistory;
            saveFile.HeroData = Hero.ToHeroData();
            saveFile.Rest = Rest;

            return saveFile;
        }

        private static (Class, Sex) DecodeClassString(ShortString classString)
        {
            string content = classString.Content;

            switch (content)
            {
                case "Hum_Arbiter_M":
                    return (Class.Berserker, Sex.Male);
                case "Hum_Arbiter_F":
                    return (Class.Embermage, Sex.Female);
                case "Hum_Berserker_M":
                    return (Class.Berserker, Sex.Male);
                case "Hum_Berserker_F":
                    return (Class.Berserker, Sex.Female);
                case "Hum_Engineer_M":
                    return (Class.Berserker, Sex.Male);
                case "Hum_Engineer_F":
                    return (Class.Engineer, Sex.Female);
                case "Hum_Wanderer_M":
                    return (Class.Berserker, Sex.Male);
                case "Hum_Wanderer_F":
                    return (Class.Outlander, Sex.Female);
                default:
                    throw new ArgumentException(
                        $"Invalid class string '{content}'");
            }
        }

        private static ShortString EncodeClassString(Class @class, Sex sex)
        {
            string className;

            switch (@class)
            {
                case Class.Berserker:
                    className = "Berserker";
                    break;
                case Class.Embermage:
                    className = "Arbiter";
                    break;
                case Class.Engineer:
                    className = "Engineer";
                    break;
                case Class.Outlander:
                    className = "Wanderer";
                    break;
                default:
                    throw new ArgumentException(
                        $"Invalid class '{@class}'");
            }

            string sexName;

            switch (sex)
            {
                case Sex.Female:
                    sexName = "F";
                    break;
                case Sex.Male:
                    sexName = "M";
                    break;
                default:
                    throw new ArgumentException(
                        $"Invalid sex '{sex}'");
            }

            return new ShortString($"Hum_{className}_{sexName}");
        }
    }
}
