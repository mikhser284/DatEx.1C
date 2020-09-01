using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OneS = DatEx.OneC.DataModel;
using ITIS = DatEx.Creatio.DataModel.ITIS;
using Terrasoft = DatEx.Creatio.DataModel.Terrasoft.Base;
using System.Runtime;
using DatEx.OneC.DataModel;
using System.Runtime.CompilerServices;
using DatEx.Creatio.DataModel.ITIS;
using DatEx.Creatio;
using System.Threading;
using System.Security.Cryptography.X509Certificates;
using System.Net.WebSockets;

namespace App
{
    /// <summary> Первичная синхронизация контактов, сотрудников и контактной информации </summary>
    public partial class Program
    {
        public static void SyncEmployees(SyncSettings settings)
        {
            Task<SyncObjs> oneS_getSyncObjects = new Task<SyncObjs>(() => OneC_GetPersonsAndRelatedInfoForSync(settings));
            
            Task[] tasks = new Task[]
            {
                new Task(() => Creatio_ClearSyncObjects()),
                oneS_getSyncObjects
            }.StartAndWaitForAll();

            SyncObjs syncObjs = oneS_getSyncObjects.Result;
            OneC_ShowPersonFullInfo(syncObjs.OneS_PersonsOrderedById.Values.Take(2));
            syncObjs.OneS_PersonsOrderedById.Values.ForEach(person => Console.WriteLine(person.GetShortNameAndActualPositions()));

            Creatio_EmployeesFirstSyncWithOneS(syncObjs, settings);
        }


        /// <summary> Удалить из Creatio все синхронизированные ранее объекты </summary>
        public static void Creatio_ClearSyncObjects()
        {
            String removableObjsQuery = $"filter=ITISOneSId ne null and ITISOneSId ne {default(Guid)}";

            var contacts = HttpClientOfCreatio.GetObjs<ITIS.Contact>(removableObjsQuery);
            contacts.ForEach(x => HttpClientOfCreatio.DeleteObj(x));
            
            var jobs = HttpClientOfCreatio.GetObjs<ITIS.Job>(removableObjsQuery);
            jobs.ForEach(x => HttpClientOfCreatio.DeleteObj(x));

            var employeeJobs = HttpClientOfCreatio.GetObjs<ITIS.EmployeeJob>(removableObjsQuery);
            employeeJobs.ForEach(x => HttpClientOfCreatio.DeleteObj(x));

            var contractors = HttpClientOfCreatio.GetObjs<ITIS.Account>(removableObjsQuery);
            contractors.ForEach(x => HttpClientOfCreatio.DeleteObj(x));
        }



        public static void Creatio_EmployeesFirstSyncWithOneS(SyncObjs syncObjs, SyncSettings settings)
        {
            HashSet<Guid> contactIds = new HashSet<Guid>();
            List<ITIS.Contact> contacts = new List<Contact>();

            Console.WriteLine("\n ██████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████");
            Console.WriteLine(" █████ Объекты, импортированные ранее (из Excell) █████████████████████████████████████████████████████████████████████████████████████████████████████");
            Console.WriteLine(" ██████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████");

            contactIds.Add(new Guid("9ce50422-16d8-4960-8466-f0f3c2c69a77")); // Білоус Ю.О.
            contactIds.Add(new Guid("bb6658c4-33ca-4166-be23-bcd42d69078a")); // Зорін М.С.
            contacts = Creatio_GetCreatedContactsAndRelatedInfo(contactIds).Values.ToList();
            contacts.ForEach(x => Creatio_ShowContact(x));

            Console.WriteLine("\n ██████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████");
            Console.WriteLine(" █████ Синхронизированные объекты █████████████████████████████████████████████████████████████████████████████████████████████████████████████████████");
            Console.WriteLine(" ██████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████");
            
            contactIds.Clear();
            contacts.Clear();

            Creatio_FirstSyncOfEmployeesInfo(syncObjs, settings);

            foreach (Person p in syncObjs.OneS_PersonsOrderedById.Values)
            {
                ITIS.Contact c = Creatio_FirstSyncOfEmployeesInfo(p, settings, syncObjs);
                contactIds.Add((Guid)c.Id);
            }
            contacts = Creatio_GetCreatedContactsAndRelatedInfo(contactIds).Values.ToList();
            contacts.ForEach(x => Creatio_ShowContact(x));
        }

        private static void Creatio_FirstSyncOfEmployeesInfo(SyncObjs syncObjs, SyncSettings settings)
        {
            Creatio_EmployeesInfo_MapObjectsFromOneSToObjectsFromCreatio(syncObjs, settings);
        }

        private static void Creatio_EmployeesInfo_MapObjectsFromOneSToObjectsFromCreatio(SyncObjs syncObjs, SyncSettings settings)
        {
            Cratio_MapObj_Job(syncObjs, settings);
            Creatio_MapObj_EmployeeJob(syncObjs, settings);
            Creatio_MapObj_OrgStructureUnit(syncObjs, settings);
            Creatio_MapObj_Organizations(syncObjs, settings);
        }
        private static void Creatio_MapObj_Organizations(SyncObjs syncObjs, SyncSettings settings)
        {
            foreach(var org in syncObjs.OneS_Organizations.Values)
            {
                if(syncObjs.Creatio_Accounts_ByOneSId.ContainsKey(org.Id) || org.RelatedObj_Contractor == null) continue;
                ITIS.Account c = new ITIS.Account();
                //
                c.ITISOneSId = org.Id;
                c.Name = org.FullName;
                c.AlternativeName = org.Description;
                c.TypeId = settings.CreatioGuidOfOurCompany;
                c.OwnershipId = settings.CreatioGuidOfLLCOwnershipType;
                c.KPP = org.RelatedObj_Contractor?.CodeOfEdrpo;
                c.INN = org.RelatedObj_Contractor?.INN;
                c.ITISInternalCode = org.Prefix;
                c.ITISCounterpartyLegalStatusId = settings.Map_OneSEnum_LegalStatus_CreatioGuidOf_ITISCounterpartyLegalStatus[true];
                //
                c = HttpClientOfCreatio.CreateObj(c);
                syncObjs.Creatio_Accounts_ByOneSId.Add((Guid)c.ITISOneSId, c);
                syncObjs.Creatio_Accounts_ById.Add((Guid)c.Id, c);
            }
        }

