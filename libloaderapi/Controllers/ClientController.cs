using libloaderapi.Domain.Dto;
using Microsoft.AspNetCore.Mvc;

namespace libloaderapi.Controllers
{
    [Route("[controller]")]
    //[Authorize(Roles="LibClient")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        [HttpPost("endpoint")]
        public ActionResult<ulong> AnalyzeCiDll([FromBody] ClientDataReq req)
        {
            switch (req.Iter)
            {
                case 0:
                    unsafe
                    {
                        fixed (byte* @base = &req.Payload[1])
                        {
                            byte* p = @base;

                            for (ulong i = 0; i < 100; i++, p += 0x1)
                            {
                                if (((p[-1] & 0xfe) == 0xe8) && (((p[2] | p[3]) <= 0) || ((p[2] & p[3]) == 0xff)))
                                {
                                    var t = p + 4 + *(int*)p;
                                    if (t[0] == 0x48 && t[1] == 0x8b && t[2] == 0x05)
                                        continue;

                                    var j = i;
                                    for (int k = 0; k < req.Payload.Length / 2; k++)
                                        j ^= req.Payload[k];

                                    return Ok(j);
                                }
                            }

                            return NotFound();
                        }
                    }

                case 1:
                    unsafe
                    {
                        fixed (byte* @base = &req.Payload[2])
                        {
                            var p = @base;
                            for (ulong i = 0; i < 100; i++, p++)
                            {
                                if (p[-2] == 0x89 && p[-1] == 0x0d && p[3] == 0xff)
                                {
                                    var j = i;
                                    for (int k = 0; k < req.Payload.Length / 2; k++)
                                        j ^= req.Payload[k];

                                    return Ok(j);
                                }
                            }
                        }
                    }

                    return NotFound();

                default:
                    return BadRequest("Invalid request");
            }
        }
    }
}
