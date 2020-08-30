namespace libloaderapi.Common.Dto.Auth
{
    public class ClientAuthRequest
    {
        /// <summary>
        /// The SHA1 hash of the client executable
        /// </summary>
        public string CryptoId { get; set; }

        /// <summary>
        /// The HMACSHA56/1 digest of the client executable's SHA1 hash
        /// </summary>
        public string Digest { get; set; }
    }
}
