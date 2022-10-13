using FluentValidation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Linq;
using System.Xml.Serialization;
using SdkNet.AES;
using SdkNet.ExceptionHandler;
using SdkNet.Http;
using SdkNet.Models;
using SdkNet.Validators;
using static SdkNet.Models.DatosAdicionalesData;

namespace SdkNet
{
    public class WPPClient
    {
        private string endpoint;
        private string id;
        private AESHelper aesHelper;

        /// <summary>
        /// Crea un cliente para enviar solicitudes al <i>endpoint</i> dado
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="id"></param>
        /// <param name="key"></param>
        public WPPClient(string endpoint, String id, String key)
        {
            this.endpoint = endpoint;
            this.id = id;
            aesHelper = new AESHelper(key);
        }

        /// <summary>
        /// Obtiene la liga de pagos con los datos proporcionados por payment
        /// </summary>
        /// <param name="payment"></param>
        /// <returns>string</returns>
        /// <exception cref="SdkException"></exception>
        public string GetUrlPayment(PaymentData payment)
        {
            ValidaRequest(payment);
            string body = BuildRequest(payment);
            body = "xml=" + body;
            HttpHelper httpHelper = new HttpHelper(endpoint);
            string xmlResponse = httpHelper.Post(body).GetAwaiter().GetResult();
            if (!IsBase64String(xmlResponse))
            {
                throw new SdkException("Error al enviar el XML: " + xmlResponse);
            }
            xmlResponse = aesHelper.Decrypt(xmlResponse);
            XmlSerializer serializer = new XmlSerializer(typeof(GenUrlResponse));
            GenUrlResponse result;
            try
            {
                using (TextReader reader = new StringReader(xmlResponse))
                {
                    result = (GenUrlResponse)serializer.Deserialize(reader);
                }
            }
            catch (Exception ex)
            {
                throw new SdkException("Error al interpretar el XML: " + xmlResponse);
            }
            if (!"success".Equals(result.cdResponse))
            {
                throw new SdkException(result.nbResponse);
            }
            return result.nbUrl;
        }

        /// <summary>
        /// Metodo para validar los campos de la peticion
        /// </summary>
        /// <param name="payment"></param>
        private void ValidaRequest(PaymentData payment) {
            BusinessDataValidator businessDataValidator = new BusinessDataValidator();
            businessDataValidator.ValidateAndThrow(payment.business);
            if (payment.data3ds != null) { 
                D3DSValidator d3dsValidator = new D3DSValidator();
                d3dsValidator.ValidateAndThrow(payment.data3ds);
            }
            if (payment.additionalData != null && payment.additionalData.data != null)
            {
                foreach (DataItem dataItem in payment.additionalData.data)
                {
                    DatosAdicionalesValidator datosValidator = new DatosAdicionalesValidator();
                    datosValidator.ValidateAndThrow(dataItem);
                }

            }
            UrlValidator urlDataValidator = new UrlValidator();
            urlDataValidator.ValidateAndThrow(payment.url);
        }

        /// <summary>
        /// Convierte el objeto payment a una cadena XML con el formato requerido para generar una liga de pagos.
        /// </summary>
        /// <param name="payment"></param>
        /// <returns>string</returns>
        private string BuildRequest(PaymentData payment)
        {
            string xml = GenerateXML(payment);
            xml = aesHelper.Encrypt(xml);
            ParentPgs parentPgs = new ParentPgs();
            parentPgs.data = xml;
            parentPgs.data0 = id;
            xml = GenerateXML(parentPgs);
            return xml;
        }

        /// <summary>
        /// Serializa los objetos para generar el xml correspondiente
        /// </summary>
        /// <param name="payment"></param>
        /// <returns>string</returns>
        private string GenerateXML(Object payment)
        {
            System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(payment.GetType());
            TextWriter stringWriter = new StringWriter();
            x.Serialize(stringWriter, payment);
            string xml = stringWriter.ToString();
            xml = xml.Replace("<?xml version=\"1.0\" encoding=\"utf-16\"?>", "");
            xml = xml.Replace(" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"", "");
            return xml;

        }

        /// <summary>
        /// Valida si un string esta en B64
        /// </summary>
        /// <param name="base64"></param>
        /// <returns></returns>
        public static bool IsBase64String(string base64)
        {
            Span<byte> buffer = new Span<byte>(new byte[base64.Length]);
            return Convert.TryFromBase64String(base64, buffer, out int bytesParsed);
        }

        /// <summary>
        /// Descifra el resultado de pago y lo convierte a un objeto de tipo PaymentResponse
        /// </summary>
        /// <param name="bodyResponse"></param>
        /// <returns>PaymentResponse</returns>
        /// <exception cref="SdkException"></exception>
        public PaymentResponse ProcessAfterPaymentResponse(String bodyResponse)
        {
            bodyResponse = DecryptResponse(bodyResponse);
            PaymentResponse response = null;

//            var init = bodyResponse.IndexOf("<datos_adicionales>");
//            var finish = bodyResponse.IndexOf("</datos_adicionales>");

            //var stringBody = bodyResponse.Substring(0, init) + bodyResponse.Substring(finish + 20);
//            var stringBody = bodyResponse;
//            Console.WriteLine("La info es: " + bodyResponse);
            XmlSerializer serializer = new XmlSerializer(typeof(PaymentResponse));
            try
            {
                using (TextReader reader = new StringReader(bodyResponse))
                {
                    response = (PaymentResponse)serializer.Deserialize(reader);
                }
            }
            catch (Exception ex)
            {
                throw new SdkException("Error al interpretar el XML: " + bodyResponse);
            }

            return response;
        }

        /// <summary>
        ///  Descifra los mensajes enviados del servidor de pagos.
        /// </summary>
        /// <param name="bodyResponse"></param>
        /// <returns></returns>
        private string DecryptResponse(String bodyResponse)
        {
            if (bodyResponse.StartsWith("strResponse="))
            {
                bodyResponse = bodyResponse.Replace("strResponse=", "");
            }
            if (bodyResponse.Contains("%"))
            {
                bodyResponse = HttpUtility.UrlDecode(bodyResponse, Encoding.UTF8);
            }
            bodyResponse = aesHelper.Decrypt(bodyResponse);
//            bodyResponse = bodyResponse.Replace("\n","");
//            bodyResponse = bodyResponse.Replace(" ", "");
//            bodyResponse = bodyResponse.Replace("<cd_error/><nb_error/>", "");
            return bodyResponse;
        }

    }
}
