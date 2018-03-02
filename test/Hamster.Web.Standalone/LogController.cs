using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hamster.Web.Standalone
{
    [Route("api/[controller]")]
    public class LogController : Controller
    {
        // GET api/log
        [HttpGet]
        public string Get()
        {
            return "Hello from LogController.";
        }
    }
}
