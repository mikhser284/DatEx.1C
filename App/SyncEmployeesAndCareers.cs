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
using System.IO;

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

            var departments = HttpClientOfCreatio.GetObjs<ITIS.Department>(removableObjsQuery);
            var accountOrgChartsGroup = HttpClientOfCreatio.GetBindedObjsByParentIds<Terrasoft.AccountOrganizationChart>(departments.Select(x => (Guid)x.Id), "Department", x => x.DepartmentId);
            Dictionary<Guid, Terrasoft.AccountOrganizationChart> removableAccountOrgCharts = new Dictionary<Guid, Terrasoft.AccountOrganizationChart>();
            foreach(var items in accountOrgChartsGroup.Values)
                foreach (var item in items) 
                    if (!removableAccountOrgCharts.ContainsKey((Guid)item.Id)) removableAccountOrgCharts.Add((Guid)item.Id, item);
            
            removableAccountOrgCharts.Values.ForEach(x => HttpClientOfCreatio.DeleteObj(x));
            departments.ForEach(x => HttpClientOfCreatio.DeleteObj(x));

            var employeeCareers = HttpClientOfCreatio.GetObjs<ITIS.EmployeeCareer>(removableObjsQuery);
            employeeCareers.ForEach(x => HttpClientOfCreatio.DeleteObj(x));

            var contactCareers = HttpClientOfCreatio.GetObjs<ITIS.ContactCareer>(removableObjsQuery);
            contactCareers.ForEach(x => HttpClientOfCreatio.DeleteObj(x));
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
            

            Creatio_EmployeesInfo_MapObjectsFromOneSToObjectsFromCreatio(syncObjs, settings);

            contactIds = new HashSet<Guid>(syncObjs.Creatio_ContractsOrderedByOneSId.Values.Select(x => (Guid)x.Id));
            contacts = Creatio_GetCreatedContactsAndRelatedInfo(contactIds).Values.ToList();
            contacts.ForEach(x => Creatio_ShowContact(x));
        }

        
        private static void Creatio_EmployeesInfo_MapObjectsFromOneSToObjectsFromCreatio(SyncObjs syncObjs, SyncSettings settings)
        {
            Creatio_MapObj_Account(syncObjs, settings);
            Creatio_MapObj_OrgStructureUnit(syncObjs, settings);
            Creatio_MapObj_AccountOrganizationChart(syncObjs, settings);
            Creatio_MapObj_Job(syncObjs, settings);
            Creatio_MapObj_EmployeeJob(syncObjs, settings);
            Creatio_MapObj_Contacts(syncObjs, settings);
            Creataio_MapObj_Employee(syncObjs, settings);

            Creatio_MapObj_ContactCareer(syncObjs, settings);
            Creatio_MapObj_EmployeeCareer(syncObjs, settings);
        }



        private static void Creatio_MapObj_ContactCareer(SyncObjs syncObjs, SyncSettings settings)
        {
            ITIS.ContactCareer c;
            var deletableCareers = HttpClientOfCreatio.GetDistinctObjsWithPropValueIn<ITIS.ContactCareer, Guid>($"{nameof(c.Contact)}/Id", syncObjs.Creatio_ContractsOrderedByOneSId.Values.Select(e => (Guid)e.Id));
            deletableCareers.Values.ForEach(x => HttpClientOfCreatio.DeleteObj(x));

            const string valueOfPrimaryEmploymentTypeEnum = "ОсновноеМестоРаботы";

            foreach (Person person in syncObjs.OneS_PersonsOrderedById.Values)
            {
                ITIS.Contact contact = syncObjs.Creatio_ContractsOrderedByOneSId[person.Id];                

                foreach (OneS.Employee pos in person.RelatedObjs_RelatedEmployeePositions)
                {
                    ITIS.ContactCareer career = new ContactCareer();
                    //
                    //
                    career.ContactId = contact.Id;
                    career.ITISOneSId = pos.Id;
                    career.AccountId = syncObjs.Creatio_Accounts_ByOneSId[pos.NavProp_Organization.Id].Id;
                    career.StartDate = pos.DateOfEmployment;
                    career.DueDate = pos.DateOfDismisal.GetNotDefaultValue();
                    career.Current = pos.DateOfDismisal.IsNotNullOrDefault() ? false : true;
                    career.JobTitle = pos.NavProp_CurrentPositionInOrganization?.Description;
                    career.Primary = pos.TypeOfEmployment == valueOfPrimaryEmploymentTypeEnum;
                    career.ITISEmploymentTypeId = settings.Map_OneSEnum_EmploymentType_CreatioGuidOf_EmploymentType[pos.TypeOfEmployment];
                    career.DepartmentId = syncObjs.Creatio_Departaments_ByOneSId[pos.NavProp_CurrentOrganizationSubdivision.Id].Id;
                    career.JobId = syncObjs.Creatio_Jobs_ByOneSId[pos.NavProp_CurrentPositionInOrganization.Id].Id;
                    //
                    //
                    career = HttpClientOfCreatio.CreateObj<ITIS.ContactCareer>(career);
                    List<ITIS.ContactCareer> contactCareers = new List<ContactCareer>();
                    if (!syncObjs.Creatio_ContactCareersGrouppedByContactId.TryGetValue((Guid)contact.Id, out contactCareers))
                        syncObjs.Creatio_ContactCareersGrouppedByContactId.Add((Guid)contact.Id, new List<ContactCareer> { career });
                    else contactCareers.Add(career);
                }
            }
        }

        private static void Creatio_MapObj_EmployeeCareer(SyncObjs syncObjs, SyncSettings settings)
        {
            ITIS.EmployeeCareer c;
            var deletableCareers = HttpClientOfCreatio.GetDistinctObjsWithPropValueIn<ITIS.EmployeeCareer, Guid>($"{nameof(c.Employee)}/Id", syncObjs.Creatio_EmployeesOrderedByContactId.Values.Select(e => (Guid)e.Id));

            foreach(var del in deletableCareers.Values)
            {
                try
                {
                    HttpClientOfCreatio.DeleteObj(del);
                }
                catch(Exception)
                {
                    //? Почему система не позволяет нормально удалить автоматически созданные объекты
                    Console.WriteLine("!!! При попытке удалить объек карьера сотрудника возникло исключение");
                }
            }

            const string valueOfPrimaryEmploymentTypeEnum = "ОсновноеМестоРаботы";
            
            foreach(Person person in syncObjs.OneS_PersonsOrderedById.Values)
            {
                ITIS.Contact contact = syncObjs.Creatio_ContractsOrderedByOneSId[person.Id];                
                ITIS.Employee employee = syncObjs.Creatio_EmployeesOrderedByContactId[(Guid)contact.Id];

                
                foreach(OneS.Employee pos in person.RelatedObjs_RelatedEmployeePositions)
                {
                    ITIS.EmployeeCareer career = new EmployeeCareer();
                    //
                    //
                    career.EmployeeId = employee.Id;
                    career.ITISOneSId = pos.Id;
                    career.AccountId = syncObjs.Creatio_Accounts_ByOneSId[pos.NavProp_Organization.Id].Id;
                    career.StartDate = pos.DateOfEmployment;
                    career.DueDate = pos.DateOfDismisal.GetNotDefaultValue();
                    //? При попытке установить значение true - Internal server error, либо же система не реагирует никак
                    career.IsCurrent = pos.DateOfDismisal.IsNotNullOrDefault() ? false : true;
                    career.FullJobTitle = pos.NavProp_CurrentPositionInOrganization?.Description;
                    career.ITISPrimary = pos.TypeOfEmployment == valueOfPrimaryEmploymentTypeEnum;
                    career.ITISTypeOfEmploymentId = settings.Map_OneSEnum_EmploymentType_CreatioGuidOf_EmploymentType[pos.TypeOfEmployment];
                    career.OrgStructureUnitId = syncObjs.Creatio_OrgStructureUnits_ByOneSId[pos.NavProp_CurrentOrganizationSubdivision.Id].Id;
                    career.EmployeeJobId = syncObjs.Creatio_EmployeeJobs_ByOneSId[pos.NavProp_CurrentPositionInOrganization.Id].Id;
                    //
                    //
                    career = HttpClientOfCreatio.CreateObj<ITIS.EmployeeCareer>(career);
                    List<ITIS.EmployeeCareer> employeeCareers = new List<EmployeeCareer>();
                    if (!syncObjs.Creatio_EmployeeCareersGrouppedByEmployeeId.TryGetValue((Guid)employee.Id, out employeeCareers))
                        syncObjs.Creatio_EmployeeCareersGrouppedByEmployeeId.Add((Guid)employee.Id, new List<EmployeeCareer> { career });
                    else employeeCareers.Add(career);
                }
            }
        }

        private static void Creataio_MapObj_Employee(SyncObjs syncObjs, SyncSettings settings)
        {
            // Создаеться ли объект Сотрудник автоматически при создании соответствующего типа конракта
            Boolean autoCreationOfEmployeesActivated = true;
            if(autoCreationOfEmployeesActivated)
            {
                // только получаем уже созданные объекты из Creatio
                ITIS.Employee e;
                var employees = HttpClientOfCreatio.GetDistinctObjsWithPropValueIn<ITIS.Employee, Guid>($"{nameof(e.Contact)}/Id", syncObjs.Creatio_ContractsOrderedByOneSId.Values.Select(x => (Guid)x.Id)).Values.ToList();
                syncObjs.Creatio_EmployeesOrderedByContactId = employees.ToDictionary(k => (Guid)k.ContactId);
                return;
            }

            //Создаем соттудников в Creatio
            foreach(ITIS.Contact c in syncObjs.Creatio_ContractsOrderedByOneSId.Values)
            {
                ITIS.Employee e = new ITIS.Employee();
                //
                e.ContactId = c.Id;
                //e.Name = $"{c.Surname} {c.GivenName} {c.MiddleName}";
                e.OrgStructureUnitId = c.ITISOrganizationSubdivisionId;
                e.Notes = c.Notes;
                e.JobId = c.JobId;
                e.FullJobTitle = c.JobTitle;
                e.CareerStartDate = c.ITISCareeStartDate;
                e.CareerDueDate = c.ITISCareerEndDate;
                e.AccountId = c.AccountId;
                e.ITISSurName = c.Surname;
                e.ITISGivenName = c.GivenName;
                e.ITISMiddleName = c.MiddleName;
                e.ITISEmploymentsTypeId = c.ITISEmploymentTypeId;
                //
                e = HttpClientOfCreatio.CreateObj(e);
                syncObjs.Creatio_EmployeesOrderedByContactId.Add((Guid)e.ContactId, e);
            }
        }

        private static void Creatio_MapObj_Contacts(SyncObjs syncObjs, SyncSettings settings)
        {
            foreach(var p in syncObjs.OneS_PersonsOrderedById.Values)
            {
                ITIS.Contact c = new Contact();
                //
                //
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

                c.ITISCareeStartDate = (DateTime)p.RelatedObjs_RelatedEmployeePositions.Min(e => e.DateOfEmployment);
                Boolean isActualEmployee = p.RelatedObjs_RelatedEmployeePositions.Any(e => e.DateOfDismisal == null || e.DateOfDismisal == default(DateTime));
                c.ITISCareerEndDate = isActualEmployee ? null : (DateTime?)p.RelatedObjs_RelatedEmployeePositions.Max(e => e.DateOfDismisal);
                
                OneS.Employee primaryPosition = OneS_GetPrimaryPosition(p, syncObjs, settings);
                if (primaryPosition != null)
                {
                    PositionInOrganization pos = primaryPosition.NavProp_CurrentPositionInOrganization;
                    c.ITISEmploymentTypeId = settings.Map_OneSEnum_EmploymentType_CreatioGuidOf_EmploymentType[primaryPosition.TypeOfEmployment];
                    c.JobTitle = pos.Description;
                    c.JobId = syncObjs.Creatio_Jobs_ByOneSId[pos.Id].Id;
                    c.ITISEmployeePositionId = syncObjs.Creatio_EmployeeJobs_ByOneSId[pos.Id].Id;
                    //
                    Organization org = primaryPosition.NavProp_Organization;
                    Account account = syncObjs.Creatio_Accounts_ByOneSId[org.Id];
                    c.AccountId = account.Id;
                    //
                    OrganizationSubdivision subDiv = primaryPosition.NavProp_CurrentOrganizationSubdivision;
                    c.ITISOrganizationSubdivisionId = syncObjs.Creatio_OrgStructureUnits_ByOneSId[subDiv.Id].Id;
                    c.ITISSubdivisionId = syncObjs.Creatio_AccountOrganizationCharts_ByOneSId[subDiv.Id].Id;

                }

                c = HttpClientOfCreatio.CreateObj(c);
                //
                BindCareerJobs(settings, syncObjs, p, c);
                HttpClientOfCreatio.UpdateObj(c);
                //ITIS.Employee employee = Creatio_AddNewEmployee(c, p);
                syncObjs.Creatio_ContractsOrderedByOneSId.Add((Guid)c.ITISOneSId, c);
                //syncObjs.Creatio_ContractsOrderedById.Add((Guid)c.Id, c);
            }
        }

        private static void Creatio_MapObj_Account(SyncObjs syncObjs, SyncSettings settings)
        {
            foreach(var x in syncObjs.OneS_Organizations.Values)
            {
                if(syncObjs.Creatio_Accounts_ByOneSId.ContainsKey(x.Id) || x.RelatedObj_Contractor == null) continue;
                ITIS.Account c = new ITIS.Account();
                //
                //
                c.ITISOneSId = x.Id;
                c.Name = x.FullName;
                c.AlternativeName = x.Description;
                c.TypeId = settings.CreatioGuidOfOurCompany;
                c.OwnershipId = settings.CreatioGuidOfLLCOwnershipType;
                c.KPP = x.RelatedObj_Contractor?.CodeOfEdrpo;
                c.INN = x.RelatedObj_Contractor?.INN;
                c.ITISInternalCode = x.Prefix;
                c.ITISCounterpartyLegalStatusId = settings.Map_OneSEnum_LegalStatus_CreatioGuidOf_ITISCounterpartyLegalStatus[true];
                //
                //
                c = HttpClientOfCreatio.CreateObj(c);
                syncObjs.Creatio_Accounts_ByOneSId.Add((Guid)c.ITISOneSId, c);
            }
        }


        private static void Creatio_MapObj_OrgStructureUnit(SyncObjs syncObjs, SyncSettings settings)
        {
            foreach(var x in syncObjs.OneS_Subdivisions.Values)
            {
                if (syncObjs.Creatio_OrgStructureUnits_ByOneSId.ContainsKey(x.Id)) continue;
                ITIS.OrgStructureUnit c = new ITIS.OrgStructureUnit();
                //
                c.ITISOneSId = (Guid)x.Id;
                c.FullName = x.Description;
                c.Name = x.Description;                
                //
                c = HttpClientOfCreatio.CreateObj(c);
                syncObjs.Creatio_OrgStructureUnits_ByOneSId.Add(c.ITISOneSId, c);
                //syncObjs.Creatio_OrgStructureUnits_ById.Add((Guid)c.Id, c);
            }
        }

        
        private static void Creatio_MapObj_AccountOrganizationChart(SyncObjs syncObjs, SyncSettings settings)
        {
            foreach(var x in syncObjs.OneS_PersonsOrderedById.Values)
            {
                foreach(var pos in x.RelatedObjs_RelatedEmployeePositions)
                {
                    var org = pos.NavProp_Organization;
                    var subDiv = pos.NavProp_CurrentOrganizationSubdivision;

                    if (syncObjs.Creatio_AccountOrganizationCharts_ByOneSId.ContainsKey(x.Id)) continue;
                    Terrasoft.AccountOrganizationChart c = new Terrasoft.AccountOrganizationChart();
                    //
                    c.AccountId = (Guid)syncObjs.Creatio_Accounts_ByOneSId[org.Id].Id;
                    ITIS.Department dep;
                    if (!syncObjs.Creatio_Departaments_ByOneSId.TryGetValue(subDiv.Id, out dep))
                    {
                        dep = new Department();
                        dep.ITISOneSId = subDiv.Id;
                        dep.Name = subDiv.Description;
                        //
                        dep = HttpClientOfCreatio.CreateObj(dep);
                        syncObjs.Creatio_Departaments_ByOneSId.Add(dep.ITISOneSId, dep);
                    }
                    else continue;
                    c.DepartmentId = dep.Id;
                    c.CustomDepartmentName = dep.Name;
                    //
                    c = HttpClientOfCreatio.CreateObj(c);
                    syncObjs.Creatio_AccountOrganizationCharts_ByOneSId.Add(dep.ITISOneSId, c);
                }
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
                //syncObjs.Creatio_EmployeeJobs_ById.Add((Guid)c.Id, c);
            }
        }

        
        
        private static void Creatio_MapObj_Job(SyncObjs syncObjs, SyncSettings settings)
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
                //syncObjs.Creatio_Jobs_ById.Add((Guid)c.Id, c);
            }
        }


        private static OneS.Employee OneS_GetPrimaryPosition(Person person, SyncObjs syncObjs, SyncSettings settings)
        {
            const String valueOfPrimaryPositionEnumInOneS = "ОсновноеМестоРаботы";
            DateTime defaultDateTime = default(DateTime);
            List<OneS.Employee> personPositionsOrderedByDescending = person.RelatedObjs_RelatedEmployeePositions.OrderByDescending(x => x.DateOfEmployment).ToList();
            OneS.Employee lastActualPrimaryPosition = null;
            OneS.Employee lastPosition = null;

            foreach (var position in personPositionsOrderedByDescending)
            {
                if (lastActualPrimaryPosition == null && (position.DueDate == null || position.DueDate == defaultDateTime))
                {
                    if (position.TypeOfEmployment == valueOfPrimaryPositionEnumInOneS) lastActualPrimaryPosition = position;
                    if (lastPosition == null) lastPosition = position;
                }
            }

            // Если возможно то присвоить последнюю актуальную должность с основным местом работы
            if (lastActualPrimaryPosition != null) lastPosition = lastActualPrimaryPosition;
            // Если актуальных должностей вообще нет просто задать самую крайнюю
            if (lastPosition == null) lastPosition = personPositionsOrderedByDescending.FirstOrDefault();
            return lastPosition;
        }

        private static void BindCareerJobs(SyncSettings settings, SyncObjs syncObjs, Person person, ITIS.Contact contact)
        {
            const String valueOfPrimaryPositionEnumInOneS = "ОсновноеМестоРаботы";
            DateTime defaultDateTime = default(DateTime);
            List<OneS.Employee> personPositionsOrderedByDescending = person.RelatedObjs_RelatedEmployeePositions.OrderByDescending(x => x.DateOfEmployment).ToList();
            OneS.Employee lastActualPrimaryPosition = null;
            OneS.Employee lastPosition = null;

            foreach (var position in personPositionsOrderedByDescending)
            {
                if (lastActualPrimaryPosition == null && (position.DueDate == null || position.DueDate == defaultDateTime))
                {
                    if (position.TypeOfEmployment == valueOfPrimaryPositionEnumInOneS) lastActualPrimaryPosition = position;
                    if (lastPosition == null) lastPosition = position;
                }
            }

            // Если возможно то присвоить последнюю актуальную должность с основным местом работы
            if (lastActualPrimaryPosition != null) lastPosition = lastActualPrimaryPosition;
            // Если актуальных должностей вообще нет просто задать самую крайнюю
            if (lastPosition == null) lastPosition = personPositionsOrderedByDescending.FirstOrDefault();
            if (lastPosition != null)
            {
                contact.ITISEmploymentTypeId = settings.Map_OneSEnum_EmploymentType_CreatioGuidOf_EmploymentType[lastPosition.TypeOfEmployment];
                contact.JobTitle = lastPosition.NavProp_Position.Description;
                ITIS.EmployeeJob job = new ITIS.EmployeeJob();
                job.Name = lastPosition.NavProp_Position.Description;
                job.ITISOneSId = lastPosition.NavProp_Position.Id;

                contact.ITISEmployeePosition = HttpClientOfCreatio.CreateObj(job);
                contact.ITISEmployeePositionId = contact.ITISEmployeePosition.Id;
            }
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




        public static void Creatio_ShowContact(ITIS.Contact contact, Boolean shouldShowMapingsOrRemarks = true)
        {
            contact?.Show(0, shouldShowMapingsOrRemarks);
            foreach(var career in contact?.Career)
            {
                career.Show(1, shouldShowMapingsOrRemarks);
                career.Account?.Show(2, shouldShowMapingsOrRemarks);
                career.OrgStructureUnit?.Show(2, shouldShowMapingsOrRemarks);
                career.ITISSubdivision?.Show(2, shouldShowMapingsOrRemarks);
                career.Job?.Show(2, shouldShowMapingsOrRemarks);
            }

            ITIS.Employee employee = (ITIS.Employee)contact?.Employee;
            employee?.Show(1, shouldShowMapingsOrRemarks);

            if(employee != null)
                foreach(var career in employee.Career)
                {
                    career.Show(2, shouldShowMapingsOrRemarks);
                    career.Account?.Show(3, shouldShowMapingsOrRemarks);
                    career.OrgStructureUnit?.Show(3, shouldShowMapingsOrRemarks);
                    career.EmployeeJob?.Show(3, shouldShowMapingsOrRemarks);
                }

            contact?.ITISSubdivision?.Show(1, shouldShowMapingsOrRemarks);
            contact?.ITISEmploymentType?.Show(1, shouldShowMapingsOrRemarks);
            contact?.ITISOrganizationSubdivision?.Show(1, shouldShowMapingsOrRemarks);
            contact?.Account?.Show(1, shouldShowMapingsOrRemarks);
            contact?.Type?.Show(1, shouldShowMapingsOrRemarks);
            (contact?.Job as ITIS.Job)?.Show(1, shouldShowMapingsOrRemarks);
            contact?.Department?.Show(1, shouldShowMapingsOrRemarks);
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
