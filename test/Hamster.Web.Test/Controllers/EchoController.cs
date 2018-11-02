using System;
using Microsoft.AspNetCore.Mvc;

namespace Hamster.Web.Test.Controllers
{
    public class EchoController : Controller
    {
        public IActionResult Echo()
        {
            return Ok("foobar");
        }
    }
}
