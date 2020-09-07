
namespace DatEx.OneS.DataModel
{
    using DatEx.OneS.DataModel.Auxilary;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Text;

    [OneS("Catalog_ДолжностиОрганизаций", "Catalog_ДолжностиОрганизаций", "Справочник.ДолжностиОрганизаций", "Справочник.ДолжностиОрганизаций")]
    [JsonObject("ДолжностиОрганизаций")]
    public class PositionInOrganization : OneCBaseLookup
    {
        [OneS("Boolean?", "АУП", "Булево", "АУП")]
        [JsonProperty("АУП")]
        public Boolean? IsAdministrativeOrManagementPersonnel { get; set; }

        [OneS("Guid?", "Категория_Key", "-", "-")]
        [JsonProperty("Категория_Key")]
        public Guid? CategoryId { get; set; }

        [OneS("?", "NavProp(Категория)", "Справочник.КатегорииДолжностей", "Категория")]
        [JsonIgnore]
        public Object NavProp_Category { get; set; }

        [OneS("Guid?", "Должность_Key", "-", "-")]
        [JsonProperty("Должность_Key")]
        public Guid? PositionId { get; set; }

        [OneS("?", "NavProp(Должность)", "Справочник.Должности", "Должность")]
        [JsonIgnore]
        public Object NavProp_Position { get; set; }

        [OneS("String", "КодКП", "Строка", "КодКП")]
        [JsonProperty("КодКП")]
        public String Prop { get; set; }

        [OneS("Boolean", "Шахтеры", "Булево", "Шахтеры")]
        [JsonProperty("")]
        public Boolean Miners { get; set; }

        [OneS("String", "НаименованиеПоКП", "Строка", "НаименованиеПоКП")]
        [JsonProperty("НаименованиеПоКП")]
        public String NameByProffessionsClasifier { get; set; }

        [OneS("String", "КодЗКППТР", "Строка", "КодЗКППТР")]
        [JsonProperty("КодЗКППТР")]
        public String CodeByZKPPTR { get; set; }

        [OneS("Boolean", "Кусто_Водители", "Булево", "Кусто_Водители")]
        [JsonProperty("Кусто_Водители")]
        public Boolean? KustoDrivers { get; set; }

        [OneS("Boolean", "Кусто_Агрономы", "Булево", "Кусто_Агрономы")]
        [JsonProperty("Кусто_Агрономы")]
        public Boolean? KustoAgronomists { get; set; }

        [OneS("Boolean", "Кусто_МенеджерыПоля", "Булево", "Кусто_МенеджерыПоля")]
        [JsonProperty("Кусто_МенеджерыПоля")]
        public Boolean? KustoFieldManagers { get; set; }

        [OneS("Boolean", "Кусто_Заправщики", "Булево", "Кусто_Заправщики")]
        [JsonProperty("Кусто_Заправщики")]
        public Boolean? KustoRefuellers { get; set; }

        
    }
}
