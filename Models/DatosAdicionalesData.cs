using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SdkNet.Models
{
    public class DatosAdicionalesData
    {
        public class DataItem
        {
            public DataItem(){}
            [XmlElement()]
            public int id;
            [XmlElement()]
            public bool display;
            [XmlElement()]
            public string label;
            [XmlElement()]
            public string value;
        }
        [XmlElement()]
        public HashSet<DataItem> data;

        public void Append(DataItem aditionalData)
        {
            if (data == null)
            {
                data = new HashSet<DataItem>();
            }
            data.Add(aditionalData);
        }

    }
}
