using System.ComponentModel.DataAnnotations;

namespace libloaderapi.Domain.Dto
{
    public class ClientDataReq
    {
        public byte[] Payload { get; set; }

        [Required]
        public byte Iter { get; set; }
    }
}
