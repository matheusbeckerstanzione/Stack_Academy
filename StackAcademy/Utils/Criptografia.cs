using System.Security.Cryptography;
using System.Text;

namespace StackAcademy.Utils;

public static class Criptografia
{
    public static string GerarHash(string senha)
    {
        using var sha1 = SHA1.Create();
        var bytes = Encoding.UTF8.GetBytes(senha);
        var hash = sha1.ComputeHash(bytes);
        return Convert.ToHexString(hash).ToLower();    }

    public static bool ComapararHash(string senhaForm, string senhaBanco)
    {
        return GerarHash(senhaForm) == senhaBanco;
    }
}