using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using DatEx.OneC.DataModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DatEx.OneC
{   
    /// <summary> Получение контрагентов </summary>
    public partial class ClientOfOneC
    {
        #region Общие методы

        public List<T> GetObjs<T>(String query = default(String)) where T : OneCObject => GetObjs<T, T>(query);

        public List<R> GetObjs<T, R>(String query = default(String)) where T : OneCObject
        {
            String typeName = TypesMap[typeof(T)];
            String separator = !String.IsNullOrEmpty(query) ? "&" : "";
            String fullQuery = $"{typeName}/?{query}{separator}{AsJson}";
            HttpResponseMessage response = HttpClient.GetAsync(fullQuery).Result;
            response.EnsureSuccessStatusCode();
            String result = response.Content.ReadAsStringAsync().Result;
#if DEBUG
            result = JToken.Parse(result).ToString(Formatting.Indented);
#endif
            return JsonConvert.DeserializeObject<OneCODataResult<R>>(result).Values;
        }
        

        public List<Guid> GetIdsOfObjs<T>(String query = null, String nameOfGuidFieldToSelect = "Ref_Key") where T : OneCObject
        {
            String typeName = TypesMap[typeof(T)];
            String separator = !String.IsNullOrEmpty(query) ? "&" : "";
            String fullQuery = $"{typeName}/?{query}{separator}$select={nameOfGuidFieldToSelect}&{AsJson}";
            HttpResponseMessage response = HttpClient.GetAsync(fullQuery).Result;
            response.EnsureSuccessStatusCode();
            String result = response.Content.ReadAsStringAsync().Result;
#if DEBUG
            result = JToken.Parse(result).ToString(Formatting.Indented);
#endif
            //return JsonConvert.DeserializeObject<ODataIdentifiersResult>(result).Identifiers.Select(x => x.Id).ToList();
            return JObject.Parse(result)["value"].Select(i => i[nameOfGuidFieldToSelect]).Select(x => new Guid(x.Value<String>())).ToList();
        }

        public List<T> GetObjsByIds<T>(IEnumerable<Guid> identifiers, String nameOfGuidFieldToCompare = "Ref_Key") where T : OneCObject
        {
            String filter = String.Join(" or \n", identifiers.Select(id => $"{nameOfGuidFieldToCompare} eq guid'{id}'"));
            String query = $"$filter={filter}";
            return GetObjs<T>(query);
        }

        public List<T> GetObjsByIds<T>(Guid identifier, params Guid[] identifiers) where T : OneCObject
        {
            List<Guid> ids = new List<Guid>(identifiers);
            ids.Add(identifier);
            return GetObjsByIds<T>(ids);
        }

        #endregion Общие методы

        #region Получение контрагентов

        public List<Contractor> GetContractorsByCodeOfEdrpo(String codeOfEdrpo, params String[] codesOfEdrpo)
        {
            List<String> edrpoCodes = new List<string>(codesOfEdrpo);
            edrpoCodes.Add(codeOfEdrpo);
            return GetContractorsByCodeOfEdrpo(edrpoCodes);
        }

        public List<Contractor> GetContractorsByCodeOfEdrpo(IEnumerable<String> codesOfEdrpo)
        {
            String filter = String.Join(" or \n", codesOfEdrpo.Select(id => $"КодПоЕДРПОУ eq '{id}'"));
            String query = $"Catalog_Контрагенты/?$filter=\n{filter}{AsJson}";
            return GetObjs<Contractor>(query);
        }

        #endregion Получение контрагентов

        #region Служебные

        public ClientOfOneC(SettingsForClientOf1C settings)
        {
            HttpClient = GetConfiguredClient(settings);
        }

        private readonly HttpClient HttpClient;
        private const String AsJson = "$format=application/json";

        private HttpClient GetConfiguredClient(SettingsForClientOf1C settings)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(settings.ServiceBaseAddress);
            httpClient.DefaultRequestHeaders.Accept.Clear();

            String authentificationInfo = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{settings.AgentLogin}:{settings.AgentPassword}"));
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authentificationInfo);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return httpClient;
        }

        private static readonly Dictionary<Type, String> TypesMap = new Dictionary<Type, String>
        {
            { typeof(Contractor), "Catalog_Контрагенты" },
            { typeof(Employee), "Catalog_СотрудникиОрганизаций" },
            { typeof(Person), "Catalog_ФизическиеЛица" },
            { typeof(IRNamesOfPersons), "InformationRegister_ФИОФизЛиц" },
            { typeof(IRContactInfo), "InformationRegister_КонтактнаяИнформация" },
            { typeof(ContactInfoType), "Catalog_ВидыКонтактнойИнформации" }
        };

        #endregion Служебные
    }
}
