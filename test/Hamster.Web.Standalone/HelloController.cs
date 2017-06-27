using Microsoft.AspNetCore.Mvc;

namespace Hamster.Web.Standalone
{
    public class HelloController : Controller
    {
        public string Index()
        {
            return "Hello world!";
        }
    }
}
