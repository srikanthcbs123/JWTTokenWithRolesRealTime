using JWTTokenWithRolesRealTime.Utils;

namespace JWTTokenWithRolesRealTime.Interfaces
{
    public interface IEncryptionLibrary
    {
        Task<string> EncryptText(string inputText);
        Task<string> DecryptText(string inputText);
    }
}
