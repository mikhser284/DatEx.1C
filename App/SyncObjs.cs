using System;
using System.Collections.Generic;
using System.Text;
using OneC = DatEx.OneC.DataModel;
using ITIS = DatEx.Creatio.DataModel.ITIS;
using Terrasoft = DatEx.Creatio.DataModel.Terrasoft.Base;

namespace App
{
    /// <summary> Временные объекты, используемые на протяжении синхронизации </summary>
    public class SyncObjs
    {
        /// <summary> 1C Физ. лица упорядоченные по Id </summary>
        public Dictionary<Guid, OneC.Person> OneS_PersonsOrderedById { get; set; } = new Dictionary<Guid, OneC.Person>();

        /// <summary> 1C Контактная информация сгрупированная по Id физ. лица </summary>
        public Dictionary<Guid, List<OneC.IRContactInfo>> OneS_ContactInfosGroupedByPersonId { get; set; } = new Dictionary<Guid, List<OneC.IRContactInfo>>();

        /// <summary> 1С данные из регистра сведений ФиоФизЛиц </summary>
        public List<OneC.IRNamesOfPersons> OneS_NamesOfPersons { get; set; } = new List<OneC.IRNamesOfPersons>();

        /// <summary> 1С даные из справочника СотрудникиОрганизаций </summary>
        public List<OneC.Employee> OneS_Employees { get; set; } = new List<OneC.Employee>();

        /// <summary> 1С даные из справочника ВидыКонтактнойИнформации </summary>
        public Dictionary<Guid, OneC.ContactInfoType> OneS_ContactInfoTypes = new Dictionary<Guid, OneC.ContactInfoType>();

        /// <summary> 1С даные из справочника Организации </summary>
        public Dictionary<Guid, OneC.Organization> OneS_Organizations = new Dictionary<Guid, OneC.Organization>();

        /// <summary> 1С даные из справочника ПодразделенияОрганизаций </summary>
        public Dictionary<Guid, OneC.OrganizationSubdivision> OneS_Subdivisions = new Dictionary<Guid, OneC.OrganizationSubdivision>();

        /// <summary> 1С даные из справочника ДолжностиОрганизаций </summary>
        public Dictionary<Guid, OneC.PositionInOrganization> OneS_Positions = new Dictionary<Guid, OneC.PositionInOrganization>();
    }
}
