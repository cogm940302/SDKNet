using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace SdkNet.Models
{
    [XmlRoot("P_RESPONSE")]
    public class GenUrlResponse
    {
        [XmlElement("cd_response")]
        public string cdResponse { get; set; }

        [XmlElement("nb_response")]
        public string nbResponse { get; set; }
        
        [XmlElement("nb_url")]
        public string nbUrl { get; set; }
    }
}