        private static void Test_CreateAccount(SyncSettings settings)
        {
            ITIS.Account c = new ITIS.Account();
            c.ITISOneSId = new Guid("D4741F7B-5AA3-470B-A935-B26494017ED3");
            c.Name = "Test contractor";
            c.TypeId = settings.CreatioGuidOfOurCompany;
            c.KPP = "123456789";
            c.OwnershipId = settings.CreatioGuidOfLLCOwnershipType;
            //c.ITISInternalCode = "TST";
            //c.ITISEmail = "Test@mail.com";
            //c.ITISCounterpartyLegalStatusId = settings.Map_OneSEnum_LegalStatus_CreatioGuidOf_ITISCounterpartyLegalStatus[true];
            ////
            //c.INN = "123456789012";

            //c.AlternativeName = "Test contractor";

            //c.Code = "Code";
            //c.Phone = "+38 (097) 621 9438";
            //c.AdditionalPhone = "+38 (097) 621 9438";
            //c.Fax = "9876543210";
            //c.Web = "https://Test-contractor.com";
            //c.Address = "Аддресс";
            //c.Zip = "987654";
            //c.Notes = "Примечания";
            //c.GPSE = "150";
            //c.GPSN = "324";
            //
            c = HttpClientOfCreatio.CreateObj(c);
        }

        private static void Creatio_MapObj_OrgStructureUnit(SyncObjs syncObjs, SyncSettings settings)
        {
            foreach(var x in syncObjs.OneS_Subdivisions.Values)
            {
                if (syncObjs.Creatio_OrgStructureUnits_ByOneSId.ContainsKey(x.Id)) continue;
                ITIS.OrgStructureUnit c = new OrgStructureUnit();
                //
                c.ITISOneSId = (Guid)x.Id;
                c.FullName = x.Description;
                c.Name = x.Description;                
                //
                c = HttpClientOfCreatio.CreateObj(c);
                syncObjs.Creatio_OrgStructureUnits_ByOneSId.Add(c.ITISOneSId, c);
                syncObjs.Creatio_OrgStructureUnits_ById.Add((Guid)c.Id, c);
            }
        }
        
        
        
        private static void Creatio_MapObj_EmployeeJob(SyncObjs syncObjs, SyncSettings settings)
        {
            foreach(var x in syncObjs.OneS_Positions.Values)
            {
                if(syncObjs.Creatio_EmployeeJobs_ByOneSId.ContainsKey(x.Id)) continue;
                ITIS.EmployeeJob c = new EmployeeJob();
                //
                c.ITISOneSId = x.Id;
                c.Name = x.Description;
                //
                c = HttpClientOfCreatio.CreateObj(c);
                syncObjs.Creatio_EmployeeJobs_ByOneSId.Add(c.ITISOneSId, c);
                syncObjs.Creatio_EmployeeJobs_ById.Add((Guid)c.Id, c);
            }
        }

        
        
        private static void Cratio_MapObj_Job(SyncObjs syncObjs, SyncSettings settings)
        {
            foreach(var x in syncObjs.OneS_Positions.Values)
            {
                if(syncObjs.Creatio_Jobs_ByOneSId.ContainsKey(x.Id)) continue;
                ITIS.Job c = new Job();
                //
                c.ITISOneSId = x.Id;
                c.Name = x.Description;
                //
                c = HttpClientOfCreatio.CreateObj(c);
                syncObjs.Creatio_Jobs_ByOneSId.Add(c.ITISOneSId, c);
                syncObjs.Creatio_Jobs_ById.Add((Guid)c.Id, c);
            }
        }


        private static ITIS.Contact Creatio_FirstSyncOfEmployeesInfo(Person p, SyncSettings settings, SyncObjs temp)
        {
            ITIS.Contact c = new Contact();

            c.ITISOneSId = p.Id;
            c.TypeId = settings.CreatioGuidOfContactsWithTypeOurEmployees;
            c.Surname = p.RelatedObj_NameInfo.Surname;
            c.GivenName = p.RelatedObj_NameInfo.GivenName;
            c.MiddleName = p.RelatedObj_NameInfo.MiddleName;
            //Отключить авто заполнение фамилии имени и отчества из свойства Name; Наоборот брать Name из них:
            //c.Name = $"# {c.Surname} {c.GivenName} {c.MiddleName}"; 
            c.BirthDate = p.BirthDate;
            c.GenderId = settings.Map_OneSEnum_Gender_CreatioGuidOf_Gender[p.Gender];
            c.Email = p.RelatedObj_ContactInfoEmail.View;
            c.Phone = p.RelatedObj_ContactInfoWorkPhone.View;
            c.MobilePhone = p.RelatedObj_ContactInfoPhone.View;
            
            c.ITISCareeStartDate = (DateTime)p.RelatedObjs_RelatedEmployeePositions.Min(x => x.DateOfEmployment);
            Boolean isActualEmployee = p.RelatedObjs_RelatedEmployeePositions.Any(x => x.DateOfDismisal == null || x.DateOfDismisal == default(DateTime));
            c.ITISCareerEndDate = isActualEmployee ? null : (DateTime?)p.RelatedObjs_RelatedEmployeePositions.Max(x => x.DateOfDismisal);
            c = HttpClientOfCreatio.CreateObj(c);

            BindCareerJobs(settings, temp, p, c);

            HttpClientOfCreatio.UpdateObj(c);
            ITIS.Employee employee = Creatio_AddNewEmployee(c, p);
            return c;
        }

