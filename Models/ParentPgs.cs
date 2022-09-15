using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SdkNet.Models
{
    [XmlRoot("pgs")]
    public class ParentPgs
    {
        [XmlElement()]
        public string data0;
        [XmlElement()]
        public string data;
    }
}
