using System;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace libloaderapi.Utils
{
    public static class CryptoUtils
    {
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
