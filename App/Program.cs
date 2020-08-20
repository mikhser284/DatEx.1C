using System;
using System.Collections.Generic;
using System.Linq;
using DatEx.OneC;
using OneC = DatEx.OneC.DataModel;
using DatEx.Creatio;
using ITIS = DatEx.Creatio.DataModel.ITIS;
using System.Security.Cryptography.X509Certificates;
using Newtonsoft.Json.Linq;
using Terrasoft = DatEx.Creatio.DataModel.Terrasoft.Base;
using System.Collections.Concurrent;

namespace App
{
    partial class Program
    {
        private static HttpClientOfCreatio CreatioHttpClient = 
            HttpClientOfCreatio.LogIn("http://185.59.101.152:51080/", "Supervisor", "Supervisor"); // Creatio-Dev1
            //HttpClientOfCreatio.LogIn("http://185.59.101.152:50080/", "Supervisor", "Supervisor"); // Creatio-Dev3
        public static HttpClientOfOneC OneCHttpClient =
            new HttpClientOfOneC(new HttpClientOfOneCSettings("http://185.59.101.152:51081/Dev01_1C/odata/standard.odata/", "Администратор", "")); // Creatio-Dev1
            //new HttpClientOfOneC(new HttpClientOfOneCSettings("http://185.59.101.152:50081/Dev03_1C/odata/standard.odata/", "Администратор", "")); // Creatio-Dev3



        static void Main(string[] args)
        {
            //List<Terrasoft.ContactType> contactTypes = CreatioHttpClient.ODataGet<Terrasoft.ContactType>();

            //GetContractorsByIdentifiers();
            //GetContractorsByIdentifier();
            //GetContractorsByCodesOfEdrpo();
            //GetContractorByCodesOfEdrpo();
            //ShowContractors();
            //ShowEmployees();
            //GetGetContactInfo();

            //CreatioGetEmployees();

            //ShowIRContactInfo();

            SyncSettings settings = SyncSettings.GetDefaultSettings();
            SyncEmployees(settings);
        }



        

        public static void CreatioGetEmployees()
        {
            List<ITIS.Employee> contacts = CreatioHttpClient.GetObjs<ITIS.Employee>();
            ITIS.Employee mZorin = contacts.FirstOrDefault(x => x.ITISSurName.Contains("Зорін"));
        }

        public static void SyncEmployees(SyncSettings settings)
        {
            Dictionary<Guid, OneC.Person> personsWithEmailsOrderedById = OneC_GetPersonsAndRelatedInfoForSycn(settings);
            OneC_ShowPersonFullInfo(personsWithEmailsOrderedById.Values.Take(2));
            
            personsWithEmailsOrderedById.Values.ForEach(person => Console.WriteLine(person.GetShortNameAndActualPositions()));

            // Получить контакты по соответствующим Email и с типом Сотрудник нашей организации
            Dictionary<Guid, ITIS.Contact> creatio_Contacts = Creatio_GetContactsAcordingToPersonEmail(personsWithEmailsOrderedById);
            

            var creatioContact = creatio_Contacts.Skip(1).FirstOrDefault().Value;



            //CreatioContact.ITISOrganizationSubdivision = CreatioHttpClient.GetObjById<Terrasoft.OrgStructureUnit>(CreatioContact.ITISOrganizationSubdivisionId);
            //CreatioContact.Account = CreatioHttpClient.GetObjById<Terrasoft.Account>(CreatioContact.AccountId);
            ////CreatioContact.Gender = CreatioHttpClient.GetObjById<Terrasoft.Gender>(CreatioContact.GenderId);
            //CreatioContact.Type = CreatioHttpClient.GetObjById<Terrasoft.ContactType>(CreatioContact.TypeId);
            //CreatioContact.Job = CreatioHttpClient.GetObjById<Terrasoft.Job>(CreatioContact.JobId);
            //CreatioContact.Department = CreatioHttpClient.GetObjById<Terrasoft.Department>(CreatioContact.DepartmentId);


            Creatio_ShowContact(creatioContact);
            //// синхронизировать инфо. для контактов, которые существуют в 1С и Creatio
            //foreach (ITIS.Contact c in creatio_Contacts)
            //{
            //    OneC.Employee c1 = null;
            //    // Если контакт есть только в Creatio но отсутсвует в 1С - пропустить
            //    if (!emailsAndPersons.TryGetValue(c.Email, out c1)) continue;
            //    // Если в настройках синхронизации указанно не синхронизировать - пропустить
            //    // Todo

            //    c.IdOneC = c1.Id;
            //    //c.TypeId = settings.CreatioGuidOfContactsWithTypeOurEmployees; // Для новых объектов
            //    c.GivenName = c1.NavProp_Person.RelatedObj_NameInfo.GivenName;
            //    c.Surname = c1.NavProp_Person.RelatedObj_NameInfo.Surname;
            //    c.MiddleName = c1.NavProp_Person.RelatedObj_NameInfo.MiddleName;
            //    c.Name = $"{c.Surname} {c.MiddleName} {c.GivenName}";
            //    c.Email = c1.NavProp_Person?.RelatedObj_ContactInfoEmail?.View;
            //    c.Phone = c1.NavProp_Person?.RelatedObj_ContactInfoPhone?.View;
            //    c.MobilePhone = c1.NavProp_Person?.RelatedObj_ContactInfoWorkPhone?.View;
            //    c.BirthDate = c1.NavProp_Person?.BirthDate ?? new DateTime();
            //    c.Gender = genders[settings.MapGenderInOneC_GenderGuidInCreatio[c1.NavProp_Person.Gender]];
            //}

            //Terrasoft.Account account = CreatioHttpClient.GetObjById<Terrasoft.Account>(new Guid("b404c1c6-0508-489e-8b35-a02daca2066c"));

            //// Создать контакты, которые существуют в 1С но еще не существуют в Creatio


            //CreatioContact.Show();

            ////ITIS.Employee empl = new ITIS.Employee();
            ////empl.Contact.Email

            //// Получаем связанные физические лица
            ////List<OneC.Person> persons = OneCHttpClient.GetObjsByIds<OneC.Person>(emails.Keys);    


        }

