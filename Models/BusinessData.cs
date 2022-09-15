using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SdkNet.Models
{
    public class BusinessData
    {

        [XmlElement("id_company")]
        public string idCompany { get; set; }

        [XmlElement("id_branch")]
        public string idBranch { get; set; }

        [XmlElement()]
        public string user { get; set; }

        [XmlElement()]
        public string pwd { get; set; }
    }
}
