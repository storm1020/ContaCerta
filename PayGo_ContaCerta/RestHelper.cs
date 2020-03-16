using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace PayGo_ContaCerta
{
    public static class RestHelper
    {
        private static readonly string baseURL = "https://login-api.transfeera.com/";
        private static readonly string baseURLContaCerta = "https://contacerta-api.transfeera.com/";
        private static string keyAutorization;

        private static void SetKeyAutorization(string key)
        {
            string[] splt = key.Split('"');
            string keyTratada = splt[3].ToString().Trim();

            keyAutorization = keyTratada;
        }

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
                            SetKeyAutorization(data);
                            return data;
                        }
                    }
                }
            }

            return string.Empty;
        }

        // Realizar teste microdeposito
        public static async Task<string> Post_MicroDeposito(string name, string cpfCnpj, string bankCode, string agc, string agcDigit,
            string acct, string acctDigit, string acctType, string itgId)
        {
            var inputData = new Dictionary<string, string>
            {
                {"name", name},
                {"cpf_cnpj", cpfCnpj},
                {"bank_code", bankCode},
                {"agency", agc},
                {"agency_digit", agcDigit},
                {"account", acct},
                {"account_digit", acctDigit},
                {"account_type", acctType},
                {"integration_id", itgId}
            };

            var input = new FormUrlEncodedContent(inputData);
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseURLContaCerta);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
                //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjhmOGY3OTNmLTBkZjEtNGEwNy05OGQ5LTY0ZThkNWQ0ZjgxMyIsIm5hbWUiOiJDb250YUNlcnRhIENvbnRhQ2VydGEgUGF5R28iLCJlbWFpbCI6ImFydGh1ci5uYXNjaW1lbnRvQHBheWdvLmNvbS5iciIsInN0YXR1cyI6IkhBQklMSVRBRE8iLCJoYXNfdHdvX2ZhY3Rvcl9lbmFibGVkIjpmYWxzZSwicHJvZmlsZSI6IkFETUlOIiwiY29tcGFueSI6eyJpZCI6ImU3ZTgyZjNkLWQ5ZTgtNDgxNy1iNWExLTRmZWJkMTVhNjQ3YSIsIm5hbWUiOiJDb250YUNlcnRhIENvbnRhQ2VydGEgUGF5R28iLCJwaG9uZSI6IiAgICAgICAgICAgIiwiY3BmQ25waiI6IjY2MjEyNTI0MDAwMTU0IiwibG9nbyI6IiIsImFjY2VwdGVkQ29udHJhY3QiOnRydWUsInN0YXR1cyI6IkFDVElWRSIsImNyZWF0ZWRBdCI6IjIwMTktMTEtMTFUMTQ6NTM6NDMuMTc5NzYxLTAzOjAwIn0sImlhdCI6MTU4Mjc0NzgyMywiZXhwIjoxNTgyNzQ5NjIzfQ.gfAYp7Sz2UNQOrXVZrqeGj3RkS6d9zhF3hrTiRYRd7U");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(keyAutorization);

                using (HttpResponseMessage res = await client.PostAsync(baseURLContaCerta + "validate?type=MICRO_DEPOSITO", input))
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

        public static async Task<string> Post_MultContaBasica(string name, string cpfCnpj, string bankCode, string agc, string agcDigit,
            string acct, string acctDigit, string acctType, string itgId)
        {
            var inputData = new Dictionary<string, string>
            {
                {"name", name},
                {"cpf_cnpj", cpfCnpj},
                {"bank_code", bankCode},
                {"agency", agc},
                {"agency_digit", agcDigit},
                {"account", acct},
                {"account_digit", acctDigit},
                {"account_type", acctType},
                {"integration_id", itgId}
            };

            //Formata dados de acordo com necessidade de envio.
            var input = new FormUrlEncodedContent(inputData);

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseURLContaCerta);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(keyAutorization);

                using (HttpResponseMessage res = await client.PostAsync(baseURLContaCerta + "validate?type=BASICA", input))
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