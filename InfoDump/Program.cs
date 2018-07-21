using System;
using System.IO;
using Tl2SaveEdit;

namespace InfoDump
{
    internal static class Program
    {
        private static int Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine($"Expected 1 argument, got {args.Length}");
                return 1;
            }

            var filename = args[0];

            if (!File.Exists(filename))
            {
                Console.WriteLine($"File '{filename}' does not exist");
                return 1;
            }

            var data = File.ReadAllBytes(filename);
            var saveFile = SaveFile.Parse(data);

            foreach (var property in saveFile.GetType().GetProperties())
            {
                Console.WriteLine($"{property.Name}={property.GetValue(saveFile)}");
            }

            return 0;
        }
    }
}
