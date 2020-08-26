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

        public List<OneC.IRNamesOfPersons> OneS_NamesOfPersons { get; set; }  = new List<OneC.IRNamesOfPersons>();

    }
}
