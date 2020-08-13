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

namespace App
{
    class Program
    {
        private static HttpClientOfCreatio CreatioHttpClient = 
            HttpClientOfCreatio.LogIn("http://185.59.101.152:50080/", "Supervisor", "Supervisor");
        public static HttpClientOfOneC OneCHttpClient = 
            new HttpClientOfOneC(new HttpClientOfOneCSettings("http://185.59.101.152:50081/Dev03_1C/odata/standard.odata/", "Администратор", ""));



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
            List<ITIS.Employee> contacts = CreatioHttpClient.ODataGet<ITIS.Employee>();
            ITIS.Employee mZorin = contacts.FirstOrDefault(x => x.ITISSurName.Contains("Зорін"));
        }

        public static void SyncEmployees(SyncSettings settings)
        {
            // Получаем идентификаторы физ. лиц из 1С с почтовыми адресами, которые оканчиваются на указанный домен
            List<Guid> idsPersonsWithEmails = OneCHttpClient.GetIdsOfObjs<OneC.IRContactInfo>(
                "$filter=Объект_Type eq 'StandardODATA.Catalog_ФизическиеЛица'"
                + $" and cast(Вид, 'Catalog_ВидыКонтактнойИнформации') eq guid'{settings.OneCGuidOfEmailContactInfo}'"
                + $" and endswith(Представление, '{settings.EmailDomain}') eq true" , "Объект");

            // Получаем связанную контактрую информацию, типы контактной информации, физ. лица и сотрудников
            List<OneC.IRContactInfo> contactInfos = OneCHttpClient.GetObjsByIds<OneC.IRContactInfo>(idsPersonsWithEmails, "cast(Объект, 'Catalog_ФизическиеЛица')");
            {
                Dictionary<Guid, OneC.ContactInfoType> contactInfoTypes = 
                    OneCHttpClient.GetObjsByIds<OneC.ContactInfoType>(contactInfos.Select(x => new Guid(x.KeyKind)).Distinct().ToList())
                    .ToDictionary(k => k.Id);
                foreach(var contactInfo in contactInfos) contactInfo.RelatedObj_TypeOfContactInfo = contactInfoTypes[new Guid(contactInfo.KeyKind)];
            }
            Dictionary<Guid, OneC.Person> personsWithEmails = OneCHttpClient.GetObjsByIds<OneC.Person>(idsPersonsWithEmails).ToDictionary(k => k.Id);

            // Получаем данные из регистра InformationRegister_ФИОФизЛиц и связываем з физлицами
            List<OneC.IRNamesOfPersons> namesOfPersons = OneCHttpClient.GetObjsByIds<OneC.IRNamesOfPersons>(idsPersonsWithEmails, "cast(ФизЛицо, 'Catalog_ФизическиеЛица')");
            foreach (var personName in namesOfPersons) personsWithEmails[personName.KeyPerson].RelatedObj_NameInfo = personName;

            // Сгрупировать контактную информацию по физ. лицу
            Dictionary<Guid, List<OneC.IRContactInfo>> groupedContactInfo = new Dictionary<Guid, List<OneC.IRContactInfo>>();
            foreach(var item in contactInfos)
            {
                Guid idPerson = new Guid(item.KeyObject);
                if(!groupedContactInfo.ContainsKey(idPerson))
                    groupedContactInfo.Add(idPerson, new List<OneC.IRContactInfo> { item });
                else
                    groupedContactInfo[idPerson].Add(item);
            }

            foreach(var person in personsWithEmails.Values)
            {
                List<OneC.IRContactInfo> contactInfoOfPerson = groupedContactInfo[person.Id];
                foreach(var contactInfo in contactInfoOfPerson)
                {
                    Guid kindOfInfo = new Guid(contactInfo.KeyKind);
                    if(kindOfInfo == settings.OneCGuidOfEmailContactInfo)
                        person.RelatedObj_ContactInfoEmail = contactInfo;
                    else if(kindOfInfo == settings.OneCGuidOfPhoneContactInfo)
                        person.RelatedObj_ContactInfoPhone = contactInfo;
                    else if(kindOfInfo == settings.OneCGuidOfWorkPhoneContactInfo)
                        person.RelatedObj_ContactInfoWorkPhone = contactInfo;
                    else continue;
                }
            }

            // Связать физлица с сотрудниками
            List<OneC.Employee> employeesWithEmails = OneCHttpClient.GetObjsByIds<OneC.Employee>(idsPersonsWithEmails, "Физлицо_Key");
            foreach(var employee in employeesWithEmails) employee.NavProp_Person = personsWithEmails[(Guid)employee.PersonId];

            // Связать сотрудников с дожностями
            Dictionary<Guid, OneC.PositionInOrganization> positions = OneCHttpClient
                .GetObjsByIds<OneC.PositionInOrganization>(employeesWithEmails.DistinctValuesExcluding(default(Guid), x => x.PositionId).Cast<Guid>())
                .ToDictionary(k => k.Id);
            foreach (var employee in employeesWithEmails) employee.NavProp_Position = positions[(Guid)employee.PositionId];

            // Связать сотрудников с организациями
            Dictionary<Guid, OneC.Organization> organizations = OneCHttpClient.GetObjs<OneC.Organization>().ToDictionary(k => k.Id);
            foreach (var employee in employeesWithEmails) employee.NavProp_Organization = organizations[(Guid)employee.OrganizationId];

            // Связать сотрудников с подразделениями
            List<Guid> subdivisionIds = employeesWithEmails.DistinctValuesExcluding(default(Guid), x => x.OrganizationSubdivisionId).Cast<Guid>().ToList();
            subdivisionIds.AddRange(employeesWithEmails.DistinctValuesExcluding(default(Guid), x => x.CurrentOrganizationSubdivisionId).Cast<Guid>());
            Dictionary <Guid, OneC.OrganizationSubdivision> subdivisions = OneCHttpClient.GetObjsByIds<OneC.OrganizationSubdivision>(subdivisionIds).ToDictionary(k => k.Id);
            foreach (var employee in employeesWithEmails)
            {
                employee.NavProp_OrganizationSubdivision = subdivisions[(Guid)employee.OrganizationSubdivisionId];
                employee.NavProp_CurrentOrganizationSubdivision = subdivisions[(Guid)employee.CurrentOrganizationSubdivisionId];
            }

            //Dictionary<Guid, OneC.OrganizationSubdivision> subdivisions = OneCHttpClient.GetObjsByIds<OneC.OrganizationSubdivision>().GetObjs<OneC.OrganizationSubdivision>().ToDictionary(k => k.Id);
            //var s = subdivisions.Values.OrderBy(x => x.Description).ToList();



            foreach (var employee in employeesWithEmails)
            {
                employee.NavProp_OrganizationSubdivision = subdivisions[(Guid)employee.CurrentOrganizationSubdivisionId];
            }

            Dictionary<String, OneC.Employee> emailsAndPersons = employeesWithEmails.ToDictionary(k => k.NavProp_Person.RelatedObj_ContactInfoEmail.View);
            var oneC_Employee = employeesWithEmails.FirstOrDefault();
            oneC_Employee?.Show();
            oneC_Employee?.NavProp_Person?.Show(1);
            oneC_Employee?.NavProp_Person?.RelatedObj_NameInfo.Show(2);
            oneC_Employee?.NavProp_Person?.RelatedObj_ContactInfoEmail?.Show(2);
            oneC_Employee?.NavProp_Person?.RelatedObj_ContactInfoEmail?.RelatedObj_TypeOfContactInfo?.Show(3);
            oneC_Employee?.NavProp_Person?.RelatedObj_ContactInfoWorkPhone?.Show(2);
            oneC_Employee?.NavProp_Person?.RelatedObj_ContactInfoWorkPhone?.RelatedObj_TypeOfContactInfo?.Show(3);
            oneC_Employee?.NavProp_Person?.RelatedObj_ContactInfoPhone?.Show(2);
            oneC_Employee?.NavProp_Person?.RelatedObj_ContactInfoPhone?.RelatedObj_TypeOfContactInfo?.Show(3);
            oneC_Employee.NavProp_Organization?.Show(1);

            // Получить контакты по соответствующим Email и с типом Сотрудник нашей организации
            List<ITIS.Contact> contacts = CreatioHttpClient.GetObjsWherePropIn<ITIS.Contact, String>("Email", emailsAndPersons.Keys.ToList()).ToList();
            contacts = contacts.Where(x => x.TypeId == settings.CreatioGuidOfContactsWithTypeOurEmployees).ToList();

            var CreatioContact = contacts.FirstOrDefault();
            CreatioContact.ITISEmployeePosition = CreatioHttpClient.GetObjById<ITIS.EmployeeJob>(CreatioContact.ITISEmployeePositionId);
            CreatioContact.ITISSubdivision = CreatioHttpClient.GetObjById<Terrasoft.AccountOrganizationChart>(CreatioContact.ITISSubdivisionId);
            CreatioContact.ITISOrganizationSubdivision = CreatioHttpClient.GetObjById<Terrasoft.OrgStructureUnit>(CreatioContact.ITISOrganizationSubdivisionId);
            CreatioContact.Account = CreatioHttpClient.GetObjById<Terrasoft.Account>(CreatioContact.AccountId);
            CreatioContact.Gender = CreatioHttpClient.GetObjById<Terrasoft.Gender>(CreatioContact.GenderId);
            CreatioContact.Type = CreatioHttpClient.GetObjById<Terrasoft.ContactType>(CreatioContact.TypeId);
            CreatioContact.Job = CreatioHttpClient.GetObjById<Terrasoft.Job>(CreatioContact.JobId);
            CreatioContact.Department = CreatioHttpClient.GetObjById<Terrasoft.Department>(CreatioContact.DepartmentId);

            //CreatioContact.Show();
            // синхронизировать инфо. для контактов, которые существуют в 1С и Creatio
            foreach (ITIS.Contact c in contacts)
            {
                OneC.Employee c1 = null;
                // Если контакт есть только в Creatio но отсутсвует в 1С - пропустить
                if (!emailsAndPersons.TryGetValue(c.Email, out c1)) continue;
                // Если в настройках синхронизации указанно не синхронизировать - пропустить
                // Todo

                c.IdOneC = c1.Id;
                //c.TypeId = settings.CreatioGuidOfContactsWithTypeOurEmployees; // Для новых объектов
                c.GivenName = c1.NavProp_Person.RelatedObj_NameInfo.GivenName;
                c.Surname = c1.NavProp_Person.RelatedObj_NameInfo.Surname;
                c.MiddleName = c1.NavProp_Person.RelatedObj_NameInfo.MiddleName;
                c.Name = $"{c.Surname} {c.MiddleName} {c.GivenName}";
                c.Email = c1.NavProp_Person?.RelatedObj_ContactInfoEmail?.View;
                c.Phone = c1.NavProp_Person?.RelatedObj_ContactInfoPhone?.View;
                c.MobilePhone = c1.NavProp_Person?.RelatedObj_ContactInfoWorkPhone?.View;
                c.BirthDate = c1.NavProp_Person.BirthDate ?? new DateTime();
            }

            Terrasoft.Account account = CreatioHttpClient.GetObjById<Terrasoft.Account>(new Guid("b404c1c6-0508-489e-8b35-a02daca2066c"));

            // Создать контакты, которые существуют в 1С но еще не существуют в Creatio


            CreatioContact.Show();

            //ITIS.Employee empl = new ITIS.Employee();
            //empl.Contact.Email

            // Получаем связанные физические лица
            //List<OneC.Person> persons = OneCHttpClient.GetObjsByIds<OneC.Person>(emails.Keys);           
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
