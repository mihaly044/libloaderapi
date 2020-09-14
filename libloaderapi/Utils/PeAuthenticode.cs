using System;
using System.IO;
using System.Security.Cryptography;
using System.Threading.Tasks;
using PeNet;
using PeNet.Header.Pe;

namespace libloaderapi.Utils
{
    public class PeAuthenticode
    {
        public static async Task<byte[]> GetDigest(string file)
        {
            await using var fs = File.OpenRead(file);
            return await GetDigest(fs);
        }

        public static async Task<byte[]> GetDigest(Stream stream)
        {
            // PeNet will use this stream internally so we copy it for our purposes
            Stream ms = new MemoryStream();
            await stream.CopyToAsync(ms);
            ms.Position = 0;
            using var reader = new BinaryReader(ms);

            if (!PeFile.TryParse(stream, out var peFile) || peFile == null)
                return null; // Not a PE file

            // Decide what kind of hashing algorithm will we be using
            using var sha = SHA1.Create();

            // Hash everything up to the checksum
            if (peFile.ImageDosHeader == null || peFile.ImageNtHeaders == null)
                return null;

            var buf = reader.ReadBytes((int)(peFile.ImageDosHeader.E_lfanew + 20 + 4 + 0x40));
            sha.TransformBlock(buf, 0, buf.Length, null, 0);

            // We're skipping the checksum field here
            ms.Seek(4, SeekOrigin.Current);

            // Hash everything up to the security directory information
            // for x64 it's 16 bytes further
            buf = reader.ReadBytes(peFile.Is64Bit ? 0x3c + 0x10 : 0x3c);
            sha.TransformBlock(buf, 0, buf.Length, null, 0);

            // Skipping an other ignored field
            ms.Seek(8, SeekOrigin.Current);

            // Check if we have an embedded authenticode signature
            var embeddedSig =
                peFile.ImageNtHeaders?.OptionalHeader.DataDirectory[(int)DataDirectoryType.Security].VirtualAddress != 0;

            // The remaining bytes to hash if either all or if we have a signature,
            // it's everything up to the security directory
            long remaining;
            if (embeddedSig)
            {
                remaining = peFile.ImageNtHeaders.OptionalHeader.DataDirectory[(int)DataDirectoryType.Security]
                    .VirtualAddress - ms.Position;
            }
            else
            {
                remaining = ms.Length - ms.Position;
            }

            while (remaining > 0)
            {
                // We're hashing by 4096 bytes long blocks or otherwise what's left
                var chunk = (int)Math.Min(remaining, 4096);
                int read;

                if (ms.Position + chunk > ms.Length)
                {
                    read = (int)ms.Length - chunk;
                    buf = reader.ReadBytes(read);
                }
                else
                {
                    read = chunk;
                    buf = reader.ReadBytes(read);
                }

                remaining -= read;


                if (remaining == 0)
                    sha.TransformFinalBlock(buf, 0, buf.Length);
                else
                    sha.TransformBlock(buf, 0, buf.Length, null, 0);
            }

            return sha.Hash;
        }
    }
}
