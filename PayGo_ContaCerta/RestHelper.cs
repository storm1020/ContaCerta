using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PayGo_ContaCerta
{
    public static class RestHelper
    { 
        private static readonly string baseURL = "https://login-api-sandbox.transfeera.com/";

        public static async Task<string> Post_Authentication(string gt, string ci, string cs)
        {
            var inputData = new Dictionary<string, string>
            {
                {"grant_type", gt},
                {"client_id", ci},
                {"client_secret", cs}
            };

            var input = new FormUrlEncodedContent(inputData);

            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage res = await client.PostAsync(baseURL + "authorization", input))
                {
                    using (HttpContent content = res.Content)
                    {
                        string data = await content.ReadAsStringAsync();
                        if (data != null)
                        {
                            return data;
                        }
                    }
                }
            }
            return string.Empty;
        }

        public static string BeautifyJson(string jsonStr)
        {
            JToken parseJson = JToken.Parse(jsonStr);
            return parseJson.ToString(Formatting.Indented);
        }
    }
}