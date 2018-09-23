using Xunit;

namespace Tl2SaveEdit.UnitTests
{
    public class ParsingTests
    {
        [Fact]
        public void RoundTrip()
        {
            // Arrange
            var data = TestFile.Read("new_mage.svb");

            // Act
            var saveFile = SaveFile.Parse(data);
            var copy = saveFile.Write();

            // Assert
            Assert.Equal(data, copy);
        }
    }
}
