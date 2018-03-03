using Microsoft.Azure.KeyVault;
using Microsoft.Azure.KeyVault.WebKey;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Services
{
    public class KeyVaultCrypto
    {
        private readonly KeyVaultClient client;
        private readonly string keyId;

        public KeyVaultCrypto(KeyVaultClient client, string keyId)
        {
            this.client = client;
            this.keyId = keyId;
        }

        public async Task<string> DecryptAsync(string encryptedText)
        {
            var encryptedBytes = Convert.FromBase64String(encryptedText);
            var decryptionResult = await client.DecryptAsync(keyId, JsonWebKeyEncryptionAlgorithm.RSAOAEP, encryptedBytes);
            var decryptedText = Encoding.Unicode.GetString(decryptionResult.Result);
            return decryptedText;
        }

        public async Task<string> EncryptAsync(string value)
        {
            var bundle = await client.GetKeyAsync(keyId);
            var key = bundle.Key;

            using (var rsa = new RSACryptoServiceProvider())
            {
                var parameters = new RSAParameters() { Modulus = key.N, Exponent = key.E };
                rsa.ImportParameters(parameters);
                var byteData = Encoding.Unicode.GetBytes(value);

                var encryptedText = rsa.Encrypt(byteData, true);
                var encodedText = Convert.ToBase64String(encryptedText);
                return encodedText;
            }
        }
    }
}
