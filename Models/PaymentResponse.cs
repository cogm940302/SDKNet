using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace SdkNet.Models
{
    [XmlRoot("CENTEROFPAYMENTS")]
    public class PaymentResponse
    {
        [XmlElement()]
        public string reference;

        [XmlElement()]
        public string response;

        [XmlElement("foliocpagos")]
        public string folio;

        [XmlElement("auth")]
        public string authorization;

        [XmlElement("cd_response")]
        public string responseCode;

        [XmlElement("cd_error")]
        public string errorCode;

        [XmlElement("nb_error")]
        public string errorDescription;

        [XmlElement()]
        public TimeOnly time;

        [XmlElement()]
        public DateOnly date;

        [XmlElement("nb_company")]
        public string companyName;

        [XmlElement("nb_merchant")]
        public string merchantName;

        [XmlElement("cc_type")]
        public string ccType;

        [XmlElement("tp_operation")]
        public string operacion;

        [XmlElement("cc_name ")]
        public string name;

        [XmlElement("cc_number")]
        public string ccNumber;

        [XmlElement("amount")]
        public double amount;

        [XmlElement("id_url")]
        public string idUrl;

        [XmlElement("email")]
        public string eamil;

        [XmlElement("cc_mask")]
        public string ccMask;

        [XmlElement("datos_adicionales")]
        public List<Data> datosAdicionales;
    }
}
