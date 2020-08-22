using System.ComponentModel.DataAnnotations;

namespace libloaderapi.Domain.Dto
{
    public class ClientDataReq
    {
        [Required]
        public ulong Key { get; set; }

        [Required]
        public byte[] Payload { get; set; }

        [Required]
        public byte Iter { get; set; }
    }
}
