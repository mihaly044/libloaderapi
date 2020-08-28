using System.ComponentModel.DataAnnotations;

namespace libloaderapi.Domain.Dto.Client
{
    public class AnalyserContext
    {
        [Required]
        public byte[] Payload { get; set; }

        [Required]
        public byte Iter { get; set; }
    }
}
