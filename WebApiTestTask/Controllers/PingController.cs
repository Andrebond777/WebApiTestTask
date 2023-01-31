using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiTestTask.Controllers
{
    [ApiController]
    [Route("ping")]
    public class PingController : ControllerBase
    {
        [HttpGet]
        public ContentResult Ping()
        {
            return Content("Dogs house service. Version 1.0.1");
        }
    }
}
