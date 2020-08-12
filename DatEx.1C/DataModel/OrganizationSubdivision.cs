namespace DatEx.OneC.DataModel
{
    using DatEx.OneC.DataModel.Auxilary;
    using Newtonsoft.Json;
    using System;

    [OneC("Catalog_ПодразделенияОрганизаций", "Catalog_ПодразделенияОрганизаций", "", "")]
    [JsonObject("Catalog_ПодразделенияОрганизаций")]
    public class OrganizationSubdivision : OneCBaseLookup
    {
        /// <summary> Родитель </summary>
        [OneC("Guid?", "Parent_Key", "-", "-")]
        [JsonProperty("Parent_Key")]
        public Guid? ParentId { get; set; }

        [OneC("Guid?", "Owner_Key", "-", "-")]
        [JsonProperty("Owner_Key")]
        public Guid? OwnerId { get; set; }

        [OneC("String", "ВидПодразделения", "Перечисление.ВидыПодразделений", "ВидПодразделения")]
        [JsonProperty("ВидПодразделения")]
        public String SubdivisionType { get; set; }

        [OneC("Guid?", "НалоговоеНазначение_Key", "-", "-")]
        [JsonProperty("НалоговоеНазначение_Key")]
        public Guid? TaxPurposeId { get; set; }

        [OneC("?", "NavProp(НалоговоеНазначение)", "Справочник.НалоговыеНазначенияАктивовИЗатрат", "НалоговоеНазначение")]
        [JsonIgnore]
        public Object NavProp_TaxPurpos { get; set; }

        [OneC("Int64?", "Порядок", "Число", "Порядок")]
        [JsonProperty("Порядок")]
        public Int64? Order { get; set; }

        [OneC("Guid?", "ИНАГРО_ВидДеятельности_Key", "-", "-")]
        [JsonProperty("ИНАГРО_ВидДеятельности_Key")]
        public Guid? InagroActivityId { get; set; }

        [OneC("?", "NavProp(ИНАГРО_ВидДеятельности)", "Справочник.ИНАГРО_ВидыДеятельности", "ИНАГРО_ВидДеятельности")]
        [JsonIgnore]
        public Object NavProp_InagroActivity { get; set; }

        [OneC("Guid?", "ИНАГРО_Подразделение_Key", "-", "-")]
        [JsonProperty("ИНАГРО_Подразделение_Key")]
        public Guid? InagroSubdivisionId { get; set; }

        [OneC("?", "NavProp(ИНАГРО_Подразделение)", "Справочник.Подразделения", "ИНАГРО_Подразделение")]
        [JsonIgnore]
        public Object NavProp_InagroSubdivision { get; set; }
        


        public override string ToString() => $"{Description} ({SubdivisionType})";
    }
}
