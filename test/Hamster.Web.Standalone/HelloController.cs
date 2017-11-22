using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hamster.Web.Standalone
{
    public class HelloController : Controller
    {
        [HttpGet]
        public string Index()
        {
            return "Hello world!";
        }
    }
}
