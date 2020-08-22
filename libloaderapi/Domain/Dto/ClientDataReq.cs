using System.ComponentModel.DataAnnotations;

namespace libloaderapi.Domain.Dto
{
    public class ClientDataReq
    {
        [Required]
        public byte[] Param { get; set; }

        [Required]
        public byte Step { get; set; }
    }
}
