using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using libloaderapi.Domain.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;

namespace libloaderapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        [HttpPost("test")]
        public ActionResult<ulong> Test([FromBody] ClientDataReq req)
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

                                    return Ok(i);
                                }
                            }

                            return NotFound();
                        }
                    }

                // TODO: Review this ...
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
                                    return Ok(i);
                                }
                            }
                        }
                    }

                    return NotFound();

                default:
                    return BadRequest();
            }
        }
    }
}
