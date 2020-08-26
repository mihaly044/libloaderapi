using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace libloaderapi.Domain.Utils
{
    public static class PasswordUtils
    {
        public static string GetSHA1Hash(string password)
        {
            return string.Concat(new SHA1Managed()
                .ComputeHash(Encoding.UTF8.GetBytes(password))
                .Select(b => b.ToString("x2")));
        }
    }
}
