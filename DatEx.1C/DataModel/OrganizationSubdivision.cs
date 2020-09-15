namespace DatEx.OneS.DataModel
{
    using DatEx.OneS.DataModel.Auxilary;
    using Newtonsoft.Json;
    using System;

    [OneS("Catalog_ПодразделенияОрганизаций", "Catalog_ПодразделенияОрганизаций", "Справочник.ПодразделенияОрганизаций", "Справочник.ПодразделенияОрганизаций")]
    [JsonObject("Catalog_ПодразделенияОрганизаций")]
    public class OrganizationSubdivision : OneSBaseLookup
    {
        /// <summary> Родитель </summary>
        [OneS("Guid?", "Parent_Key", "-", "-")]
        [JsonProperty("Parent_Key")]
        public Guid? ParentId { get; set; }

        [OneS("Guid?", "Owner_Key", "-", "-")]
        [JsonProperty("Owner_Key")]
        public Guid? OwnerId { get; set; }

        [OneS("String", "ВидПодразделения", "Перечисление.ВидыПодразделений", "ВидПодразделения")]
        [JsonProperty("ВидПодразделения")]
        public String SubdivisionType { get; set; }

        [OneS("Guid?", "НалоговоеНазначение_Key", "-", "-")]
        [JsonProperty("НалоговоеНазначение_Key")]
        public Guid? TaxPurposeId { get; set; }

        [OneS("?", "NavProp(НалоговоеНазначение)", "Справочник.НалоговыеНазначенияАктивовИЗатрат", "НалоговоеНазначение")]
        [JsonIgnore]
        public Object NavProp_TaxPurpos { get; set; }

        [OneS("Int64?", "Порядок", "Число", "Порядок")]
        [JsonProperty("Порядок")]
        public Int64? Order { get; set; }

        [OneS("Guid?", "ИНАГРО_ВидДеятельности_Key", "-", "-")]
        [JsonProperty("ИНАГРО_ВидДеятельности_Key")]
        public Guid? InagroActivityId { get; set; }

        [OneS("?", "NavProp(ИНАГРО_ВидДеятельности)", "Справочник.ИНАГРО_ВидыДеятельности", "ИНАГРО_ВидДеятельности")]
        [JsonIgnore]
        public Object NavProp_InagroActivity { get; set; }

        [OneS("Guid?", "ИНАГРО_Подразделение_Key", "-", "-")]
        [JsonProperty("ИНАГРО_Подразделение_Key")]
        public Guid? InagroSubdivisionId { get; set; }

        [OneS("?", "NavProp(ИНАГРО_Подразделение)", "Справочник.Подразделения", "ИНАГРО_Подразделение")]
        [JsonIgnore]
        public Object NavProp_InagroSubdivision { get; set; }
        


        public override string ToString() => $"{Description} ({SubdivisionType})";
    }
}
