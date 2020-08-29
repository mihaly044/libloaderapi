using System.IO;
using PeNet;

namespace libloaderapi.Utils
{
    public class PeUtils
    {
        public static bool IsValidExe(Stream peStream)
        {
            if (!PeFile.TryParse(peStream, out var peFile))
                return false;

            return peFile != null && peFile.IsExe && !peFile.IsDll;
        }
    }
}
