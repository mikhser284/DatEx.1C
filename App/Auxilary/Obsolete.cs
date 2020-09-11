using System;
using System.Collections.Generic;
using System.Linq;
using DatEx.OneS;
using OneC = DatEx.OneS.DataModel;
using DatEx.Creatio;
using ITIS = DatEx.Creatio.DataModel.ITIS;
using System.Security.Cryptography.X509Certificates;
using Newtonsoft.Json.Linq;
using Terrasoft = DatEx.Creatio.DataModel.Terrasoft.Base;
using System.Collections.Concurrent;

namespace App
{
    public partial class Program
    {
        public static void CreatioGetEmployees()
        {
            List<ITIS.Employee> contacts = HttpClientOfCreatio.GetObjs<ITIS.Employee>();
            ITIS.Employee mZorin = contacts.FirstOrDefault(x => x.ITISSurName.Contains("Зорін"));
        }

        public static void ShowIRContactInfo()
        {
            Console.WriteLine("————— #1 ————————————————————");
            HttpClientOfOneS.GetObjs<OneC.IRContactInfo>("$filter=cast(Объект, 'Catalog_ФизическиеЛица') eq guid'23d75280-5b29-11e7-80cb-00155d65b717'")
                .ShowOneCObjects();

            Console.WriteLine("————— #1a ————————————————————");
            HttpClientOfOneS.GetObjs<OneC.IRContactInfo>("$filter=Объект_Type eq 'StandardODATA.Catalog_ФизическиеЛица' and cast(Вид, 'Catalog_ВидыКонтактнойИнформации') eq guid'6b1ae98e-bb91-11ea-80c7-00155d65b747'")
                .ShowOneCObjects();

            Console.WriteLine("————— #2 ————————————————————");
            HttpClientOfOneS.GetObjs<OneC.ContactInfoType>("$filter=Ref_Key eq guid'08188400-bb94-11ea-80c7-00155d65b747'").ShowOneCObjects();
            HttpClientOfOneS.GetObjs<OneC.ContactInfoType>("$filter=Ref_Key eq guid'f1862c22-bb94-11ea-80c7-00155d65b747'").ShowOneCObjects();
            HttpClientOfOneS.GetObjs<OneC.ContactInfoType>("$filter=Ref_Key eq guid'6b1ae98e-bb91-11ea-80c7-00155d65b747'").ShowOneCObjects();

            Console.WriteLine("————— #3 ————————————————————");
            HttpClientOfOneS.GetObjs<OneC.Person>("$top=20&$filter=Ref_Key eq guid'23d75280-5b29-11e7-80cb-00155d65b717'")
                .ShowOneCObjects();

            Console.WriteLine("————— #4 ————————————————————");
            //OneCHttpClient.GetObjs<OneC.IRNamesOfPersons>("$top=20&$filter=ФизЛицо_Type eq 'StandardODATA.Catalog_ФизическиеЛица' and ФизЛицо like '23d75280-5b29-11e7-80cb-00155d65b717'")
            HttpClientOfOneS.GetObjs<OneC.IRNamesOfPersons>("$top=20&$filter=cast(ФизЛицо, 'Catalog_ФизическиеЛица') eq guid'23d75280-5b29-11e7-80cb-00155d65b717'")
                .ShowOneCObjects();

            Console.WriteLine("————— #5 ————————————————————");
            HttpClientOfOneS.GetObjs<OneC.Employee>("$top=20&$filter=Физлицо_Key eq guid'23d75280-5b29-11e7-80cb-00155d65b717'")
                .ShowOneCObjects();



            //List<OneC.IRContactInfo> objs = OneCHttpClient.GetObjs<OneC.IRContactInfo>("$top=20");            
        }



        public static void ShowEmployees()
        {
            List<Guid> ids = HttpClientOfOneS.GetIdsOfObjs<OneC.Employee>();

            ConsoleKeyInfo input;
            Int32 index = 0;
            Int32 count = 1;
            do
            {
                Console.Clear();
                Console.WriteLine($"Index: {index,4}; Count: {count,4}\n\n");
                List<Guid> idsPage = ids.GetRange(index, count);
                //ClientOf1C.GetEmployeesByIds(idsPage).ShowOneCObjects();
                HttpClientOfOneS.GetObjsByIds<OneC.Employee>(new Guid("f9e7b11f-609a-11e7-80cb-00155d65b717")).ShowOneCObjects();
                //ClientOf1C.GetEmployeesLike("Зорін").ShowOneCObjects();
                input = Console.ReadKey();
                index += count;
            } while (input.Key != ConsoleKey.Escape);
        }

        public static void ShowContractors()
        {
            List<Guid> ids = HttpClientOfOneS.GetIdsOfObjs<OneC.Contractor>();

            ConsoleKeyInfo input;
            Int32 index = 0;
            Int32 count = 1;
            do
            {
                Console.Clear();
                Console.WriteLine($"Index: {index,4}; Count: {count,4}\n\n");
                List<Guid> idsPage = ids.GetRange(index, count);
                HttpClientOfOneS.GetObjsByIds<OneC.Contractor>(idsPage).ShowOneCObjects();
                input = Console.ReadKey();
                index += count;
            } while (input.Key != ConsoleKey.Escape);
        }

        public static void GetContractorByCodesOfEdrpo()
        {
            HttpClientOfOneS.GetContractorsByCodeOfEdrpo("40623794").ShowOneCObjects();
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
            HttpClientOfOneS.GetContractorsByCodeOfEdrpo(codesOfEdrpo).ShowOneCObjects();
        }

        public static void GetContractorsByIdentifier()
        {
            HttpClientOfOneS.GetObjsByIds<OneC.Contractor>(new Guid("848b5acf-83ed-11e6-80ba-00155d65b717")).ShowOneCObjects();
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
            HttpClientOfOneS.GetObjsByIds<OneC.Contractor>(identifiers).ShowOneCObjects();
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
}