        private static void BindCareerJobs(SyncSettings settings, SyncObjs temp, Person p, ITIS.Contact c)
        {
            List<OneS.Employee> orderedPositions = p.RelatedObjs_RelatedEmployeePositions.OrderByDescending(x => x.DateOfEmployment).ToList();
            OneS.Employee lastActualPrimaryPosition = null;
            OneS.Employee lastPosition = null;

            foreach (var position in orderedPositions)
            {
                if (lastActualPrimaryPosition == null && (position.DueDate == null || position.DueDate == default(DateTime)))
                {
                    if (position.TypeOfEmployment == "ОсновноеМестоРаботы") lastActualPrimaryPosition = position;
                    if (lastPosition == null) lastPosition = position;
                }
            }

            // Если возможно то присвоить последнюю актуальную должность с основным местом работы
            if (lastActualPrimaryPosition != null) lastPosition = lastActualPrimaryPosition;
            // Если актуальных должностей вообще нет просто задать самую крайнюю
            if (lastPosition == null) lastPosition = orderedPositions.FirstOrDefault();
            if (lastPosition != null)
            {
                c.ITISEmploymentTypeId = settings.Map_OneSEnum_EmploymentType_CreatioGuidOf_EmploymentType[lastPosition.TypeOfEmployment];
                c.JobTitle = lastPosition.NavProp_Position.Description;
                ITIS.EmployeeJob job = new ITIS.EmployeeJob();
                job.Name = lastPosition.NavProp_Position.Description;
                job.ITISOneSId = lastPosition.NavProp_Position.Id;

                c.ITISEmployeePosition = HttpClientOfCreatio.CreateObj(job);
                c.ITISEmployeePositionId = c.ITISEmployeePosition.Id;
            }
        }

        private static ITIS.Employee Creatio_AddNewEmployee(ITIS.Contact c, Person p)
        {
            ITIS.Employee e = HttpClientOfCreatio.GetObjsByIds<ITIS.Employee>(new List<Guid> { (Guid)c.Id }, "Contact").Values.FirstOrDefault();
            Boolean employeeCreated = false;
            if (e == null)
            {
                employeeCreated = true;
                e = new ITIS.Employee();
            }

            e.ContactId = (Guid)c.Id;
            e.ITISSurName = p.RelatedObj_NameInfo.Surname;
            e.ITISGivenName = p.RelatedObj_NameInfo.GivenName;
            e.ITISMiddleName = p.RelatedObj_NameInfo.MiddleName;
            e.Notes = "* Создано/обновлено во время синхнонизации с 1С";


            // Создать или обновить связанного с контактом сотрудника
            // 1. При попытке обновить объект Сотрудник по протоколу oData возникает ошибка Internal server error, именно для данного типа объектов (возможно это связанно с его автоматическим создании после создания объекта Контакт)
            // 2. Во время автоматического создания Сотрудника почему-то создаеться пустой объект в детали Карьера

            //if (employeeCreated) CreatioHttpClient.CreateObj(e);
            //else CreatioHttpClient.UpdateObj(e);

            

            return e;
        }
    }

    /// <summary> Получение из 1C структур данных связанных с физлицами, сотрудниками и контактной информацией </summary>
    public partial class Program
    {
        /// <summary> Показать всю информацию по физ.лицу </summary>
        public static void OneC_ShowPersonFullInfo(IEnumerable<OneS.Person> persons)
        {
            if(persons == null)
            {
                Console.WriteLine("Persons is null");
                return;
            }

            if(persons.Count() == 0)
            {
                Console.WriteLine("Persons count == 0");
                return;
            }

            foreach (OneS.Person person in persons)
            {
                person.Show();
                person.RelatedObj_NameInfo?.Show(1);
            
                person.RelatedObj_ContactInfoEmail?.Show(1);
                person.RelatedObj_ContactInfoEmail?.RelatedObj_TypeOfContactInfo?.Show(2);

                person.RelatedObj_ContactInfoWorkPhone?.Show(1);
                person.RelatedObj_ContactInfoWorkPhone?.RelatedObj_TypeOfContactInfo?.Show(2);

                person.RelatedObj_ContactInfoPhone?.Show(1);
                person.RelatedObj_ContactInfoPhone?.RelatedObj_TypeOfContactInfo?.Show(2);

                foreach(OneS.Employee pos in person.RelatedObjs_RelatedEmployeePositions)
                {
                    pos.Show(1);
                    pos.NavProp_Organization?.Show(2);
                    pos.NavProp_Organization?.RelatedObj_LinkWithContractor?.Show(3);
                    pos.NavProp_Organization?.RelatedObj_Contractor?.Show(4);
                }
            }
        }

        /// <summary> Получить из 1С физ. лица и всю связанную с ними информацию необходимою для синхронизации </summary>
        public static SyncObjs OneC_GetPersonsAndRelatedInfoForSync(SyncSettings settings)
        {
            SyncObjs syncObjs = new SyncObjs();
            OneC_GetPersonsWithSufficientEmail(settings, syncObjs);
            Task[] tasks = new Task[]
            {
                new Task(() => OneC_GetContactInfoAndBindWithPersons(syncObjs, settings)),
                new Task(() => OneC_GetNamesAndBindWithPersons(syncObjs)),
                new Task(() => OneC_GetEmployeesAndBindWithPersons(syncObjs)),
            }.StartAndWaitForAll();
            return syncObjs;
        }

        /// <summary> Получить из 1С физ. лица с подходящим email </summary>
        public static Dictionary<Guid, OneS.Person> OneC_GetPersonsWithSufficientEmail(SyncSettings settings, SyncObjs syncObjs)
        {
            // Получаем идентификаторы физ. лиц с почтовыми адресами, которые оканчиваются на указанный домен
            List<Guid> idsPersonsWithEmails = HttpClientOfOneS.GetIdsOfObjs<OneS.IRContactInfo>(
                "$filter=Объект_Type eq 'StandardODATA.Catalog_ФизическиеЛица'"
                + $" and cast(Вид, 'Catalog_ВидыКонтактнойИнформации') eq guid'{settings.OneCGuidOfEmailContactInfo}'"
                + $" and endswith(Представление, '{settings.EmailDomain}') eq true", "Объект");

            // Получаем объекты физ. лиц
            syncObjs.OneS_PersonsOrderedById = HttpClientOfOneS.GetObjsByIds<OneS.Person>(idsPersonsWithEmails).ToDictionary(k => k.Id);
            return syncObjs.OneS_PersonsOrderedById;
        }

