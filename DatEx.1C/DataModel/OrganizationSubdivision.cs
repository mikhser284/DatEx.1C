using DatEx.OneC.DataModel.Auxilary;

namespace DatEx.OneC.DataModel
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Text;

    
    [JsonObject("Catalog_ПодразделенияОрганизаций")]
    public class OrganizationSubdivision : OneCBaseLookup
    {
        /// <summary> Родитель </summary>
        
        [JsonProperty("Parent_Key")]
        public Guid? ParentId { get; set; }


        
        [JsonProperty("Owner_Key")]
        public Guid? OwnerId { get; set; }

        
        [JsonProperty("ВидПодразделения")]
        public String SubdivisionType { get; set; }

        
        [JsonProperty("НалоговоеНазначение_Key")]
        public Guid? TaxPurposeId { get; set; }

        
        [JsonProperty("Порядок")]
        public Int64? Order { get; set; }

        
        [JsonProperty("ИНАГРО_ВидДеятельности_Key")]
        public Guid? InagroActivityId { get; set; }

        
        [JsonProperty("ИНАГРО_Подразделение_Key")]
        public Guid? InagroSubdivisionId { get; set; }

        public override string ToString()
        {
            return $"{Description} ({SubdivisionType})";
        }
    }
}
