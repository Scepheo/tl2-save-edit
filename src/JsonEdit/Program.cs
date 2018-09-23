using Newtonsoft.Json;
using System;
using System.IO;
using Tl2SaveEdit;
using Tl2SaveEdit.Data;

namespace JsonEdit
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 3)
            {
                Console.WriteLine("You forgot the arguments");
                return;
            }

            var action = args[0].ToUpperInvariant();
            var saveFilename = args[1];
            var jsonFilename = args[2];

            switch (action)
            {
                case "READ":
                    {
                        var data = File.ReadAllBytes(saveFilename);
                        var saveFile = SaveFile.Parse(data);
                        var json = JsonConvert.SerializeObject(saveFile.HeroData, Formatting.Indented);
                        File.WriteAllText(jsonFilename, json);
                        break;
                    }
                case "WRITE":
                    {
                        var json = File.ReadAllText(jsonFilename);
                        var heroData = JsonConvert.DeserializeObject<HeroData>(json);

                        var data = File.ReadAllBytes(saveFilename);
                        var saveFile = SaveFile.Parse(data);

                        saveFile.HeroData = heroData;
                        var newData = saveFile.Write();

                        File.WriteAllBytes(saveFilename, newData);
                        break;
                    }
            }
        }
    }
}