        /// <summary> Получить из 1С контактную информацию и связать ее с физ. лицам </summary>
        public static void OneC_GetContactInfoAndBindWithPersons(SyncObjs syncObjs, SyncSettings settings)
        {
            syncObjs.OneS_ContactInfosGroupedByPersonId = HttpClientOfOneS
                .GetObjsByIds<OneS.IRContactInfo>(syncObjs.OneS_PersonsOrderedById.Keys, "cast(Объект, 'Catalog_ФизическиеЛица')")
                .GroupToDictionaryBy(x => new Guid(x.KeyObject));
            OneC_GetAndBindContactInfoTypes(syncObjs);
            foreach(OneS.Person person in syncObjs.OneS_PersonsOrderedById.Values)
            {
                List<OneS.IRContactInfo> contactInfoOfPerson = syncObjs.OneS_ContactInfosGroupedByPersonId[person.Id];
                foreach (var contactInfo in contactInfoOfPerson)
                {
                    Guid kindOfInfo = new Guid(contactInfo.KeyKind);
                    if (kindOfInfo == settings.OneCGuidOfEmailContactInfo)
                        person.RelatedObj_ContactInfoEmail = contactInfo;
                    else if (kindOfInfo == settings.OneCGuidOfPhoneContactInfo)
                        person.RelatedObj_ContactInfoPhone = contactInfo;
                    else if (kindOfInfo == settings.OneCGuidOfWorkPhoneContactInfo)
                        person.RelatedObj_ContactInfoWorkPhone = contactInfo;
                    else continue;
                }
            }
        }

        /// <summary> Получить из 1С типы контактной информации и связать с контактной информацией </summary>
        static void OneC_GetAndBindContactInfoTypes(SyncObjs syncObjs)
        {
            HashSet<Guid> idsOfcontactInfoTypes = new HashSet<Guid>();
            syncObjs.OneS_ContactInfosGroupedByPersonId.ForEach(key => key.Value.ForEach(value => idsOfcontactInfoTypes.Add(new Guid(value.KeyKind))));
            syncObjs.OneS_ContactInfoTypes = HttpClientOfOneS.GetObjsByIds<OneS.ContactInfoType>(idsOfcontactInfoTypes).ToDictionary(k => k.Id);
            //
            syncObjs.OneS_ContactInfosGroupedByPersonId.ForEach(key => key.Value.ForEach(value => value.RelatedObj_TypeOfContactInfo = syncObjs.OneS_ContactInfoTypes[new Guid(value.KeyKind)]));
        }

        /// <summary> Получить из 1С данные из инфо. регистра InformationRegister_ФИОФизЛиц и связываем з физлицами </summary>
        public static void OneC_GetNamesAndBindWithPersons(SyncObjs syncObjs)
        {
            syncObjs.OneS_NamesOfPersons = HttpClientOfOneS
                .GetObjsByIds<OneS.IRNamesOfPersons>(syncObjs.OneS_PersonsOrderedById.Keys, "cast(ФизЛицо, 'Catalog_ФизическиеЛица')");
            foreach (var personName in syncObjs.OneS_NamesOfPersons)
                syncObjs.OneS_PersonsOrderedById[personName.KeyPerson].RelatedObj_NameInfo = personName;
        }

        /// <summary> Получить из 1С сотрудников и связать с физ. лицами </summary>
        public static void OneC_GetEmployeesAndBindWithPersons(SyncObjs syncObjs)
        {
            // Связать сотрудников с физ. лицами
            syncObjs.OneS_Employees = HttpClientOfOneS.GetObjsByIds<OneS.Employee>(syncObjs.OneS_PersonsOrderedById.Keys, "Физлицо_Key");
            syncObjs.OneS_Employees.ForEach(employee => employee.NavProp_Person = syncObjs.OneS_PersonsOrderedById[(Guid)employee.PersonId]);

            // Связать физ. лиц с сотрудниками
            syncObjs.OneS_Employees.ForEach(employee => syncObjs.OneS_PersonsOrderedById[(Guid)employee.PersonId].RelatedObjs_RelatedEmployeePositions.Add(employee));

            Task[] tasks = new Task[]
            {
                new Task(() => OneC_GetPositionsAndBindWithEmployees(syncObjs)),
                new Task(() => OneC_GetOrganizationsAndBindWithEmployees(syncObjs)),
                new Task(() => OneC_GetSubdivisionsAndBindWithEmployees(syncObjs))
            }.StartAndWaitForAll();
        }

        /// <summary> Получить из 1С должности и связать их с сотрудниками </summary>
        static void OneC_GetPositionsAndBindWithEmployees(SyncObjs syncObjs)
        {
            syncObjs.OneS_Positions = null;
            {
                List<Guid> listOfpositions = syncObjs.OneS_Employees.DistinctValuesExcluding(default(Guid), x => x.PositionId).Cast<Guid>().ToList();
                listOfpositions.AddRange(syncObjs.OneS_Employees.DistinctValuesExcluding(default(Guid), x => x.CurrentCompanyPositionId).Cast<Guid>());
                listOfpositions.AddRange(syncObjs.OneS_Employees.DistinctValuesExcluding(default(Guid), x => x.CurrentPositionInOrganizationId).Cast<Guid>());
                syncObjs.OneS_Positions = HttpClientOfOneS.GetObjsByIds<OneS.PositionInOrganization>(listOfpositions.DistinctValuesExcluding(default(Guid), x => x).Cast<Guid>())
                    .ToDictionary(k => k.Id);
            }
            foreach (var employee in syncObjs.OneS_Employees)
            {

                if (employee.PositionId.IsNotNullOrDefault())
                    employee.NavProp_Position = syncObjs.OneS_Positions[(Guid)employee.PositionId];
                if (employee.CurrentCompanyPositionId.IsNotNullOrDefault())
                    employee.NavProp_CurrentCompanyPosition = syncObjs.OneS_Positions[(Guid)employee.CurrentCompanyPositionId];
                if (employee.CurrentPositionInOrganizationId.IsNotNullOrDefault())
                    employee.NavProp_CurrentPositionInOrganization = syncObjs.OneS_Positions[(Guid)employee.CurrentPositionInOrganizationId];
            }
        }

