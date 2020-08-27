namespace libloaderapi.Domain.Dto
{
    public class ClientAuthenticationReq
    {
        /// <summary>
        /// The SHA1 hash of the client executable
        /// </summary>
        public string Hash { get; set; }

        /// <summary>
        /// The HMACSHA56/1 digest of the client executable's SHA1 hash
        /// </summary>
        public string Signature { get; set; }
    }
}