        public static void ShowIRContactInfo()
        {
            Console.WriteLine("————— #1 ————————————————————");
            OneCHttpClient.GetObjs<OneC.IRContactInfo>("$filter=cast(Объект, 'Catalog_ФизическиеЛица') eq guid'23d75280-5b29-11e7-80cb-00155d65b717'")
                .ShowOneCObjects();

            Console.WriteLine("————— #1a ————————————————————");
            OneCHttpClient.GetObjs<OneC.IRContactInfo>("$filter=Объект_Type eq 'StandardODATA.Catalog_ФизическиеЛица' and cast(Вид, 'Catalog_ВидыКонтактнойИнформации') eq guid'6b1ae98e-bb91-11ea-80c7-00155d65b747'")
                .ShowOneCObjects();

            Console.WriteLine("————— #2 ————————————————————");
            OneCHttpClient.GetObjs<OneC.ContactInfoType>("$filter=Ref_Key eq guid'08188400-bb94-11ea-80c7-00155d65b747'").ShowOneCObjects();
            OneCHttpClient.GetObjs<OneC.ContactInfoType>("$filter=Ref_Key eq guid'f1862c22-bb94-11ea-80c7-00155d65b747'").ShowOneCObjects();
            OneCHttpClient.GetObjs<OneC.ContactInfoType>("$filter=Ref_Key eq guid'6b1ae98e-bb91-11ea-80c7-00155d65b747'").ShowOneCObjects();

            Console.WriteLine("————— #3 ————————————————————");
            OneCHttpClient.GetObjs<OneC.Person>("$top=20&$filter=Ref_Key eq guid'23d75280-5b29-11e7-80cb-00155d65b717'")
                .ShowOneCObjects();

            Console.WriteLine("————— #4 ————————————————————");
            //OneCHttpClient.GetObjs<OneC.IRNamesOfPersons>("$top=20&$filter=ФизЛицо_Type eq 'StandardODATA.Catalog_ФизическиеЛица' and ФизЛицо like '23d75280-5b29-11e7-80cb-00155d65b717'")
            OneCHttpClient.GetObjs<OneC.IRNamesOfPersons>("$top=20&$filter=cast(ФизЛицо, 'Catalog_ФизическиеЛица') eq guid'23d75280-5b29-11e7-80cb-00155d65b717'")
                .ShowOneCObjects();

            Console.WriteLine("————— #5 ————————————————————");
            OneCHttpClient.GetObjs<OneC.Employee>("$top=20&$filter=Физлицо_Key eq guid'23d75280-5b29-11e7-80cb-00155d65b717'")
                .ShowOneCObjects();

            

            //List<OneC.IRContactInfo> objs = OneCHttpClient.GetObjs<OneC.IRContactInfo>("$top=20");            
        }

        

        public static void ShowEmployees()
        {
            List<Guid> ids = OneCHttpClient.GetIdsOfObjs<OneC.Employee>();

            ConsoleKeyInfo input;
            Int32 index = 0;
            Int32 count = 1;
            do
            {
                Console.Clear();
                Console.WriteLine($"Index: {index,4}; Count: {count,4}\n\n");
                List<Guid> idsPage = ids.GetRange(index, count);
                //ClientOf1C.GetEmployeesByIds(idsPage).ShowOneCObjects();
                OneCHttpClient.GetObjsByIds<OneC.Employee>(new Guid("f9e7b11f-609a-11e7-80cb-00155d65b717")).ShowOneCObjects();
                //ClientOf1C.GetEmployeesLike("Зорін").ShowOneCObjects();
                input = Console.ReadKey();
                index += count;
            } while(input.Key != ConsoleKey.Escape);
        }

        public static void ShowContractors()
        {
            List<Guid> ids = OneCHttpClient.GetIdsOfObjs<OneC.Contractor>();

            ConsoleKeyInfo input;
            Int32 index = 0;
            Int32 count = 1;
            do
            {
                Console.Clear();
                Console.WriteLine($"Index: {index,4}; Count: {count,4}\n\n");
                List<Guid> idsPage = ids.GetRange(index, count);
                OneCHttpClient.GetObjsByIds<OneC.Contractor>(idsPage).ShowOneCObjects();
                input = Console.ReadKey();
                index += count;
            } while(input.Key != ConsoleKey.Escape);
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
            OneCHttpClient.GetObjsByIds<OneC.Contractor>(new Guid("848b5acf-83ed-11e6-80ba-00155d65b717")).ShowOneCObjects();
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
            OneCHttpClient.GetObjsByIds<OneC.Contractor>(identifiers).ShowOneCObjects();
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
        public static List<T> ShowOneCObjects<T>(this List<T> objects) where T : OneC.OneCObject
        {
            foreach(T obj in objects)
            {
                obj.Show();
                Console.WriteLine($"\n\n");
            }
            return objects;
        }
    }
}
