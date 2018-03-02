using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Hamster.Web
{
    [XmlRoot("settings", Namespace = "http://www.nowisys.de/hamster/plugins/services/web.xsd")]
    public class WebPluginSettings
    {
        [XmlElement("url")]
        public string Url { get; set; }

        [XmlElement("discoverDirectory")] 
        public string DiscoverDirectory { get; set; }
    }
}
