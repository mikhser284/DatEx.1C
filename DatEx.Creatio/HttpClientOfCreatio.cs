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
    public partial class HttpClientOfCreatio
    {
        #region ————— Общие методы ——————————————————————————————————————————————————————————————————————————————————————————————————————————————————

        /// <summary> Получить одиночный объект по его идентификатору </summary>
        public T GetSingleObjById<T>(Guid id) where T : BaseEntity
        {
            String fullQueryString = $"{OdataUri}{typeof(T).Name}({id})";
            String requestResult = GetRequest(fullQueryString);
            CreatioOdataRequestResult<T> result = JsonConvert.DeserializeObject<CreatioOdataRequestResult<T>>(requestResult);
            T res = JsonConvert.DeserializeObject<T>(requestResult);
            return res;
        }

        /// <summary> ■ Получить объекты упорядоченные по их Id по их идентификаторам </summary>
        public Dictionary<Guid, T> GetObjsByIds<T>(IEnumerable<Guid> identifiers, String nameOfLinkedObject = default(String)) where T : BaseEntity
        {
            Dictionary<Guid, T> result = new Dictionary<Guid, T>();
            if (identifiers.Count() == 0) return result;
            String linkedObj = $"{nameOfLinkedObject}{(String.IsNullOrEmpty(nameOfLinkedObject) ? "" : "/")}Id";
            String filter = String.Join(" or ", identifiers.Select(id => $"{linkedObj} eq {id} "));
            String query = $"filter={filter}";
            result = GetObjs<T>(query).ToDictionary(k => (Guid)k.Id);
            return result;
        }

        /// <summary> Получить объекты упорядоченные по их Id по их идентификаторам </summary>
        public Dictionary<Guid, T> GetObjsByIds<T>(Guid id, params Guid[] ids) where T : BaseEntity
        {
            HashSet<Guid> identifiers = new HashSet<Guid>(ids);
            identifiers.Add(id);
            return GetObjsByIds<T>(identifiers);
        }

        /// <summary> Получить связанные объекты по идентификаторам дочерних </summary>
        public Dictionary<Guid, List<T>> GetBindedObjsByParentIds<T>(IEnumerable<Guid> identifiers, String nameOfParentObj, Func<T, Guid?> parentKeySelector) where T : BaseEntity
        {
            Guid defaultGuid = default(Guid);
            Dictionary<Guid, List<T>> bindedObjsGroupedByParentId = new Dictionary<Guid, List<T>>();
            if (identifiers.Count() == 0) return bindedObjsGroupedByParentId;
            String linkedObj = $"{nameOfParentObj}{(String.IsNullOrEmpty(nameOfParentObj) ? "" : "/")}Id";
            String filter = String.Join(" or ", identifiers.Select(id => $"{linkedObj} eq {id} "));
            String query = $"filter={filter}";
            List<T> queryResult = GetObjs<T>(query);
            foreach (T item in queryResult)
            {
                if (parentKeySelector(item) is null) continue;
                Guid parentKey = (Guid)parentKeySelector(item);
                if (parentKey == defaultGuid) continue;
                if (bindedObjsGroupedByParentId.ContainsKey(parentKey)) bindedObjsGroupedByParentId[parentKey].Add(item);
                else bindedObjsGroupedByParentId.Add(parentKey, new List<T> { item });
            }
            return bindedObjsGroupedByParentId;
        }        

        /// <summary> Получить объекты указанного типа </summary>
        public List<T> GetObjs<T>(String query = default(String)) where T : BaseEntity
        {
            String fullQueryString = $"{OdataUri}{typeof(T).Name}/?${query}";
            String requestResult = GetRequest(fullQueryString);
            CreatioOdataRequestResult<T> result = JsonConvert.DeserializeObject<CreatioOdataRequestResult<T>>(requestResult);
            //if (result.Values.Count == 0)
            //{
            //    T singleValue = JsonConvert.DeserializeObject<T>(requestResult);
            //    return new List<T> { singleValue };
            //}

            return result.Values;
        }

        /// <summary> Получить проекцию объектов указанного типа </summary>
        public List<ProjectionType> GetObjsProjection<T, ProjectionType>(String query = default(String)) where T : BaseEntity
        {
            CreatioOdataRequestResult<ProjectionType> oDataRequestResult = GetRequestResult<CreatioOdataRequestResult<ProjectionType>>($"{OdataUri}{typeof(T).Name}{query}");
            return oDataRequestResult.Values;
        }

        /// <summary> Получить объекты указанного типа у которых значение указанного свойства входить в список значений </summary>
        public List<T> GetObjsWherePropIn<T, P>(String propName, IEnumerable<P> values) where T : BaseEntity
        {
            String filter = String.Join(" or \n", values.Select(p => $"{propName} eq '{p}'"));

            String fullQueryString = $"{OdataUri}{typeof(T).Name}/?$filter={filter}";
            String requestResult = GetRequest(fullQueryString);
            CreatioOdataRequestResult<T> result = JsonConvert.DeserializeObject<CreatioOdataRequestResult<T>>(requestResult);
            //if (result.Values.Count == 0)
            //{
            //    T singleValue = JsonConvert.DeserializeObject<T>(requestResult);
            //    result.Values = new List<T> { singleValue };
            //}

            return result.Values;
        }


        /// <summary> Добавить объект </summary>
        public T CreateObj<T>(T obj) where T : BaseEntity
        {
            String serializedEntity = JsonConvert.SerializeObject(obj,
                new JsonSerializerSettings
                {
                    ContractResolver = new JsonPropertiesResolver(),
                    NullValueHandling = NullValueHandling.Ignore,
                    Formatting = Formatting.Indented
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

        /// <summary> Модифицировать объект </summary>
        public void UpdateObj<T>(T obj) where T : BaseEntity
        {
            if (obj.Id == null) throw new InvalidOperationException("Не указан идентификатор изменяемого объекта");

            String serializedEntity = JsonConvert.SerializeObject(obj,
                new JsonSerializerSettings
                {
                    ContractResolver = new JsonPropertiesResolver(),
                    NullValueHandling = NullValueHandling.Ignore
                });
#if DEBUG
            String entityForDebug = JsonConvert.SerializeObject(obj,
                new JsonSerializerSettings
                {
                    ContractResolver = new JsonPropertiesResolver(),
                    NullValueHandling = NullValueHandling.Ignore,
                    Formatting = Formatting.Indented
                });
            //serializedEntity = "{\"Notes\": \"*****\"}";
#endif


            StringContent content = new StringContent(serializedEntity, Encoding.UTF8, "application/json");
            Uri odataUri = new Uri(OdataUri, $"{typeof(T).Name}({obj.Id})");
            HttpResponseMessage response = CreatioClient.PatchAsync(odataUri, content).Result;
            response.EnsureSuccessStatusCode();
        }

        /// <summary> Удалить объект </summary>
        public void DeleteObj<T>(T obj) where T : BaseEntity
        {
            if (obj.Id == null) throw new InvalidOperationException("Не указан идентификатор изменяемого объекта");
            Uri odataUri = new Uri(OdataUri, $"{typeof(T).Name}({obj.Id})");
            HttpResponseMessage response = CreatioClient.DeleteAsync(odataUri).Result;
            response.EnsureSuccessStatusCode();
        }

        #endregion ————— Общие методы



        #region ————— Служебные —————————————————————————————————————————————————————————————————————————————————————————————————————————————————————

        private readonly Uri BaseUri;
        private readonly Uri AuthServiceUri;
        private readonly Uri OdataUri;
        private HttpClient CreatioClient;
        
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
        
        /// <summary> Залогиниться в Creatio </summary>
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
        private T GetRequestResult<T>(String query) => JsonConvert.DeserializeObject<T>(GetRequest(query));

        #endregion ————— Служебные



        #region ————— Временные (удалить) ———————————————————————————————————————————————————————————————————————————————————————————————————————————
        
        public String GetODataRequestResult(String dataSource, String query) => GetRequest($"{OdataUri}{dataSource}{query}");

        #endregion ————— Временные (удалить)
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