        /// <summary> Получить из 1С организации и связать их с сотрудниками </summary>
        static void OneC_GetOrganizationsAndBindWithEmployees(SyncObjs syncObjs)
        {
            syncObjs.OneS_Organizations = HttpClientOfOneS.GetObjs<OneS.Organization>().ToDictionary(k => k.Id);
            foreach (var employee in syncObjs.OneS_Employees)
                if (employee.OrganizationId.IsNotNullOrDefault())
                    employee.NavProp_Organization = syncObjs.OneS_Organizations[(Guid)employee.OrganizationId];
            OneS_GetContractorsAndBindWithOrganizations(syncObjs);
        }

        public static void OneS_GetContractorsAndBindWithOrganizations(SyncObjs syncObjs)
        {
            //OneCHttpClient.GetObjsByIds<OneC.IROwnContracror>(syncObjs.OneS_Organizations.Keys, "Объект").ToDictionary(k => k.ObjectId);
            syncObjs.OneS_IROwnContractorsOrderedByOrganizationId = HttpClientOfOneS.GetObjs<OneS.IROwnContracror>().ToDictionary(k => k.ObjectId);
            HashSet<Guid> idsOfContractors = new HashSet<Guid>();
            foreach(var org in syncObjs.OneS_Organizations.Values)
            {
                IROwnContracror ownContractor;
                if (!syncObjs.OneS_IROwnContractorsOrderedByOrganizationId.TryGetValue(org.Id, out ownContractor)) continue;
                org.RelatedObj_LinkWithContractor = ownContractor;
                idsOfContractors.Add(ownContractor.KeyContractorId);
            }

            syncObjs.OneS_ContractorsOrderedById = HttpClientOfOneS.GetObjsByIds<OneS.Contractor>(idsOfContractors).ToDictionary(k => k.Id);
            foreach(var org in syncObjs.OneS_Organizations.Values)
            {
                if (org.RelatedObj_LinkWithContractor == null) continue;
                org.RelatedObj_Contractor = syncObjs.OneS_ContractorsOrderedById[org.RelatedObj_LinkWithContractor.KeyContractorId];
            }
        }

        /// <summary> Получить из 1С подразделения и связать их с сотрудниками </summary>
        static void OneC_GetSubdivisionsAndBindWithEmployees(SyncObjs syncObjs)
        {

            List<Guid> subdivisionIds = syncObjs.OneS_Employees.DistinctValuesExcluding(default(Guid), x => x.OrganizationSubdivisionId).Cast<Guid>().ToList();
            subdivisionIds.AddRange(syncObjs.OneS_Employees.DistinctValuesExcluding(default(Guid), x => x.CurrentOrganizationSubdivisionId).Cast<Guid>());
            syncObjs.OneS_Subdivisions = HttpClientOfOneS.GetObjsByIds<OneS.OrganizationSubdivision>(subdivisionIds).ToDictionary(k => k.Id);
            foreach (var employee in syncObjs.OneS_Employees)
            {
                if (employee.OrganizationSubdivisionId.IsNotNullOrDefault())
                    employee.NavProp_OrganizationSubdivision = syncObjs.OneS_Subdivisions[(Guid)employee.OrganizationSubdivisionId];
                if (employee.CurrentOrganizationSubdivisionId.IsNotNullOrDefault())
                    employee.NavProp_CurrentOrganizationSubdivision = syncObjs.OneS_Subdivisions[(Guid)employee.CurrentOrganizationSubdivisionId];
            }
        }
    }

    /// <summary> Получение объектов из Creatio </summary>
    public partial class Program
    {
        /// <summary> Получить из Creatio контакты по email физ.лиц из 1C </summary>
        public static Dictionary<Guid, ITIS.Contact> Creatio_GetCreatedContactsAndRelatedInfo(HashSet<Guid> contactsIds)
        {
            Dictionary<Guid, ITIS.Contact> creatio_ContactsOrderedById = HttpClientOfCreatio.GetObjsByIds<ITIS.Contact>(contactsIds);
            List<ITIS.Contact> creatio_Contacts = creatio_ContactsOrderedById.Values.ToList();

            Task[] tasks = new Task[]
            {
                new Task(() => Creatio_GetEmployeeJobAndBindWithContact(creatio_Contacts)),
                new Task(() => Cratio_GetContactCareerAndBindWithContact(creatio_ContactsOrderedById)),
                new Task(() => Creatio_GetEmployeeAndBindWithContact(creatio_ContactsOrderedById)),
                new Task(() => Creatio_GetContactGenderAndBindWithContact(creatio_Contacts)),
                new Task(() => Creatio_GetSubdivisionsAndBindWithEmployees(creatio_Contacts)),
                new Task(() => Creatio_GetEmploymentTypesAndBindWitEmployees(creatio_Contacts)),
                new Task(() => Creatio_GetSubdivisionAndBindWithContact(creatio_Contacts)),
                new Task(() => Creaio_GetAccountsAndBindWithContacts(creatio_Contacts)),
                new Task(() => Creatio_GetContactTypesAndBindWithContacts(creatio_Contacts)),
                new Task(() => Creatio_GetJobsAndBindWithContacts(creatio_Contacts)),
                new Task(() => Creatio_GetDepartmentsAndBindWithContacts(creatio_Contacts)),
            }.StartAndWaitForAll();            

            return creatio_ContactsOrderedById;
        }




