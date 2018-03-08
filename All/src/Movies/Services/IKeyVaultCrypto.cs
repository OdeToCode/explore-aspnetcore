using System.Threading.Tasks;

namespace Movies.Services
{
    public interface IKeyVaultCrypto
    {
        Task<string> DecryptAsync(string encryptedText);
        Task<string> EncryptAsync(string value);
        Task<string> UnwrapAsync(string encryptedKey);
        Task<string> WrapKeyAsync(string key);
    }
}