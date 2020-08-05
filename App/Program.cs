using System;
using System.Collections.Generic;
using System.Linq;
using DatEx.OneC;
using OneC = DatEx.OneC.DataModel;
using DatEx.Creatio;
using ITIS = DatEx.Creatio.DataModel.ITIS;
using System.Security.Cryptography.X509Certificates;
using Newtonsoft.Json.Linq;

namespace App
{
    class Program
    {
        private static ClientOfCreatio CreatioHttpClient = ClientOfCreatio.LogIn("http://185.59.101.152:50080/", "Supervisor", "Supervisor");
        private static SettingsForClientOf1C settings = new SettingsForClientOf1C("http://185.59.101.152:50081/Dev03_1C/odata/standard.odata/", "Администратор", "");
        public static ClientOfOneC OneCHttpClient = new ClientOfOneC(settings);



        static void Main(string[] args)
        {
            //GetContractorsByIdentifiers();
            //GetContractorsByIdentifier();
            //GetContractorsByCodesOfEdrpo();
            //GetContractorByCodesOfEdrpo();
            //ShowContractors();
            //ShowEmployees();
            //GetGetContactInfo();

            //CreatioGetEmployees();

            //ShowIRContactInfo();

        
            SyncEmployees();
        }

        

        public static void CreatioGetEmployees()
        {
            List<ITIS.Employee> contacts = CreatioHttpClient.ODataGet<ITIS.Employee>();
            ITIS.Employee mZorin = contacts.FirstOrDefault(x => x.ITISSurName.Contains("Зорін"));
        }

        public static void SyncEmployees(Guid? guidOfAddressInfoOfTypeEmail = null, String domain = null)
        {
            // Так как ключевое свойство для синхронизации информации о сотрудниках начинаем поиск с InformationRegister_КонтактнаяИнформация
            // и ищем объекты которые связанны с записью Catalog_ВидыКонтактнойИнформации електронная почта,
            // то есть по нижеследующему Guid:
            // Указанный идентификатор нужно будет добавить в настройки Creatio, чтобы иметь возпожность изменить его на продуктиве
            guidOfAddressInfoOfTypeEmail ??= new Guid("6b1ae98e-bb91-11ea-80c7-00155d65b747");

            // Домен почты. Также нужно будет добавить в настройки Creatio, чтобы иметь возможность изменить при необходимости
            domain ??= "@kustoagro.com";

            // Получаем идентификаторы физ. лиц из 1С с почтовыми адресами, которые оканчиваются на указанный домен
            List<Guid> idsPersonsWithEmails = OneCHttpClient.GetIdsOfObjs<OneC.IRContactInfo>(
                "$filter=Объект_Type eq 'StandardODATA.Catalog_ФизическиеЛица'"
                + $" and cast(Вид, 'Catalog_ВидыКонтактнойИнформации') eq guid'{guidOfAddressInfoOfTypeEmail}'"
                + $" and endswith(Представление, '{domain}') eq true" , "Объект");

            // Одновременно получаем связанную контактрую информацию, типы контактной информации, физ. лица и сотрудников
            List<OneC.IRContactInfo> contactInfos = OneCHttpClient.GetObjsByIds<OneC.IRContactInfo>(idsPersonsWithEmails, "cast(Объект, 'Catalog_ФизическиеЛица')");
            contactInfos.ShowOneCObjects();            
            {
                Dictionary<Guid, OneC.ContactInfoType> contactInfoTypes = 
                    OneCHttpClient.GetObjsByIds<OneC.ContactInfoType>(contactInfos.Select(x => new Guid(x.KeyKind)).Distinct().ToList())
                    .ToDictionary(k => k.Ref_Key);
                foreach(var contactInfo in contactInfos) contactInfo.TypeOfContactInfo = contactInfoTypes[new Guid(contactInfo.KeyKind)];
            }
            List<OneC.Person> personsWithEmails = OneCHttpClient.GetObjsByIds<OneC.Person>(idsPersonsWithEmails);

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

            Guid kindOfEmailContactInfo = new Guid("6b1ae98e-bb91-11ea-80c7-00155d65b747");
            Guid kindOfPhoneContactInfo = new Guid("f1862c22-bb94-11ea-80c7-00155d65b747");
            Guid kindOfWorkPhoneContactInfo = new Guid("08188400-bb94-11ea-80c7-00155d65b747");

            foreach(var person in personsWithEmails)
            {
                List<OneC.IRContactInfo> contactInfoOfPerson = groupedContactInfo[person.Ref_Key];
                foreach(var contactInfo in contactInfoOfPerson)
                {
                    Guid kindOfInfo = new Guid(contactInfo.KeyKind);
                    if(kindOfInfo == kindOfEmailContactInfo)
                        person.ContactInfoEmail = contactInfo;
                    else if(kindOfInfo == kindOfPhoneContactInfo)
                        person.ContactInfoPhone = contactInfo;
                    else if(kindOfInfo == kindOfWorkPhoneContactInfo)
                        person.ContactInfoWorkPhone = contactInfo;
                    else continue;
                }
            }


            List<OneC.Employee> employeesWithEmails = OneCHttpClient.GetObjsByIds<OneC.Employee>(idsPersonsWithEmails, "Физлицо_Key");

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