        public static void Creatio_ShowContact(ITIS.Contact contact, Boolean showMapingsOrRemarks = true)
        {
            contact?.Show(0, showMapingsOrRemarks);
            foreach(var career in contact?.Career)
            {
                career.Show(1, showMapingsOrRemarks);
                career.Account?.Show(2, showMapingsOrRemarks);
                career.OrgStructureUnit?.Show(2, showMapingsOrRemarks);
                career.ITISSubdivision?.Show(2, showMapingsOrRemarks);
                career.Job?.Show(2, showMapingsOrRemarks);
            }

            ITIS.Employee employee = (ITIS.Employee)contact?.Employee;
            employee?.Show(1, showMapingsOrRemarks);
            foreach(var career in employee?.Career)
            {
                career.Show(2, showMapingsOrRemarks);
                career.Account?.Show(3, showMapingsOrRemarks);
                career.OrgStructureUnit?.Show(3, showMapingsOrRemarks);
                career.EmployeeJob?.Show(3, showMapingsOrRemarks);
            }
            employee?.Career.ForEach(x => x.Show(2, showMapingsOrRemarks));

            contact?.ITISSubdivision?.Show(1, showMapingsOrRemarks);
            contact?.ITISEmploymentType?.Show(1, showMapingsOrRemarks);
            contact?.ITISOrganizationSubdivision?.Show(1, showMapingsOrRemarks);
            contact?.Account?.Show(1, showMapingsOrRemarks);
            contact?.Type?.Show(1, showMapingsOrRemarks);
            (contact?.Job as ITIS.Job)?.Show(1, showMapingsOrRemarks);
            contact?.Department?.Show(1, showMapingsOrRemarks);
        }




        /// <summary> Получить из Creatio сотрудников и связать с контактами </summary>
        public static void Creatio_GetEmployeeAndBindWithContact(Dictionary<Guid, ITIS.Contact> creatio_ContactsOrderedById)
        {
            Dictionary<Guid, ITIS.Employee> employeesOrderedById = HttpClientOfCreatio.GetObjsByIds<ITIS.Employee>(creatio_ContactsOrderedById.Values.Select(x => (Guid)x.Id), "Contact");
            
            foreach (var employee in employeesOrderedById.Values)
            {
                if (employee.ContactId.IsNotNullOrDefault())
                {
                    ITIS.Contact contact = creatio_ContactsOrderedById[(Guid)employee.ContactId];
                    employee.Contact = contact;
                    contact.Employee = employee;
                }                
            }

            Creatio_SyncEmployeesWithContact(employeesOrderedById.Values.ToList());
            Creatio_GetEmployeeCareerAndBindWithEmploee(employeesOrderedById);
        }



        public static void Creatio_GetContactGenderAndBindWithContact(List<ITIS.Contact> creatio_Contacts)
        {
            Dictionary<Guid, Terrasoft.Gender> genders = HttpClientOfCreatio.GetObjsByIds<Terrasoft.Gender>(creatio_Contacts.Select(x => (Guid)x.GenderId));
            foreach(var contact in creatio_Contacts)
            {
                if (contact.GenderId.IsNotNullOrDefault())
                    contact.Gender = genders[(Guid)contact.GenderId];
            }
        }

        
        
        /// <summary> Получить из Creatio должности сотрудников и связать с контактами </summary>
        public static void Creatio_GetEmployeeJobAndBindWithContact(List<ITIS.Contact> creatio_Contacts)
        {
            Dictionary<Guid, ITIS.EmployeeJob> jobs = HttpClientOfCreatio.GetObjsByIds<ITIS.EmployeeJob>(creatio_Contacts.SelectValuableGuids(x => x.ITISEmployeePositionId));

            foreach(var contact in creatio_Contacts)
            {
                if (contact.ITISEmployeePositionId.IsNotNullOrDefault())
                    contact.ITISEmployeePosition = jobs[(Guid)contact.ITISEmployeePositionId];
            }
        }

        
        
        /// <summary> Получить из Creatio подразделения и связать с сотрудниками </summary>
        public static void Creatio_GetSubdivisionsAndBindWithEmployees(List<ITIS.Contact> creatio_Contacts)
        {
            Dictionary<Guid, Terrasoft.AccountOrganizationChart> subdivisions = HttpClientOfCreatio.GetObjsByIds<Terrasoft.AccountOrganizationChart>(creatio_Contacts.SelectValuableGuids(x => x.ITISSubdivisionId));
            
            foreach(var contact in creatio_Contacts)
            {
                if (contact.ITISSubdivisionId.IsNotNullOrDefault())
                    contact.ITISSubdivision = subdivisions[(Guid)contact.ITISSubdivisionId];
            }
        }

        
        
        /// <summary> Получить из Creatio тип зайнятости и связать с контактами </summary>
        public static void Creatio_GetEmploymentTypesAndBindWitEmployees(List<ITIS.Contact> creatio_Contacts)
        {
            Dictionary<Guid, ITISEmploymentType> employmentTypes = HttpClientOfCreatio.GetObjsByIds<ITISEmploymentType>(creatio_Contacts.SelectValuableGuids(x => x.ITISEmploymentTypeId));
            foreach(var contact in creatio_Contacts)
            {
                if (contact.ITISEmploymentTypeId.IsNotNullOrDefault())
                    contact.ITISEmploymentType = employmentTypes[(Guid)contact.ITISEmploymentTypeId];
            }
        }

        
        
        /// <summary> Получить из Creatio подразделения организаций зайнятости и связать с контактами </summary>
        public static void Creatio_GetSubdivisionAndBindWithContact(List<ITIS.Contact> creatio_Contacts)
        {
            Dictionary<Guid, Terrasoft.OrgStructureUnit> organizationSubdivisions = HttpClientOfCreatio.GetObjsByIds<Terrasoft.OrgStructureUnit>(creatio_Contacts.SelectValuableGuids(x => x.ITISOrganizationSubdivisionId));
            foreach(var contact in creatio_Contacts)
            {
                if (contact.ITISOrganizationSubdivisionId.IsNotNullOrDefault())
                    contact.ITISOrganizationSubdivision = organizationSubdivisions[(Guid)contact.ITISOrganizationSubdivisionId];
            }
        }

        
        
        /// <summary> Получить из Creatio контрагентов и связать с контактами </summary>
        public static void Creaio_GetAccountsAndBindWithContacts(List<ITIS.Contact> creatio_Contacts)
        {
            Dictionary<Guid, ITIS.Account> accounts = HttpClientOfCreatio.GetObjsByIds<ITIS.Account>(creatio_Contacts.SelectValuableGuids(x => x.AccountId));
            foreach(var contact in creatio_Contacts)
            {
                if (contact.AccountId.IsNotNullOrDefault())
                    contact.Account = accounts[(Guid)contact.AccountId];
            }
        }

        
        
