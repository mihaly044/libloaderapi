namespace libloaderapi.Common.Dto.Client
{
    public class ClientRegistrationResult : ApiResponse
    {
        public byte[] ApiKey { get; set; }
        public bool Skipped { get; set; }
    }
}