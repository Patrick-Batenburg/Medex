using System.Text;
using Windows.Security.Cryptography;
using Windows.Storage.Streams;
using Windows.Security.Cryptography.Core;
using System;

namespace WindowsPhoneApp.Providers
{
    public static class EncryptionProvider
    {
        /// <summary> 
        /// Encrypt a string 
        /// </summary> 
        /// <param name="value">String to encrypt</param> 
        /// <returns>Encrypted string</returns> 
        public static string Encrypt(string value)
        {
            HashAlgorithmProvider hashAlgorithmProvider = HashAlgorithmProvider.OpenAlgorithm(HashAlgorithmNames.Md5);
            IBuffer hash = hashAlgorithmProvider.HashData(CryptographicBuffer.ConvertStringToBinary(value, BinaryStringEncoding.Utf8));

            return CryptographicBuffer.EncodeToHexString(hash);
        }
    }
}
