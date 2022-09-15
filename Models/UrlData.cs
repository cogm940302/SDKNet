using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SdkNet.Models
{
    public class UrlData
    {
        public enum MonedaType
        {
            MXN, USD
        };
        [XmlElement()]
        public string reference { get; set; }
        [XmlElement()]
        public double amount { get; set; }
        [XmlElement()]
        public MonedaType moneda { get; set; }
        [XmlElement()]
        public string canal  { get; set; }
        [XmlElement("omitir_notif_default")]
        public int omitNotification { get; set; }
        [XmlElement("promociones")]
        public string promotions { get; set; }
        [XmlElement("id_promotion")]
        public string idPromotion { get; set; }
        [XmlElement("st_correo")]
        public int stEmail { get; set; }
        [XmlElement("fh_vigencia")]
        public DateTime? expirationDate = null;
        [XmlElement("mail_cliente")]
        public string clientEmail { get; set; }
        [XmlElement("prepago")]
        public int prepaid { get; set; } // 0 | 1
        public void SetPromotions(String[] promotion)
        {
            String promos = "";
            foreach (String promo in promotion)
            {
                promos += promo + ",";
            }
            this.promotions = promos.Substring(0, promos.Length - 1);
        }
    }
}
