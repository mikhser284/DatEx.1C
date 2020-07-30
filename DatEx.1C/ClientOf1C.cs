using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using DatEx._1C.DataModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DatEx._1C
{
    public partial class ClientOf1C
    {
        public ClientOf1C(SettingsForClientOf1C settings)
        {
            HttpClient = GetConfiguredClient(settings);
        }

        public List<Contractor> GetContractors(Int32 skip, Int32 top)
        {   
            ODataResult<Contractor> oDataRes = GetAsData<ODataResult<Contractor>>($"Catalog_Контрагенты/?$top={top}&$skip={skip}{AsJson}");
            return oDataRes.Values;
        }
    }

    public partial class ClientOf1C
    {
        private readonly HttpClient HttpClient;
        private const String AsJson = "&$format=application/json";

        private HttpClient GetConfiguredClient(SettingsForClientOf1C settings)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(settings.ServiceBaseAddress);
            httpClient.DefaultRequestHeaders.Accept.Clear();

            String authentificationInfo = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{settings.AgentLogin}:{GetMD5Hash(settings.AgentPassword)}"));
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authentificationInfo);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return httpClient;

            static String GetMD5Hash(String sourceString)
            {
                if(String.IsNullOrEmpty(sourceString)) return String.Empty;
                using(System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
                {
                    byte[] inputBytes = Encoding.ASCII.GetBytes(sourceString);
                    byte[] hashBytes = md5.ComputeHash(inputBytes);

                    // Convert the byte array to hexadecimal string
                    StringBuilder sb = new StringBuilder();
                    for(int i = 0; i < hashBytes.Length; i++)
                        sb.Append(hashBytes[i].ToString("X2"));
                    return sb.ToString();
                }
            }
        }


        private String GetAsJson(String query)
        {
            HttpResponseMessage response = HttpClient.GetAsync(query).Result;
            response.EnsureSuccessStatusCode();
            String result = response.Content.ReadAsStringAsync().Result;
#if DEBUG
            result = JToken.Parse(result).ToString(Formatting.Indented);
#endif
            return result;
        }

        private T GetAsData<T>(String query)
        {
            HttpResponseMessage response = HttpClient.GetAsync(query).Result;
            
            response.EnsureSuccessStatusCode();
            String result = response.Content.ReadAsStringAsync().Result;
#if DEBUG
            result = JToken.Parse(result).ToString(Formatting.Indented);
#endif
            return JsonConvert.DeserializeObject<T>(result);
        }
    }
}
