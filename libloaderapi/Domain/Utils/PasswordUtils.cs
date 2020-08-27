using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace libloaderapi.Domain.Utils
{
    public static class PasswordUtils
    {
        public static string GetSha1Hash(string password)
        {
            return string.Concat(new SHA1Managed()
                .ComputeHash(Encoding.UTF8.GetBytes(password))
                .Select(b => b.ToString("x2")));
        }

        public static string GetHMacSha1Hash(byte[] bytes, string key)
        {
            return string.Concat(new HMACSHA1(Encoding.UTF8.GetBytes(key))
               .ComputeHash(bytes)
               .Select(b => b.ToString("x2")));
        }
    }
}
