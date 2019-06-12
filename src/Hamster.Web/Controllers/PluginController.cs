using Microsoft.AspNetCore.Mvc;
using Hamster.Plugin;

namespace Hamster.Web.Controllers
{
    [Route("plugin")]
    public class PluginController : Controller
    {
        private IPlugin plugin;

        public PluginController(IPlugin plugin)
        {
            this.plugin = plugin;
        }

        [HttpGet("status")]
        public IActionResult Status()
        {
            if (plugin.IsOpen) {
                return Ok("open");
            } else {
                return Ok("closed");
            }
        }

        [HttpPut("status")]
        public IActionResult Status(string status)
        {
            switch (status) {
                case "open":
                    plugin.Open();
                    return Ok();

                case "closed":
                    plugin.Close();
                    return Ok();

                default:
                    return BadRequest();
            }
        }
    }
}
