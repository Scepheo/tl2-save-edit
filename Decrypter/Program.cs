using System;
using System.IO;
using Tl2SaveEdit;

namespace Decrypter
{
    internal static class Program
    {
        private static int Main(string[] args)
        {
            if (args.Length != 3)
            {
                Console.WriteLine($"Expected 3 argument, got {args.Length}");
                return 1;
            }

            var mode = args[0];
            bool encrypt;

            switch (mode.ToUpperInvariant())
            {
                case "ENCRYPT":
                    encrypt = true;
                    break;
                case "DECRYPT":
                    encrypt = false;
                    break;
                default:
                    Console.WriteLine($"Expected 'encrypt' or 'decrypt' as mode, got '{mode}' instead");
                    return 1;
            }

            var inputFilename = args[1];

            if (!File.Exists(inputFilename))
            {
                Console.WriteLine($"Input file '{inputFilename}' does not exist");
                return 1;
            }

            var outputFilename = args[2];

            if (File.Exists(outputFilename))
            {
                Console.WriteLine($"File '{outputFilename}' already exists");
                return 1;
            }

            var data = File.ReadAllBytes(inputFilename);

            if (encrypt)
            {
                Encryption.Encrypt(data);
            }
            else
            {
                Encryption.Decrypt(data);
            }

            File.WriteAllBytes(outputFilename, data);
            return 0;
        }
    }
}
