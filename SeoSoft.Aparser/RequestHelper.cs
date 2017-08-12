using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
namespace GParser
{
    public class RequestHelper
    {
        private string _password;
        private string _host;

        public RequestHelper(string password, string host)
        {
            _password = password;
            _host =  host;
        }

        public async Task<dynamic> MakeRequestAsync(string action)
        {
            dynamic data = new {};

            dynamic result = await MakeRequestAsync(action, data);

            return result;
        }

        public async Task<dynamic> MakeRequestAsync(string action, dynamic data)
        {

            var request = new { action = action, password = _password, data = data };

            string requestJson = JsonConvert.SerializeObject(request);

            var client = new HttpClient();
            client.Timeout = TimeSpan.FromMinutes(30);
            var stringContent = new StringContent(requestJson, Encoding.UTF8, "application/json");

            // Get the response.
            HttpResponseMessage response = await client.PostAsync(
                _host,
                stringContent);

            // Get the response content.
            string responseContent = await response.Content.ReadAsStringAsync();

            dynamic result = JsonConvert.DeserializeObject(responseContent);

            Type objType = result.GetType();

            if (result.success != 1) {
                throw new Exception(String.Format("Response fail: {0}", result.msg));
            }

            return result;

        }

    }
}
