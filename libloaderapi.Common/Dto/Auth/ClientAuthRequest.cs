namespace libloaderapi.Common.Dto.Auth
{
    public class ClientAuthRequest
    {
        /// <summary>
        /// The SHA1 authenticode digest of the client
        /// </summary>
        public string Digest { get; set; }

        /// <summary>
        /// The HMACSHA2561 digest of the authenticode digest
        /// of the client signed with a secret api key
        /// </summary>
        public string Signature { get; set; }
    }
}
