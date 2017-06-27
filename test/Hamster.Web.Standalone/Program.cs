using System;

using Hamster.Plugin.Debug;
using Hamster.Web;

namespace Hamster.Web.Standalone
{
    class Program
    {
        static void Main(string[] args)
        {
            var plugin = new WebPlugin();
            plugin.Name = "Web";
            plugin.Logger = new DebugLogger(plugin.Name);
            plugin.Settings = new WebPluginSettings() { Url="http://localhost:8080/" };
            plugin.Init();
            plugin.Open();

            Console.ReadLine();

            plugin.Close();
        }
    }
}
