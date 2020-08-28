using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace libloaderapi.Utils
{
    public static class CryptoUtils
    {
        public static byte[] Sha256(byte[] buffer)
        {
            return new SHA256Managed().ComputeHash(buffer);
        }

        public static string Sha265HashAsString(byte[] buffer)
        {
            return string.Concat(Sha256(buffer)
                .Select(b => b.ToString("x2")));
        }

        public static string Sha256(string password)
        {
            return string.Concat(Sha256(Encoding.UTF8.GetBytes(password))
                .Select(b => b.ToString("x2")));
        }

        public static string CreatePseudoRandomKey()
        {
            using var rng = new RNGCryptoServiceProvider();
            var key = new byte[63];
            rng.GetBytes(key);
            var regex = new Regex("[^a-z0-9 ]",
                RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);

            return regex.Replace(Convert.ToBase64String(key), "q");
        }
    }
}
