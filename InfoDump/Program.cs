using System;
using System.CodeDom.Compiler;
using System.IO;
using System.Text;
using Tl2SaveEdit;

namespace InfoDump
{
    internal static class Program
    {
        private static int Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine($"Expected 2 argument, got {args.Length}");
                return 1;
            }

            var inputFilename = args[0];

            if (!File.Exists(inputFilename))
            {
                Console.WriteLine($"File '{inputFilename}' does not exist");
                return 1;
            }

            var outputFilename = args[1];

            var data = File.ReadAllBytes(inputFilename);
            var saveFile = SaveFile.Parse(data);

            using (var writer = new StreamWriter(outputFilename))
            using (var indentedWriter = new IndentedTextWriter(writer, "  "))
            {
                foreach (var property in saveFile.GetType().GetProperties())
                {
                    indentedWriter.WriteLine(property.Name);
                    indentedWriter.Indent++;
                    var value = property.GetValue(saveFile);
                    Dumper.Dump(indentedWriter, value);
                    indentedWriter.Indent--;
                    indentedWriter.WriteLine();
                }
            }

            return 0;
        }
    }
}
