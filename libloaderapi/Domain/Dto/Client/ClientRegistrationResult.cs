using System;

namespace libloaderapi.Domain.Dto.Client
{
    public class ClientRegistrationResult : ApiResponse
    {
        public string ApiKey { get; set; }
        public bool Skipped { get; set; }
    }
}