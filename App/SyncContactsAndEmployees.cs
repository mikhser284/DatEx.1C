using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OneC = DatEx.OneC.DataModel;
using ITIS = DatEx.Creatio.DataModel.ITIS;
using Terrasoft = DatEx.Creatio.DataModel.Terrasoft.Base;
using System.Runtime;
using DatEx.OneC.DataModel;
using System.Runtime.CompilerServices;
using DatEx.Creatio.DataModel.ITIS;
using DatEx.Creatio;
using System.Threading;
using System.Security.Cryptography.X509Certificates;

namespace App
{
    /// <summary> Первичная синхронизация контактов, сотрудников и контактной информации </summary>
    public partial class Program
    {
        public static void SyncEmployees(SyncSettings settings)
        {
            Task<Dictionary<Guid, OneC.Person>> oneS_getPersonsInfo = new Task<Dictionary<Guid, Person>>(() => OneC_GetPersonsAndRelatedInfoForSycn(settings));
            
            Task[] tasks = new Task[]
            {
                new Task(() => Creatio_ClearSyncObjects()),
                oneS_getPersonsInfo
            }.StartAndWaitForAll();


            Dictionary<Guid, OneC.Person> personsWithEmailsOrderedById = oneS_getPersonsInfo.Result;
            OneC_ShowPersonFullInfo(personsWithEmailsOrderedById.Values.Take(2));
            personsWithEmailsOrderedById.Values.ForEach(person => Console.WriteLine(person.GetShortNameAndActualPositions()));

            Creatio_EmployeesFirstSyncWithOneS(personsWithEmailsOrderedById, settings);


            //Dictionary<Guid, ITIS.Contact> creatio_Contacts = Creatio_GetContactsAcordingToPersonEmail(personsWithEmailsOrderedById);
            //var creatioContact = creatio_Contacts.Skip(1).FirstOrDefault().Value;
            //Creatio_ShowContact(creatioContact);
        }


        /// <summary> Получить из 1С физ. лица и всю связанную с ними информацию необходимою для синхронизации </summary>
        public static Dictionary<Guid, OneC.Person> OneC_GetPersonsAndRelatedInfoForSycn(SyncSettings settings = null)
        {
            settings ??= SyncSettings.GetDefaultSettings();
            Dictionary<Guid, OneC.Person> personsWithEmailsOrderedById = OneC_GetPersonsWithSufficientEmail(settings);

            Task[] tasks = new Task[]
            {
                new Task(() => OneC_GetContactInfoAndBindWithPersons(personsWithEmailsOrderedById, settings)),
                new Task(() => OneC_GetNamesAndBindWithPersons(personsWithEmailsOrderedById)),
                new Task(() => OneC_GetEmployeesAndBindWithPersons(personsWithEmailsOrderedById)),
            }.StartAndWaitForAll();
            //
            return personsWithEmailsOrderedById;
        }

        /// <summary> Удалить из Creatio все синхронизированные ранее объекты </summary>
        public static void Creatio_ClearSyncObjects()
        {
            String defaultGuid = default(Guid).ToString();
            
            var res = CreatioHttpClient.GetObjs<ITIS.Contact>($"filter=ITISOneSId ne null and ITISOneSId ne {defaultGuid}");
            res.ForEach(x => CreatioHttpClient.DeleteObj(x));
        }

        public static void Creatio_EmployeesFirstSyncWithOneS(Dictionary<Guid, OneC.Person> persons, SyncSettings settings)
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

            SyncTemporaryObjects temp = new SyncTemporaryObjects();
            temp.EmploymentTypes = CreatioHttpClient.GetObjsByIds<ITIS.ITISEmploymentType>(settings.MapEmploymentTypeEnumInOneS_EmploymentTypeGuidInCreatio.Values);

            foreach (Person p in persons.Values)
            {
                ITIS.Contact c = Creatio_FirstSyncOfEmployeesInfo(p, settings, temp);
                contactIds.Add((Guid)c.Id);
            }
            contacts = Creatio_GetCreatedContactsAndRelatedInfo(contactIds).Values.ToList();
            contacts.ForEach(x => Creatio_ShowContact(x));
        }

