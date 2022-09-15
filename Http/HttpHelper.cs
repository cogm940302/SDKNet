using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using SdkNet.ExceptionHandler;

namespace SdkNet.Http
{
    internal class HttpHelper
    {
        private string endpoint;
        private HttpClient client;

        /// <summary>
        /// Genera una instancia para realizar consumos http
        /// </summary>
        /// <param name="endpoint"></param>
        public HttpHelper(string endpoint) { 
            this.endpoint = endpoint;
            client = new HttpClient();
        }
        /// <summary>
        /// Implementacion para metodo POST
        /// </summary>
        /// <param name="message"></param>
        /// <returns>Task<string></returns>
        /// <exception cref="SdkException"></exception>
        public async Task<string> Post(string message)
        {
            try
            {
                var nvc = new List<KeyValuePair<string, string>>();
                nvc.Add(new KeyValuePair<string, string>("xml", message));
                var client = new HttpClient();
                var req = new HttpRequestMessage(HttpMethod.Post, endpoint) { Content = new FormUrlEncodedContent(nvc) };
                var res = await client.SendAsync(req);
                var content = await res.Content.ReadAsStringAsync();
                return content;
            }
            catch (Exception ex) {
                throw new SdkException("Error en los datos de envio: " + ex.Message);
            }
        }

    }
}
