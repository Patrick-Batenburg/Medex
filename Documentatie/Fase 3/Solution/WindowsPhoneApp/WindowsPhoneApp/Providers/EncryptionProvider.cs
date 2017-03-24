using Windows.Security.Cryptography;
using Windows.Storage.Streams;
using Windows.Security.Cryptography.Core;
using Windows.UI.Xaml;

namespace WindowsPhoneApp.Providers
{
    public class EncryptionProvider
    {
        private App app = (Application.Current as App);

        /// <summary> 
        /// Encrypt a string 
        /// </summary> 
        /// <param name="value">String to encrypt</param> 
        /// <returns>Encrypted string</returns> 
        public string Encrypt(string value)
        {
            HashAlgorithmProvider hashAlgorithmProvider = HashAlgorithmProvider.OpenAlgorithm(HashAlgorithmNames.Sha256);
            IBuffer hash = hashAlgorithmProvider.HashData(CryptographicBuffer.ConvertStringToBinary(value + app.SALT, BinaryStringEncoding.Utf8));
            return CryptographicBuffer.EncodeToHexString(hash);
        }
    }
}
