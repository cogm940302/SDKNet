using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SdkNet.Models
{
    public class D3DSData
    {
        [XmlElement("ml")]
        public string email;
        [XmlElement("cl")]
        public string phone;
        [XmlElement("dir")]
        public string address;
        [XmlElement("cd")]
        public string city;
        [XmlElement("est")]
        public string state;
        [XmlElement("cp")]
        public string zipCode;
        [XmlElement("idc")]
        public string countryCode;
    }
}
