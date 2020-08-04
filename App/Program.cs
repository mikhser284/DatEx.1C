using System;
using System.Collections.Generic;
using System.Linq;
using DatEx.OneC;
using DatEx.OneC.DataModel;
using DatEx.Creatio;
using DatEx.Creatio.DataModel.Terrasoft.Base;
using ITIS = DatEx.Creatio.DataModel.ITIS;

namespace App
{
    class Program
    {
        private static ClientOfCreatio CreatioHttpClient = ClientOfCreatio.LogIn("http://185.59.101.152:50080/", "Supervisor", "Supervisor");
        private static SettingsForClientOf1C settings = new SettingsForClientOf1C("http://185.59.101.152:50081/Dev03_1C/odata/standard.odata/", "Администратор", "");
        public static ClientOf1C OneCHttpClient = new ClientOf1C(settings);



        static void Main(string[] args)
        {
            //GetContractorsByIdentifiers();
            //GetContractorsByIdentifier();
            //GetContractorsByCodesOfEdrpo();
            //GetContractorByCodesOfEdrpo();
            //ShowContractors();
            //ShowEmployees();
            //GetGetContactInfo();

            CreatioGetEmployees();
        }

        public static void CreatioGetEmployees()
        {
            List<ITIS.Employee> contacts = CreatioHttpClient.ODataGet<ITIS.Employee>();
            ITIS.Employee mZorin = contacts.FirstOrDefault(x => x.ITISSurName.Contains("Зорін"));
        }

        public static void ShowEmployees()
        {
            List<Guid> ids = OneCHttpClient.GetIdsOfEmployees();

            ConsoleKeyInfo input;
            Int32 index = 0;
            Int32 count = 1;
            do
            {
                Console.Clear();
                Console.WriteLine($"Index: {index,4}; Count: {count,4}\n\n");
                List<Guid> idsPage = ids.GetRange(index, count);
                //ClientOf1C.GetEmployeesByIds(idsPage).ShowOneCObjects();
                OneCHttpClient.GetEmployeesByIds(new Guid("f9e7b11f-609a-11e7-80cb-00155d65b717")).ShowOneCObjects();
                //ClientOf1C.GetEmployeesLike("Зорін").ShowOneCObjects();
                input = Console.ReadKey();
                index += count;
            } while(input.Key != ConsoleKey.Escape);
        }

        public static void ShowContractors()
        {
            List<Guid> ids = OneCHttpClient.GetIdsOfContractors();

            ConsoleKeyInfo input;
            Int32 index = 0;
            Int32 count = 1;
            do
            {
                Console.Clear();
                Console.WriteLine($"Index: {index,4}; Count: {count,4}\n\n");
                List<Guid> idsPage = ids.GetRange(index, count);
                OneCHttpClient.GetContracorsByIds(idsPage).ShowOneCObjects();
                input = Console.ReadKey();
                index += count;
            } while(input.Key != ConsoleKey.Escape);
        }

        public static void GetGetContactInfo()
        {
            OneCHttpClient.GetContactInfo().ShowOneCObjects();
        }

        public static void GetContractorByCodesOfEdrpo()
        {
            OneCHttpClient.GetContractorsByCodeOfEdrpo("40623794").ShowOneCObjects();
        }

        public static void GetContractorsByCodesOfEdrpo()
        {
            List<String> codesOfEdrpo = new List<string>
            {
                "40623794",
                "37652846",
                "39137820",
                "37630010",
                "40792278"
            };
            OneCHttpClient.GetContractorsByCodeOfEdrpo(codesOfEdrpo).ShowOneCObjects();
        }

        public static void GetContractorsByIdentifier()
        {
            OneCHttpClient.GetContracorsByIds(new Guid("848b5acf-83ed-11e6-80ba-00155d65b717")).ShowOneCObjects();
        }

        public static void GetContractorsByIdentifiers()
        {
            List<Guid> identifiers = new List<Guid>
            {
                new Guid("848b5acf-83ed-11e6-80ba-00155d65b717"),
                new Guid("059ee0c6-7ff2-11e6-80ba-00155d65b717"),
                new Guid("ffe137d9-7018-11e6-80b9-00155d65b717"),
                new Guid("3c2d8c7f-7670-11e6-80ba-00155d65b717"),
                new Guid("86569aec-7f0d-11e6-80ba-00155d65b717"),
            };
            OneCHttpClient.GetContracorsByIds(identifiers).ShowOneCObjects();
        }

        // ————————————————————————————————————————————————————————————————————————————————————————————————————

        public static void SyncEmployes(String employeeEmail)
        {
            if (String.IsNullOrEmpty(employeeEmail) || !employeeEmail.EndsWith("@kustoagro.com")) return;
        }

        public static void SyncContractor(String codeOfEdrpo)
        {

        }

    }

    public static class Ext_OneCObject
    {
        public static void ShowOneCObjects<T>(this IEnumerable<T> objects) where T : OneCBase
        {
            foreach(T obj in objects)
            {
                obj.Show();
                Console.WriteLine($"\n\n");
            }
        }
    }
}