        private static ITIS.Contact Creatio_FirstSyncOfEmployeesInfo(Person p, SyncSettings settings, SyncTemporaryObjects temp)
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
            c.GenderId = settings.MapGenderInOneC_GenderGuidInCreatio[p.Gender];
            c.Email = p.RelatedObj_ContactInfoEmail.View;
            c.Phone = p.RelatedObj_ContactInfoWorkPhone.View;
            c.MobilePhone = p.RelatedObj_ContactInfoPhone.View;
            
            c.ITISCareeStartDate = (DateTime)p.RelatedObjs_RelatedEmployeePositions.Min(x => x.DateOfEmployment);
            Boolean isActualEmployee = p.RelatedObjs_RelatedEmployeePositions.Any(x => x.DateOfDismisal == null || x.DateOfDismisal == default(DateTime));
            c.ITISCareerEndDate = isActualEmployee ? null : (DateTime?)p.RelatedObjs_RelatedEmployeePositions.Max(x => x.DateOfDismisal);
            ITIS.Contact contact = CreatioHttpClient.CreateObj(c);

            List<OneC.Employee> orderedPositions = p.RelatedObjs_RelatedEmployeePositions.OrderByDescending(x => x.DateOfEmployment).ToList();
            OneC.Employee lastActualPrimaryPosition = null;
            OneC.Employee lastPosition = null;

            foreach(var position in orderedPositions)
            {
                if(lastActualPrimaryPosition == null && (position.DueDate == null || position.DueDate == default(DateTime)))
                {
                    if (position.TypeOfEmployment == "ОсновноеМестоРаботы") lastActualPrimaryPosition = position;
                    if(lastPosition == null) lastPosition = position;
                }
            }

            // Если возможно то присвоить последнюю актуальную должность с основным местом работы
            if (lastActualPrimaryPosition != null) lastPosition = lastActualPrimaryPosition;
            // Если актуальных должностей вообще нет просто задать самую крайнюю
            if (lastPosition == null) lastPosition = orderedPositions.FirstOrDefault();
            if (lastPosition != null)
            {
                c.ITISEmploymentTypeId = settings.MapEmploymentTypeEnumInOneS_EmploymentTypeGuidInCreatio[lastPosition.TypeOfEmployment];
                c.JobTitle = lastPosition.NavProp_Position.Description;
                ITIS.EmployeeJob job = new ITIS.EmployeeJob();
                job.Name = lastPosition.NavProp_Position.Description;
                job.ITISOneSId = lastPosition.NavProp_Position.Id;
                job.
                c.ITISEmployeePosition = CreatioHttpClient.CreateObj(job);
                c.ITISEmployeePositionId = c.ITISEmployeePosition.Id;
            }

