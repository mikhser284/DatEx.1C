using System;
using System.Collections.Generic;
using OneS = DatEx.OneS.DataModel;
using ITIS = DatEx.Creatio.DataModel.ITIS;
using Terrasoft = DatEx.Creatio.DataModel.Terrasoft.Base;
using DatEx.OneS.DataModel;
using DatEx.Creatio.DataModel.Terrasoft.Base;

namespace App
{
    /// <summary> Временные объекты, используемые на протяжении синхронизации </summary>
    public class SyncObjs_SyncEmployeesAndCareers
    {
        /// <summary> 1С даные из справочника ПодразделенияОрганизаций </summary>
        public Dictionary<Guid, OneS.OrganizationSubdivision> OneS_Subdivisions = new Dictionary<Guid, OneS.OrganizationSubdivision>();
        public Dictionary<Guid, ITIS.OrgStructureUnit> Creatio_OrgStructureUnits_ByOneSId = new Dictionary<Guid, ITIS.OrgStructureUnit>();
        public Dictionary<Guid, ITIS.Department> Creatio_Departaments_ByOneSId = new Dictionary<Guid, ITIS.Department>();
        public Dictionary<Guid, AccountOrganizationChart> Creatio_AccountOrganizationCharts_ByOneSId = new Dictionary<Guid, AccountOrganizationChart>();
        



        /// <summary> 1С даные из справочника ДолжностиОрганизаций </summary>
        public Dictionary<Guid, OneS.PositionInOrganization> OneS_Positions = new Dictionary<Guid, OneS.PositionInOrganization>();
        public Dictionary<Guid, ITIS.Job> Creatio_Jobs_ByOneSId = new Dictionary<Guid, ITIS.Job>();
        public Dictionary<Guid, ITIS.EmployeeJob> Creatio_EmployeeJobs_ByOneSId = new Dictionary<Guid, ITIS.EmployeeJob>();



        /// <summary> 1С даные из справочника Организации </summary>
        public Dictionary<Guid, OneS.Organization> OneS_Organizations = new Dictionary<Guid, OneS.Organization>();
        public Dictionary<Guid, ITIS.Account> Creatio_Accounts_ByOneSId = new Dictionary<Guid, ITIS.Account>();



        /// <summary> 1C Физ. лица упорядоченные по Id </summary>
        public Dictionary<Guid, OneS.Person> OneS_PersonsOrderedById { get; set; } = new Dictionary<Guid, OneS.Person>();
        public Dictionary<Guid, ITIS.Contact> Creatio_ContractsOrderedByOneSId = new Dictionary<Guid, ITIS.Contact>();
        public Dictionary<Guid, List<ITIS.ContactCareer>> Creatio_ContactCareersGrouppedByContactId = new Dictionary<Guid, List<ITIS.ContactCareer>>();
        public Dictionary<Guid, ITIS.Employee> Creatio_EmployeesOrderedByContactId = new Dictionary<Guid, ITIS.Employee>();
        public Dictionary<Guid, List<ITIS.EmployeeCareer>> Creatio_EmployeeCareersGrouppedByEmployeeId = new Dictionary<Guid, List<ITIS.EmployeeCareer>>();

        

        /// <summary> 1С регистр сведений Собственные контрагенты </summary>
        public Dictionary<Guid, IROwnContracror> OneS_IROwnContractorsOrderedByOrganizationId { get; set; } = new Dictionary<Guid, IROwnContracror>();


        /// <summary> 1C Контактная информация сгрупированная по Id физ. лица </summary>
        public Dictionary<Guid, List<OneS.IRContactInfo>> OneS_ContactInfosGroupedByPersonId { get; set; } = new Dictionary<Guid, List<OneS.IRContactInfo>>();


        /// <summary> 1С данные из регистра сведений ФиоФизЛиц </summary>
        public List<OneS.IRNamesOfPersons> OneS_NamesOfPersons { get; set; } = new List<OneS.IRNamesOfPersons>();

        /// <summary> 1С даные из справочника СотрудникиОрганизаций </summary>
        public List<OneS.Employee> OneS_Employees { get; set; } = new List<OneS.Employee>();

        /// <summary> 1С даные из справочника ВидыКонтактнойИнформации </summary>
        public Dictionary<Guid, OneS.ContactInfoType> OneS_ContactInfoTypes = new Dictionary<Guid, OneS.ContactInfoType>();

        /// <summary> 1С даные из справочника ВидыКонтактнойИнформации </summary>
        public Dictionary<Guid, OneS.Contractor> OneS_ContractorsOrderedById = new Dictionary<Guid, Contractor>();
    }
}