        /// <summary> Получить из Creatio типы контактов и связать с контактами </summary>
        public static void Creatio_GetContactTypesAndBindWithContacts(List<ITIS.Contact> creatio_Contacts)
        {
            Dictionary<Guid, Terrasoft.ContactType> contactTypes = HttpClientOfCreatio.GetObjsByIds<Terrasoft.ContactType>(creatio_Contacts.Select(x => (Guid)x.TypeId));
            foreach(var contact in creatio_Contacts)
            {
                if (contact.TypeId.IsNotNullOrDefault())
                    contact.Type = contactTypes[(Guid)contact.TypeId];
            }
        }

        
        
        /// <summary> Получить из Creatio должности и связать с контактами </summary>
        public static void Creatio_GetJobsAndBindWithContacts(List<ITIS.Contact> creatio_Contacts)
        {
            Dictionary<Guid, Terrasoft.Job> jobs = HttpClientOfCreatio.GetObjsByIds<Terrasoft.Job>(creatio_Contacts.SelectValuableGuids(x => x.JobId));
            foreach(var contact in creatio_Contacts)
            {
                if (contact.JobId.IsNotNullOrDefault())
                    contact.Job = jobs[(Guid)contact.JobId];
            }
        }

        
        
        /// <summary> Получить из Creatio подразделения и связать с контактами </summary>
        public static void Creatio_GetDepartmentsAndBindWithContacts(List<ITIS.Contact> creatio_Contacts)
        {
            Dictionary<Guid, Terrasoft.Department> departmentns = HttpClientOfCreatio.GetObjsByIds<Terrasoft.Department>(creatio_Contacts.SelectValuableGuids(x => x.DepartmentId));
            foreach(var contact in creatio_Contacts)
            {
                if (contact.DepartmentId.IsNotNullOrDefault())
                    contact.Department = departmentns[(Guid)contact.DepartmentId];
            }
        }

        
        
        /// <summary> Получить из Creatio подразделения и связать с контактами </summary>
        public static void Creatio_SyncEmployeesWithContact(List<ITIS.Employee> creatio_Employees)
        {
            foreach(var employee in creatio_Employees)
            {
                ITIS.Contact contact = (ITIS.Contact)employee.Contact;
                if (contact == null) continue;

                employee.ITISSurName = contact.Surname;
                employee.ITISGivenName = contact.GivenName;
                employee.ITISMiddleName = contact.MiddleName;
                employee.ITISEmploymentsTypeId = contact.ITISEmploymentTypeId;
                employee.ITISEmploymentsType = contact.ITISEmploymentType;
                employee.Name = $"{contact.Surname} {contact.GivenName} {contact.MiddleName}";
                employee.OrgStructureUnitId = contact.ITISOrganizationSubdivisionId;
                employee.OrgStructureUnit = contact.ITISOrganizationSubdivision;
                employee.JobId = contact.JobId;
                employee.Job = contact.Job;
                employee.CareerStartDate = contact.ITISCareeStartDate;
                //employee.CareerDueDate = contact.ItisCareerDueDate; // Поле contact.ItisCareerDueDate отсутствует
                employee.AccountId = contact.AccountId;
                employee.Account = contact.Account;
            }
        }

        
        
        /// <summary> Получить из Creatio карьеру сотрудников и связать с сотрудниками </summary>
        public static void Creatio_GetEmployeeCareerAndBindWithEmploee(Dictionary<Guid, ITIS.Employee> employeesOrderedById)
        {          
            Dictionary<Guid, List<ITIS.EmployeeCareer>> careersGroupedByEmployeeId = HttpClientOfCreatio.GetBindedObjsByParentIds<ITIS.EmployeeCareer>(employeesOrderedById.Keys, "Employee", x => x.EmployeeId);
            foreach(var item in careersGroupedByEmployeeId)
            {
                ITIS.Employee employee;
                if (!employeesOrderedById.TryGetValue(item.Key, out employee)) continue;
                employee.Career.AddRange(item.Value);
                item.Value.ForEach(x => x.Employee = employee);
            }

            List<ITIS.EmployeeCareer> careers = new List<EmployeeCareer>();
            careersGroupedByEmployeeId.ForEach(x => careers.AddRange(x.Value));

            Task[] bindRelatedObjs = new Task[]
            {
                new Task(() => Creatio_GetEmployementTypeAndBindWithEmploymentCareer(careers)),
                new Task(() => Creatio_GetAccountsAndBindWithEmployeeCareers(careers)),
                new Task(() => Creatio_GetOrgStructureUnitsAndBindWithEmployeeCareers(careers)),
                new Task(() => Creatio_GetJobAndBindWithEmployeeCareers(careers)),
            }.StartAndWaitForAll();
        }

        
        
        /// <summary> Получить из Creatio типы видов зайнятости и связать с карьерой сотрудника </summary>
        public static void Creatio_GetEmployementTypeAndBindWithEmploymentCareer(List<ITIS.EmployeeCareer> careers)
        {
            HashSet<Guid> employmentTypesIds = new HashSet<Guid>(careers.SelectValuableGuids(x => x.ITISTypeOfEmploymentId));
            Dictionary<Guid, ITIS.ITISEmploymentType> employmentTypes = HttpClientOfCreatio.GetObjsByIds<ITIS.ITISEmploymentType>(employmentTypesIds);

            foreach(var career in careers)
            {
                if (career.ITISTypeOfEmploymentId.IsNotNullOrDefault())
                    career.ITISTypeOfEmployment = employmentTypes[(Guid)career.ITISTypeOfEmploymentId];
            }
        }

        
        
        /// <summary> Получить из Creatio контрагентов и связать с карьерой сотрудника </summary>
        public static void Creatio_GetAccountsAndBindWithEmployeeCareers(List<ITIS.EmployeeCareer> careers)
        {
            HashSet<Guid> accountIds = new HashSet<Guid>(careers.SelectValuableGuids(x => x.AccountId));
            Dictionary<Guid, ITIS.Account> accounts = HttpClientOfCreatio.GetObjsByIds<ITIS.Account>(accountIds);

            foreach(var career in careers)
            {
                if (career.AccountId.IsNotNullOrDefault())
                    career.Account = accounts[(Guid)career.AccountId];
            }
        }

        
        
