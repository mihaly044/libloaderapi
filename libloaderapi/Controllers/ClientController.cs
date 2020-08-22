using System;
using System.Collections.Generic;
using System.Linq;
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
        public ActionResult<ulong> Test(ClientDataReq req)
        {
            if (req.Param.Length < 200 || req.Param.Length > 500)
                return BadRequest();

            switch (req.Step)
            {
                case 0:

                    unsafe
                    {
                        fixed (byte* @base = &req.Param[1])
                        {
                            var p = @base;

                            for (var i = 0; i < 100; i++, p++)
                            {
                                if ((p[-1] & 0xfe) != 0xe8 || (((p[2] | p[3]) != 0) && ((p[2] & p[3]) != 0xff)))
                                    continue;

                                var t = p + 4 + *(int*)p;
                                if (t[0] == 0x48 && t[1] == 0x8b && t[2] == 0x05)
                                    continue;

                                return Ok((ulong) p);
                            }

                            return NotFound();
                        }
                    }

                case 1:
                    unsafe
                    {
                        fixed (byte* @base = &req.Param[2])
                        {
                            var p = @base;
                            for (var i = 0; i < 100; i++, p++)
                            {
                                if (p[-2] == 0x89 && p[-1] == 0x0d && p[3] == 0xff)
                                {
                                    return Ok((ulong) p);
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
