using System.IO;
using System.Reflection;

namespace Tl2SaveEdit.UnitTests
{
    public static class TestFile
    {
        private static readonly Assembly _assembly = Assembly.GetExecutingAssembly();

        public static byte[] Read(string name)
        {
            var fullName = $"Tl2SaveEdit.UnitTests.Files.{name}";

            using (var stream = _assembly.GetManifestResourceStream(fullName))
            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}