            contact = CreatioHttpClient.CreateObj(c);
            ITIS.Employee employee = Creatio_AddNewEmployee(contact, p);
            return contact;
        }

        private static ITIS.Employee Creatio_AddNewEmployee(ITIS.Contact c, Person p)
        {
            ITIS.Employee e = CreatioHttpClient.GetObjsByIds<ITIS.Employee>(new List<Guid> { (Guid)c.Id }, "Contact").Values.FirstOrDefault();
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
        public static void OneC_ShowPersonFullInfo(IEnumerable<OneC.Person> persons)
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

            foreach (OneC.Person person in persons)
            {
                person.Show();
                person.RelatedObj_NameInfo?.Show(1);
            
                person.RelatedObj_ContactInfoEmail?.Show(1);
                person.RelatedObj_ContactInfoEmail?.RelatedObj_TypeOfContactInfo?.Show(2);

                person.RelatedObj_ContactInfoWorkPhone?.Show(1);
                person.RelatedObj_ContactInfoWorkPhone?.RelatedObj_TypeOfContactInfo?.Show(2);

                person.RelatedObj_ContactInfoPhone?.Show(1);
                person.RelatedObj_ContactInfoPhone?.RelatedObj_TypeOfContactInfo?.Show(2);

                person.RelatedObjs_RelatedEmployeePositions?.ForEach(employee => employee.Show(1));
            }
        }

        /// <summary> Получить из 1С физ. лица с подходящим email </summary>
        public static Dictionary<Guid, OneC.Person> OneC_GetPersonsWithSufficientEmail(SyncSettings settings)
        {
            // Получаем идентификаторы физ. лиц с почтовыми адресами, которые оканчиваются на указанный домен
            List<Guid> idsPersonsWithEmails = OneCHttpClient.GetIdsOfObjs<OneC.IRContactInfo>(
                "$filter=Объект_Type eq 'StandardODATA.Catalog_ФизическиеЛица'"
                + $" and cast(Вид, 'Catalog_ВидыКонтактнойИнформации') eq guid'{settings.OneCGuidOfEmailContactInfo}'"
                + $" and endswith(Представление, '{settings.EmailDomain}') eq true", "Объект");

            // Получаем объекты физ. лиц
            Dictionary<Guid, OneC.Person> personsWithEmails = OneCHttpClient.GetObjsByIds<OneC.Person>(idsPersonsWithEmails).ToDictionary(k => k.Id);
            return personsWithEmails;
        }

        /// <summary> Получить из 1С контактную информацию и связать ее с физ. лицам </summary>
        public static void OneC_GetContactInfoAndBindWithPersons(Dictionary<Guid, OneC.Person> personsWithEmailsOrderedById, SyncSettings settings)
        {
            Dictionary<Guid, List<OneC.IRContactInfo>> contactInfosGroupedByPersonId = OneCHttpClient
                .GetObjsByIds<OneC.IRContactInfo>(personsWithEmailsOrderedById.Keys, "cast(Объект, 'Catalog_ФизическиеЛица')")
                .GroupToDictionaryBy(x => new Guid(x.KeyObject));
            OneC_GetAndBindContactInfoTypes(contactInfosGroupedByPersonId);
            foreach(OneC.Person person in personsWithEmailsOrderedById.Values)
            {
                List<OneC.IRContactInfo> contactInfoOfPerson = contactInfosGroupedByPersonId[person.Id];
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
        static void OneC_GetAndBindContactInfoTypes(Dictionary<Guid, List<OneC.IRContactInfo>> personIdAndContactInfos)
        {
            Dictionary<Guid, OneC.ContactInfoType> contactInfoTypes;
            {
                HashSet<Guid> idsOfcontactInfoTypes = new HashSet<Guid>();
                personIdAndContactInfos.ForEach(key => key.Value.ForEach(value => idsOfcontactInfoTypes.Add(new Guid(value.KeyKind))));
                contactInfoTypes = OneCHttpClient.GetObjsByIds<OneC.ContactInfoType>(idsOfcontactInfoTypes).ToDictionary(k => k.Id);
            }
            personIdAndContactInfos.ForEach(key => key.Value.ForEach(value => value.RelatedObj_TypeOfContactInfo = contactInfoTypes[new Guid(value.KeyKind)]));
        }

        /// <summary> Получить из 1С данные из инфо. регистра InformationRegister_ФИОФизЛиц и связываем з физлицами </summary>
        public static void OneC_GetNamesAndBindWithPersons(Dictionary<Guid, OneC.Person> personsWithEmailsOrderedById)
        {
            List<OneC.IRNamesOfPersons> namesOfPersons = OneCHttpClient
                .GetObjsByIds<OneC.IRNamesOfPersons>(personsWithEmailsOrderedById.Keys, "cast(ФизЛицо, 'Catalog_ФизическиеЛица')");
            foreach (var personName in namesOfPersons)
                personsWithEmailsOrderedById[personName.KeyPerson].RelatedObj_NameInfo = personName;
        }

        /// <summary> Получить из 1С сотрудников и связать с физ. лицами </summary>
        public static void OneC_GetEmployeesAndBindWithPersons(Dictionary<Guid, OneC.Person> personsWithEmailsOrderedById)
        {
            // Связать сотрудников с физ. лицами
            List<OneC.Employee> employeesWithEmails = OneCHttpClient.GetObjsByIds<OneC.Employee>(personsWithEmailsOrderedById.Keys, "Физлицо_Key");
            employeesWithEmails.ForEach(employee => employee.NavProp_Person = personsWithEmailsOrderedById[(Guid)employee.PersonId]);

            // Связать физ. лиц с сотрудниками
            employeesWithEmails.ForEach(employee => personsWithEmailsOrderedById[(Guid)employee.PersonId].RelatedObjs_RelatedEmployeePositions.Add(employee));

            Task[] tasks = new Task[]
            {
                new Task(() => OneC_GetPositionsAndBindWithEmployees(employeesWithEmails)),
                new Task(() => OneC_GetOrganizationsAndBindWithEmployees(employeesWithEmails)),
                new Task(() => OneC_GetSubdivisionsAndBindWithEmployees(employeesWithEmails))
            }.StartAndWaitForAll();
        }

        /// <summary> Получить из 1С должности и связать их с сотрудниками </summary>
        static void OneC_GetPositionsAndBindWithEmployees(List<OneC.Employee> employeesWithEmails)
        {
            Dictionary<Guid, OneC.PositionInOrganization> positions = null;
            {
                List<Guid> listOfpositions = employeesWithEmails.DistinctValuesExcluding(default(Guid), x => x.PositionId).Cast<Guid>().ToList();
                listOfpositions.AddRange(employeesWithEmails.DistinctValuesExcluding(default(Guid), x => x.CurrentCompanyPositionId).Cast<Guid>());
                listOfpositions.AddRange(employeesWithEmails.DistinctValuesExcluding(default(Guid), x => x.CurrentPositionInOrganizationId).Cast<Guid>());
                positions = OneCHttpClient.GetObjsByIds<OneC.PositionInOrganization>(listOfpositions.DistinctValuesExcluding(default(Guid), x => x).Cast<Guid>())
                    .ToDictionary(k => k.Id);
            }
            foreach (var employee in employeesWithEmails)
            {

                if (employee.PositionId.IsNotNullOrDefault())
                    employee.NavProp_Position = positions[(Guid)employee.PositionId];
                if (employee.CurrentCompanyPositionId.IsNotNullOrDefault())
                    employee.NavProp_CurrentCompanyPosition = positions[(Guid)employee.CurrentCompanyPositionId];
                if (employee.CurrentPositionInOrganizationId.IsNotNullOrDefault())
                    employee.NavProp_CurrentPositionInOrganization = positions[(Guid)employee.CurrentPositionInOrganizationId];
            }
        }

        /// <summary> Получить из 1С организации и связать их с сотрудниками </summary>
        static void OneC_GetOrganizationsAndBindWithEmployees(List<OneC.Employee> employeesWithEmails)
        {
            Dictionary<Guid, OneC.Organization> organizations = OneCHttpClient.GetObjs<OneC.Organization>().ToDictionary(k => k.Id);
            foreach (var employee in employeesWithEmails)
            {
                if (employee.OrganizationId.IsNotNullOrDefault())
                    employee.NavProp_Organization = organizations[(Guid)employee.OrganizationId];
            }
        }

        /// <summary> Получить из 1С подразделения и связать их с сотрудниками </summary>
        static void OneC_GetSubdivisionsAndBindWithEmployees(List<OneC.Employee> employeesWithEmails)
        {

            List<Guid> subdivisionIds = employeesWithEmails.DistinctValuesExcluding(default(Guid), x => x.OrganizationSubdivisionId).Cast<Guid>().ToList();
            subdivisionIds.AddRange(employeesWithEmails.DistinctValuesExcluding(default(Guid), x => x.CurrentOrganizationSubdivisionId).Cast<Guid>());
            Dictionary<Guid, OneC.OrganizationSubdivision> subdivisions = OneCHttpClient.GetObjsByIds<OneC.OrganizationSubdivision>(subdivisionIds).ToDictionary(k => k.Id);
            foreach (var employee in employeesWithEmails)
            {
                if (employee.OrganizationSubdivisionId.IsNotNullOrDefault())
                    employee.NavProp_OrganizationSubdivision = subdivisions[(Guid)employee.OrganizationSubdivisionId];
                if (employee.CurrentOrganizationSubdivisionId.IsNotNullOrDefault())
                    employee.NavProp_CurrentOrganizationSubdivision = subdivisions[(Guid)employee.CurrentOrganizationSubdivisionId];
            }
        }
    }

    /// <summary> Получение объектов из Creatio </summary>
    public partial class Program
    {
        /// <summary> Получить из Creatio контакты по email физ.лиц из 1C </summary>
        public static Dictionary<Guid, ITIS.Contact> Creatio_GetCreatedContactsAndRelatedInfo(HashSet<Guid> contactsIds)
        {
            //List<String> emails = personsWithEmailsOrderedById.Values.Select(x => x.RelatedObj_ContactInfoEmail.View).Distinct().ToList();
            //List<ITIS.Contact> creatio_Contacts = CreatioHttpClient.GetObjsWherePropIn<ITIS.Contact, String>("Email", emails);
            Dictionary<Guid, ITIS.Contact> creatio_ContactsOrderedById = CreatioHttpClient.GetObjsByIds<ITIS.Contact>(contactsIds);
            List<ITIS.Contact> creatio_Contacts = creatio_ContactsOrderedById.Values.ToList();

            Task[] tasks = new Task[]
            {
                new Task(() => Creatio_GetEmployeeJobAndBindWithContact(creatio_Contacts)),
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

        /// <summary> Получить из Creatio сотрудников и связать с контактами </summary>
        public static void Creatio_GetEmployeeAndBindWithContact(Dictionary<Guid, ITIS.Contact> creatio_ContactsOrderedById)
        {
            Dictionary<Guid, ITIS.Employee> employeesOrderedById = CreatioHttpClient.GetObjsByIds<ITIS.Employee>(creatio_ContactsOrderedById.Values.Select(x => (Guid)x.Id), "Contact");
            
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
            Dictionary<Guid, Terrasoft.Gender> genders = CreatioHttpClient.GetObjsByIds<Terrasoft.Gender>(creatio_Contacts.Select(x => (Guid)x.GenderId));
            foreach(var contact in creatio_Contacts)
            {
                if (contact.GenderId.IsNotNullOrDefault())
                    contact.Gender = genders[(Guid)contact.GenderId];
            }
        }

        /// <summary> Получить из Creatio должности и связать с контактами </summary>
        public static void Creatio_GetEmployeeJobAndBindWithContact(List<ITIS.Contact> creatio_Contacts)
        {
            Dictionary<Guid, ITIS.EmployeeJob> jobs = CreatioHttpClient.GetObjsByIds<ITIS.EmployeeJob>(creatio_Contacts.SelectValuableGuids(x => x.ITISEmployeePositionId));

            foreach(var contact in creatio_Contacts)
            {
                if (contact.ITISEmployeePositionId.IsNotNullOrDefault())
                    contact.ITISEmployeePosition = jobs[(Guid)contact.ITISEmployeePositionId];
            }
        }

        /// <summary> Получить из Creatio подразделения и связать с сотрудниками </summary>
        public static void Creatio_GetSubdivisionsAndBindWithEmployees(List<ITIS.Contact> creatio_Contacts)
        {
            Dictionary<Guid, Terrasoft.AccountOrganizationChart> subdivisions = CreatioHttpClient.GetObjsByIds<Terrasoft.AccountOrganizationChart>(creatio_Contacts.SelectValuableGuids(x => x.ITISSubdivisionId));
            
            foreach(var contact in creatio_Contacts)
            {
                if (contact.ITISSubdivisionId.IsNotNullOrDefault())
                    contact.ITISSubdivision = subdivisions[(Guid)contact.ITISSubdivisionId];
            }
        }

        /// <summary> Получить из Creatio тип зайнятости и связать с контактами </summary>
        public static void Creatio_GetEmploymentTypesAndBindWitEmployees(List<ITIS.Contact> creatio_Contacts)
        {
            Dictionary<Guid, ITISEmploymentType> employmentTypes = CreatioHttpClient.GetObjsByIds<ITISEmploymentType>(creatio_Contacts.SelectValuableGuids(x => x.ITISEmploymentTypeId));
            foreach(var contact in creatio_Contacts)
            {
                if (contact.ITISEmploymentTypeId.IsNotNullOrDefault())
                    contact.ITISEmploymentType = employmentTypes[(Guid)contact.ITISEmploymentTypeId];
            }
        }

        /// <summary> Получить из Creatio подразделения организаций зайнятости и связать с контактами </summary>
        public static void Creatio_GetSubdivisionAndBindWithContact(List<ITIS.Contact> creatio_Contacts)
        {
            Dictionary<Guid, Terrasoft.OrgStructureUnit> organizationSubdivisions = CreatioHttpClient.GetObjsByIds<Terrasoft.OrgStructureUnit>(creatio_Contacts.SelectValuableGuids(x => x.ITISOrganizationSubdivisionId));
            foreach(var contact in creatio_Contacts)
            {
                if (contact.ITISOrganizationSubdivisionId.IsNotNullOrDefault())
                    contact.ITISOrganizationSubdivision = organizationSubdivisions[(Guid)contact.ITISOrganizationSubdivisionId];
            }
        }

        /// <summary> Получить из Creatio контрагентов и связать с контактами </summary>
        public static void Creaio_GetAccountsAndBindWithContacts(List<ITIS.Contact> creatio_Contacts)
        {
            Dictionary<Guid, ITIS.Account> accounts = CreatioHttpClient.GetObjsByIds<ITIS.Account>(creatio_Contacts.SelectValuableGuids(x => x.AccountId));
            foreach(var contact in creatio_Contacts)
            {
                if (contact.AccountId.IsNotNullOrDefault())
                    contact.Account = accounts[(Guid)contact.AccountId];
            }
        }

        /// <summary> Получить из Creatio типы контактов и связать с контактами </summary>
        public static void Creatio_GetContactTypesAndBindWithContacts(List<ITIS.Contact> creatio_Contacts)
        {
            Dictionary<Guid, Terrasoft.ContactType> contactTypes = CreatioHttpClient.GetObjsByIds<Terrasoft.ContactType>(creatio_Contacts.Select(x => (Guid)x.TypeId));
            foreach(var contact in creatio_Contacts)
            {
                if (contact.TypeId.IsNotNullOrDefault())
                    contact.Type = contactTypes[(Guid)contact.TypeId];
            }
        }

        /// <summary> Получить из Creatio должности и связать с контактами </summary>
        public static void Creatio_GetJobsAndBindWithContacts(List<ITIS.Contact> creatio_Contacts)
        {
            Dictionary<Guid, Terrasoft.Job> jobs = CreatioHttpClient.GetObjsByIds<Terrasoft.Job>(creatio_Contacts.SelectValuableGuids(x => x.JobId));
            foreach(var contact in creatio_Contacts)
            {
                if (contact.JobId.IsNotNullOrDefault())
                    contact.Job = jobs[(Guid)contact.JobId];
            }
        }

        /// <summary> Получить из Creatio подразделения и связать с контактами </summary>
        public static void Creatio_GetDepartmentsAndBindWithContacts(List<ITIS.Contact> creatio_Contacts)
        {
            Dictionary<Guid, Terrasoft.Department> departmentns = CreatioHttpClient.GetObjsByIds<Terrasoft.Department>(creatio_Contacts.SelectValuableGuids(x => x.DepartmentId));
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
            Dictionary<Guid, List<ITIS.EmployeeCareer>> careersGroupedByEmployeeId = CreatioHttpClient.GetBindedObjsByParentIds<ITIS.EmployeeCareer>(employeesOrderedById.Keys, "Employee", x => x.EmployeeId);
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
                new Task(() => Creatio_GetAccountsAndBindWithCareers(careers)),
                new Task(() => Creatio_GetOrgStructureUnitsAndBindWithCareers(careers)),
                new Task(() => Creatio_GetJobAndBindWithCareers(careers)),
            }.StartAndWaitForAll();
        }

        /// <summary> Получить из Creatio типы видов зайнятости и связать с карьерой сотрудника </summary>
        public static void Creatio_GetEmployementTypeAndBindWithEmploymentCareer(List<ITIS.EmployeeCareer> careers)
        {
            HashSet<Guid> employmentTypesIds = new HashSet<Guid>(careers.Select(x => x.ITISTypeOfEmploymentId));
            Dictionary<Guid, ITIS.ITISEmploymentType> employmentTypes = CreatioHttpClient.GetObjsByIds<ITIS.ITISEmploymentType>(employmentTypesIds);

            foreach(var career in careers)
            {
                if (career.ITISTypeOfEmploymentId.IsNotDefault())
                    career.ITISTypeOfEmployment = employmentTypes[career.ITISTypeOfEmploymentId];
            }
        }

        /// <summary> Получить из Creatio контрагентов и связать с карьерой сотрудника </summary>
        public static void Creatio_GetAccountsAndBindWithCareers(List<ITIS.EmployeeCareer> careers)
        {
            HashSet<Guid> accountIds = new HashSet<Guid>(careers.Select(x => x.AccountId));
            Dictionary<Guid, Terrasoft.Account> accounts = CreatioHttpClient.GetObjsByIds<Terrasoft.Account>(accountIds);

            foreach(var career in careers)
            {
                if (career.AccountId.IsNotDefault())
                    career.Account = accounts[career.AccountId];
            }
        }

        /// <summary> Получить из Creatio елементы орг. структуры и привязать к карьере сотрудника </summary>
        public static void Creatio_GetOrgStructureUnitsAndBindWithCareers(List<ITIS.EmployeeCareer> careers)
        {
            HashSet<Guid> orgStructUnitsIds = new HashSet<Guid>(careers.Select(x => x.OrgStructureUnitId));
            Dictionary<Guid, Terrasoft.OrgStructureUnit> orgStructUnits = CreatioHttpClient.GetObjsByIds<Terrasoft.OrgStructureUnit>(orgStructUnitsIds);

            foreach(var career in careers)
            {
                if (career.OrgStructureUnitId.IsNotDefault())
                    career.OrgStructureUnit = orgStructUnits[career.OrgStructureUnitId];
            }
        }

        /// <summary> Получить из Creatio должности и привязать к карьере сотрудника </summary>
        public static void Creatio_GetJobAndBindWithCareers(List<ITIS.EmployeeCareer> careers)
        {
            HashSet<Guid> jobIds = new HashSet<Guid>(careers.Select(x => x.EmployeeJobId));
            Dictionary<Guid, ITIS.EmployeeJob> jobs = CreatioHttpClient.GetObjsByIds<ITIS.EmployeeJob>(jobIds);

            foreach(var career in careers)
            {
                if (career.EmployeeJobId.IsNotDefault())
                    career.EmployeeJob = jobs[career.EmployeeJobId];
            }
        }

        public static void Creatio_ShowContact(ITIS.Contact contact)
        {
            contact?.Show();
            
            ITIS.Employee employee = (ITIS.Employee)contact?.Employee;
            employee?.Show(1);
            employee?.Career.ForEach(x => x.Show(2));
            
            contact?.ITISSubdivision?.Show(1);
            contact?.ITISEmploymentType?.Show(1);
            contact?.ITISOrganizationSubdivision?.Show(1);
            contact?.Account?.Show(1);
            contact?.Type?.Show(1);
            contact?.Job?.Show(1);
            contact?.Department?.Show(1);

        }
    }
}
