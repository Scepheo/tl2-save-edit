using Xunit;

namespace Tl2SaveEdit.UnitTests
{
    public class EncryptionTests
    {
        [Fact]
        public void RoundTrip()
        {
            // Arrange
            var original = TestFile.Read("new_mage.svb");

            // Act
            var decrypted = Encryption.Decrypt(original);
            var copy = Encryption.Encrypt(decrypted);

            // Assert
            Assert.Equal(original, copy);
        }
    }
}
