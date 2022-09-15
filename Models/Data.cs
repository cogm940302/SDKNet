using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SdkNet.Models
{
    public class Data
    {
        [XmlElement()]
        public string label { get; set; }

        [XmlElement()]
        public string value { get; set; }
    }
}
