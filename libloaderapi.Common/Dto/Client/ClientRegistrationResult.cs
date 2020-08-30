namespace libloaderapi.Common.Dto.Client
{
    public class ClientRegistrationResult : ApiResponse
    {
        public string ApiKey { get; set; }
        public bool Skipped { get; set; }
    }
}