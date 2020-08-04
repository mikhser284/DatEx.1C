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
    /// <summary> Сотрудники организаций </summary>
    public partial class ClientOf1C
    {
        public List<Guid> GetIdsOfEmployees()
        {
            String query = $"Catalog_СотрудникиОрганизаций/?$select=Ref_Key{AsJson}";
            ODataIdentifiersResult oDataRes = GetAsData<ODataIdentifiersResult>(query);
            return oDataRes.Identifiers.Select(x => x.Id).ToList();
        }

        public List<Employee> GetEmployeesByIds(Guid identifier, params Guid[] identifiers)
        {
            List<Guid> ids = new List<Guid>(identifiers);
            ids.Add(identifier);
            return GetEmployeesByIds(ids);
        }

        public List<Employee> GetEmployeesByIds(IEnumerable<Guid> identifiers)
        {
            String filter = String.Join(" or \n", identifiers.Select(id => $"Ref_Key eq guid'{id}'"));
            String query = $"Catalog_СотрудникиОрганизаций/?$filter=\n{filter}{AsJson}";

            OneCODataResult<Employee> oDataRes = GetAsData<OneCODataResult<Employee>>(query);
            return oDataRes.Values;
        }

        public List<Employee> GetEmployeesLike(String partOfName)
        {
            String query = $"Catalog_СотрудникиОрганизаций/?$filter=substringof('{partOfName}', Description){AsJson}";

            OneCODataResult<Employee> oDataRes = GetAsData<OneCODataResult<Employee>>(query);
            return oDataRes.Values;
        }
    }

    /// <summary> Получение контрагентов </summary>
    public partial class ClientOf1C
    {
        public List<ContactInfo> GetContactInfo()
        {
            String query = $"InformationRegister_КонтактнаяИнформация/?$skip=0&$top=10{AsJson}";
            OneCODataResult<ContactInfo> oDataRes = GetAsData<OneCODataResult<ContactInfo>>(query);
            return oDataRes.Values;
        }
    }

    /// <summary> Получение контрагентов </summary>
    public partial class ClientOf1C
    {
        public List<Guid> GetIdsOfContractors()
        {
            String query = $"Catalog_Контрагенты/?$select=Ref_Key{AsJson}";
            ODataIdentifiersResult oDataRes = GetAsData<ODataIdentifiersResult>(query);
            return oDataRes.Identifiers.Select(x => x.Id).ToList();
        }

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

            OneCODataResult<Contractor> oDataRes = GetAsData<OneCODataResult<Contractor>>(query);
            return oDataRes.Values;
        }

        public List<Contractor> GetContracorsByIds(Guid identifier, params Guid[] identifiers)
        {
            List<Guid> ids = new List<Guid>(identifiers);
            ids.Add(identifier);
            return GetContracorsByIds(ids);
        }

        public List<Contractor> GetContracorsByIds(IEnumerable<Guid> identifiers)
        {
            String filter = String.Join(" or \n", identifiers.Select(id => $"Ref_Key eq guid'{id}'"));
            String query = $"Catalog_Контрагенты/?$filter=\n{filter}{AsJson}";

            OneCODataResult<Contractor> oDataRes = GetAsData<OneCODataResult<Contractor>>(query);
            return oDataRes.Values;
        }
    }


    public partial class ClientOf1C
    {
        public ClientOf1C(SettingsForClientOf1C settings)
        {
            HttpClient = GetConfiguredClient(settings);
        }

        private readonly HttpClient HttpClient;
        private const String AsJson = "&$format=application/json";

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
