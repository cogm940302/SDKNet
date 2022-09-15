using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SdkNet.Models
{
    [XmlRoot("P")]
    public class PaymentData
    {
        public enum FormaPagoType
        {
            GPY, APY, C2P, COD, TCD, BNPL
        };
        [XmlElement()]
        public BusinessData business;
        [XmlElement("nb_fpago")]
        public FormaPagoType paymentMethod;
        [XmlElement()]
        public string version = "IntegraWPP";
        [XmlElement()]
        public UrlData url;
        [XmlElement()]
        public D3DSData data3ds;
        [XmlElement("datos_adicionales")]
        public DatosAdicionalesData additionalData;

    }
}
