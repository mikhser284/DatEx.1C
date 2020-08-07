using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using DatEx.Creatio.DataModel;
using DatEx.Creatio.DataModel.Terrasoft.Base;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DatEx.Creatio
{
    public class HttpClientOfCreatio
    {
        private readonly Uri BaseUri;
        private readonly Uri AuthServiceUri;
        private readonly Uri OdataUri;
        private HttpClient CreatioClient;
        //private Dictionary<String, Cookie> Cookies { get; set; } = new Dictionary<string, Cookie>();

        private HttpClientOfCreatio(String baseUri)
        {
            BaseUri = new Uri(baseUri);
            AuthServiceUri = new Uri(BaseUri, "ServiceModel/AuthService.svc/Login");
            OdataUri = new Uri(BaseUri, "0/odata/");
        }

        private static HttpClientOfCreatio GetConfiguredClient(String baseAddress, HttpClientHandler handler = null)
        {
            HttpClientOfCreatio client = new HttpClientOfCreatio(baseAddress);
            HttpClient httpClient = handler is null ? new HttpClient() : new HttpClient(handler);
            httpClient.BaseAddress = client.BaseUri;
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.CreatioClient = httpClient;
            return client;
        }

        public static HttpClientOfCreatio LogIn(String baseAddress, String userName, String userPassword)
        {
            AuthResponse loginStatus;
            HttpClientHandler handler = new HttpClientHandler() { CookieContainer = new CookieContainer() };
            HttpClientOfCreatio creatioClient = GetConfiguredClient(baseAddress, handler);
            HttpClient client = creatioClient.CreatioClient;
            String authData = JsonConvert.SerializeObject(new AuthRequest(userName, userPassword));
            StringContent content = new StringContent(authData, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(creatioClient.AuthServiceUri, content).Result;
            response.EnsureSuccessStatusCode();
            String result = response.Content.ReadAsStringAsync().Result;
#if DEBUG
            result = JToken.Parse(result).ToString(Formatting.Indented);
#endif
            loginStatus = JsonConvert.DeserializeObject<AuthResponse>(result);
            // TODO Throw exception if not authorized
            if(loginStatus.Code != 0) throw new InvalidOperationException("Не удалось авторизоваться");
            Dictionary<String, Cookie> cookies = handler.CookieContainer.GetCookies(creatioClient.AuthServiceUri).Cast<Cookie>().ToDictionary(k => k.Name);
            creatioClient.CreatioClient.DefaultRequestHeaders.Add("ForceUseSession", "true");
            creatioClient.CreatioClient.DefaultRequestHeaders.Add("BPMCSRF", cookies["BPMCSRF"].Value);
            return creatioClient;
        }

        private String GetRequest(String query)
        {
            HttpResponseMessage response = CreatioClient.GetAsync(query).Result;
            response.EnsureSuccessStatusCode();
            String result = response.Content.ReadAsStringAsync().Result;
#if DEBUG
            result = JToken.Parse(result).ToString(Formatting.Indented);
#endif
            return result;
        }

        private T GetRequest<T>(String query) => JsonConvert.DeserializeObject<T>(GetRequest(query));

        public String GetContactIdByName(String name) => GetRequest($"0/rest/UsrCustomConfigurationService/GetContactIdByName?Name={name}");

        public String GetOData(String dataSource, String query) => GetRequest($"{OdataUri}{dataSource}{query}");

        public List<T> ODataGet<T>(String query = default(String))
        {
            String fullQueryString = $"{OdataUri}{typeof(T).Name}/?{query}";
            String requestResult = GetRequest(fullQueryString);
            CreatioOdataRequestResult<T> result = JsonConvert.DeserializeObject<CreatioOdataRequestResult<T>>(requestResult);
            if(result.Values.Count == 0)
            {
                T singleValue = JsonConvert.DeserializeObject<T>(requestResult);
                return new List<T> { singleValue };
            }

            return result.Values;
        }

        public List<TB> ODataGet<TA, TB>(String query = default(String))
        {
            CreatioOdataRequestResult<TB> oDataRequestResult = GetRequest<CreatioOdataRequestResult<TB>>($"{OdataUri}{typeof(TA).Name}{query}");
            return oDataRequestResult.Values;
        }

        public List<T> ODataGet<T, P>()
        {
            String fullQueryString = $"{OdataUri}{typeof(T).Name}/?{query}";
            String requestResult = GetRequest(fullQueryString);
            CreatioOdataRequestResult<T> result = JsonConvert.DeserializeObject<CreatioOdataRequestResult<T>>(requestResult);
            if(result.Values.Count == 0)
            {
                T singleValue = JsonConvert.DeserializeObject<T>(requestResult);
                return new List<T> { singleValue };
            }

            return result.Values;
        }

        public T ODataAdd<T>(T entity) where T : BaseEntity
        {
            String serializedEntity = JsonConvert.SerializeObject(entity,
                new JsonSerializerSettings
                {
                    ContractResolver = new JsonPropertiesResolver(),
                    NullValueHandling = NullValueHandling.Ignore,
                    //Formatting = Formatting.Indented
                });

            StringContent content = new StringContent(serializedEntity, Encoding.UTF8, "application/json");
            Uri odataUri = new Uri(OdataUri, typeof(T).Name);
            HttpResponseMessage response = CreatioClient.PostAsync(odataUri, content).Result;
            response.EnsureSuccessStatusCode();
            String result = response.Content.ReadAsStringAsync().Result;
#if DEBUG
            result = JToken.Parse(result).ToString(Formatting.Indented);
#endif
            T operationResult = JsonConvert.DeserializeObject<T>(result);
            return operationResult;
        }

        public void ODataModify<T>(T entity) where T : BaseEntity
        {
            if(entity.Id == null) throw new InvalidOperationException("Не указан идентификатор изменяемого объекта");

            String serializedEntity = JsonConvert.SerializeObject(entity,
                new JsonSerializerSettings
                {
                    ContractResolver = new JsonPropertiesResolver(),
                    NullValueHandling = NullValueHandling.Ignore
                });

            StringContent content = new StringContent(serializedEntity, Encoding.UTF8, "application/json");
            Uri odataUri = new Uri(OdataUri, $"{typeof(T).Name}({entity.Id})");
            HttpResponseMessage response = CreatioClient.PatchAsync(odataUri, content).Result;
            response.EnsureSuccessStatusCode();
        }

        public void ODataDelete<T>(T entity) where T : BaseEntity
        {
            if(entity.Id == null) throw new InvalidOperationException("Не указан идентификатор изменяемого объекта");
            Uri odataUri = new Uri(OdataUri, $"{typeof(T).Name}({entity.Id})");
            HttpResponseMessage response = CreatioClient.DeleteAsync(odataUri).Result;
            response.EnsureSuccessStatusCode();
        }


    }


    public class AuthRequest
    {
        public String UserName { get; set; }
        public String UserPassword { get; set; }

        public AuthRequest(String userName, String userPassword)
        {
            UserName = userName;
            UserPassword = userPassword;
        }
    }


    public class AuthResponse
    {
        public Int32 Code { get; set; }
        public String Message { get; set; }
        public Object Exception { get; set; }
        public Object PasswordChangeUrl { get; set; }
        public Object RedirectUrl { get; set; }
    }
}
