using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace libloaderapi.Utils
{
    public static class CryptoUtils
    {
        public static byte[] CreatePseudoRandomKey()
        {
            using var rng = new RNGCryptoServiceProvider();
            var key = new byte[128];
            rng.GetBytes(key);
            return key;
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
