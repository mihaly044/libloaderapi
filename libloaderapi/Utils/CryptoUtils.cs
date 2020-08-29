using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace libloaderapi.Utils
{
    public static class CryptoUtils
    {
        public static async Task<string> CreatePseudoRandomKey()
        {
            using var rng = new RNGCryptoServiceProvider();
            var key = new byte[63];
            rng.GetBytes(key);
            var regex = new Regex("[^a-z0-9 ]",
                RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);

            return await Task.FromResult(regex.Replace(Convert.ToBase64String(key), "q"));
        }

        public static async Task<string> CalcSha256Hash(string password)
        {
            return await Task.FromResult(string.Concat(new SHA256Managed()
                .ComputeHash(Encoding.UTF8.GetBytes(password))
                .Select(x => x.ToString("x2"))));
        }

        public static async Task<string> CalcSha256Hash(Stream stream)
        {
            return await Task.FromResult(string.Concat(new SHA256Managed()
                .ComputeHash(stream)
                .Select(x => x.ToString("x2"))));
        }
    }
}
