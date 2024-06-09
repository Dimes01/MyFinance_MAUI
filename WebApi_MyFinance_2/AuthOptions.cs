using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Text;

namespace WebAPI_MyFinance;

public class AuthOptions
{
    public const string Issuer = "WebAPI_MyFinance";
    public const string Audience = "Client_WebAPI";
    private const string SecretKey = "banknalnvonro3hr9fnlM1M',L;3M128";

    public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
            new(Encoding.UTF8.GetBytes(SecretKey));

    public static byte[] GetHashSHA256(string password) => SHA256.HashData(Encoding.UTF8.GetBytes(password));
}
