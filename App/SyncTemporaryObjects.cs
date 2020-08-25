using System;
using System.Collections.Generic;
using System.Text;
using OneC = DatEx.OneC.DataModel;
using ITIS = DatEx.Creatio.DataModel.ITIS;
using Terrasoft = DatEx.Creatio.DataModel.Terrasoft.Base;

namespace App
{
    /// <summary> Временные объекты, используемые на протяжении синхронизации </summary>
    public class SyncTemporaryObjects
    {
        public Dictionary<Guid, ITIS.ITISEmploymentType> EmploymentTypes { get; set; }  = new Dictionary<Guid, ITIS.ITISEmploymentType>();
    }
}
