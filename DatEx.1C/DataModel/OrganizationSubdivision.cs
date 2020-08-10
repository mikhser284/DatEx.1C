using DatEx.OneC.DataModel.Auxilary;

namespace DatEx.OneC.DataModel
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Text;


    [JsonObject("Catalog_Организации")]
    public class OrganizationSubdivision : OneCBaseLookup
    {
        /// <summary> Родитель </summary>
        [CreatioAux]
        [JsonProperty("Parent_Key")]
        public Guid? ParentId { get; set; }


        [CreatioAux]
        [JsonProperty("Owner_Key")]
        public Guid? OwnerId { get; set; }

        [CreatioAux]
        [JsonProperty("ВидПодразделения")]
        public String SubdivisionType { get; set; }

        [CreatioAux]
        [JsonProperty("НалоговоеНазначение_Key")]
        public Guid? TaxPurposeId { get; set; }

        [CreatioAux]
        [JsonProperty("Порядок")]
        public Int64? Order { get; set; }

        [CreatioAux]
        [JsonProperty("ИНАГРО_ВидДеятельности_Key")]
        public Guid? InagroActivityId { get; set; }

        [CreatioAux]
        [JsonProperty("ИНАГРО_Подразделение_Key")]
        public Guid? InagroSubdivisionId { get; set; }

        public override string ToString()
        {
            return $"{Description} ({SubdivisionType})";
        }
    }
}