        /// <summary> Получить из Creatio елементы орг. структуры и привязать к карьере сотрудника </summary>
        public static void Creatio_GetOrgStructureUnitsAndBindWithEmployeeCareers(List<ITIS.EmployeeCareer> careers)
        {
            HashSet<Guid> orgStructUnitsIds = new HashSet<Guid>(careers.SelectValuableGuids(x => x.OrgStructureUnitId));
            Dictionary<Guid, ITIS.OrgStructureUnit> orgStructUnits = HttpClientOfCreatio.GetObjsByIds<ITIS.OrgStructureUnit>(orgStructUnitsIds);

            foreach(var career in careers)
            {
                if (career.OrgStructureUnitId.IsNotNullOrDefault())
                    career.OrgStructureUnit = orgStructUnits[(Guid)career.OrgStructureUnitId];
            }
        }

        
        
        /// <summary> Получить из Creatio должности и привязать к карьере сотрудника </summary>
        public static void Creatio_GetJobAndBindWithEmployeeCareers(List<ITIS.EmployeeCareer> careers)
        {
            HashSet<Guid> jobIds = new HashSet<Guid>(careers.SelectValuableGuids(x => x.EmployeeJobId));
            Dictionary<Guid, ITIS.EmployeeJob> jobs = HttpClientOfCreatio.GetObjsByIds<ITIS.EmployeeJob>(jobIds);

            foreach(var career in careers)
            {
                if (career.EmployeeJobId.IsNotNullOrDefault())
                    career.EmployeeJob = jobs[(Guid)career.EmployeeJobId];
            }
        }


        public static void Cratio_GetContactCareerAndBindWithContact(Dictionary<Guid, ITIS.Contact> creatio_ContactsOrderedById)
        {
            Dictionary<Guid, List<ITIS.ContactCareer>> careersGroupedByContactId = HttpClientOfCreatio.GetBindedObjsByParentIds<ITIS.ContactCareer>(creatio_ContactsOrderedById.Keys, "Contact", x => x.ContactId);
            foreach(var item in careersGroupedByContactId)
            {
                ITIS.Contact contact;
                if (!creatio_ContactsOrderedById.TryGetValue(item.Key, out contact)) continue;
                contact.Career.AddRange(item.Value);
                item.Value.ForEach(x => x.Contact = contact);
            }


            List<ITIS.ContactCareer> careers = new List<ContactCareer>();
            careersGroupedByContactId.ForEach(x => careers.AddRange(x.Value));

            Task[] bindRelatedObjs = new Task[]
            {
                new Task(() => Creatio_GetEmployementTypeAndBindWithContactCareer(careers)),
                new Task(() => Creatio_GetAccountsAndBindWithContactCareers(careers)),
                new Task(() => Creatio_GetOrgStructureUnitsAndBindWithContactCareers(careers)),
                new Task(() => Creatio_GetJobAndBindWithContactCareers(careers)),
            }.StartAndWaitForAll();
        }



        /// <summary> Получить из Creatio типы видов зайнятости и связать с карьерой контакта </summary>
        public static void Creatio_GetEmployementTypeAndBindWithContactCareer(List<ITIS.ContactCareer> careers)
        {
            HashSet<Guid> employmentTypesIds = new HashSet<Guid>(careers.SelectValuableGuids(x => x.ITISEmploymentTypeId));
            Dictionary<Guid, ITIS.ITISEmploymentType> employmentTypes = HttpClientOfCreatio.GetObjsByIds<ITIS.ITISEmploymentType>(employmentTypesIds);

            foreach (var career in careers)
            {
                if (career.ITISEmploymentTypeId.IsNotNullOrDefault())
                    career.ITISEmploymentType = employmentTypes[(Guid)career.ITISEmploymentTypeId];
            }
        }


        /// <summary> Получить из Creatio контрагентов и связать с карьерой контакта </summary>
        public static void Creatio_GetAccountsAndBindWithContactCareers(List<ITIS.ContactCareer> careers)
        {
            HashSet<Guid> accountIds = new HashSet<Guid>(careers.SelectValuableGuids(x => x.AccountId));
            Dictionary<Guid, ITIS.Account> accounts = HttpClientOfCreatio.GetObjsByIds<ITIS.Account>(accountIds);

            foreach (var career in careers)
            {
                if (career.AccountId.IsNotNullOrDefault())
                    career.Account = accounts[(Guid)career.AccountId];
            }
        }



        /// <summary> Получить из Creatio елементы орг. структуры и привязать к карьере контакта </summary>
        public static void Creatio_GetOrgStructureUnitsAndBindWithContactCareers(List<ITIS.ContactCareer> careers)
        {
            HashSet<Guid> orgStructUnitsIds = new HashSet<Guid>(careers.SelectValuableGuids(x => x.ITISSubdivisionId));
            Dictionary<Guid, ITIS.OrgStructureUnit> orgStructUnits = HttpClientOfCreatio.GetObjsByIds<ITIS.OrgStructureUnit>(orgStructUnitsIds);

            foreach (var career in careers)
            {
                if (career.ITISSubdivisionId.IsNotNullOrDefault())
                    career.ITISSubdivision = orgStructUnits[(Guid)career.ITISSubdivisionId];
            }
        }



        /// <summary> Получить из Creatio должности и привязать к карьере сотрудника </summary>
        public static void Creatio_GetJobAndBindWithContactCareers(List<ITIS.ContactCareer> careers)
        {
            HashSet<Guid> jobIds = new HashSet<Guid>(careers.SelectValuableGuids(x => x.JobId));
            Dictionary<Guid, ITIS.Job> jobs = HttpClientOfCreatio.GetObjsByIds<ITIS.Job>(jobIds);

            foreach (var career in careers)
            {
                if (career.JobId.IsNotNullOrDefault())
                    career.Job = jobs[(Guid)career.JobId];
            }
        }
    }
}
