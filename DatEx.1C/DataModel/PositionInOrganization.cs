
namespace DatEx.OneC.DataModel
{
    using DatEx.OneC.DataModel.Auxilary;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Text;

    [OneC("Catalog_ДолжностиОрганизаций", "Catalog_ДолжностиОрганизаций", "Справочник.ДолжностиОрганизаций", "Справочник.ДолжностиОрганизаций")]
    [JsonObject()]
    public class PositionInOrganization : OneCObject
    {
        [OneC("Boolean?", "АУП", "Булево", "АУП")]
        [JsonProperty("АУП")]
        public Boolean? IsAdministrativeOrManagementPersonnel { get; set; }

        [OneC("Guid?", "Категория_Key", "-", "-")]
        [JsonProperty("Категория_Key")]
        public Guid? CategoryId { get; set; }

        [OneC("?", "NavProp(Категория)", "Справочник.КатегорииДолжностей", "Категория")]
        [JsonIgnore]
        public Object NavProp_Category { get; set; }

        [OneC("Guid?", "Должность_Key", "-", "-")]
        [JsonProperty("Должность_Key")]
        public Guid? PositionId { get; set; }

        [OneC("?", "NavProp(Должность)", "Справочник.Должности", "Должность")]
        [JsonIgnore]
        public Object NavProp_Position { get; set; }

        [OneC("String", "КодКП", "Строка", "КодКП")]
        [JsonProperty("КодКП")]
        public String Prop { get; set; }

        [OneC("Boolean", "Шахтеры", "Булево", "Шахтеры")]
        [JsonProperty("")]
        public Boolean Miners { get; set; }

        [OneC("String", "НаименованиеПоКП", "Строка", "НаименованиеПоКП")]
        [JsonProperty("НаименованиеПоКП")]
        public String NameByProffessionsClasifier { get; set; }

        [OneC("String", "КодЗКППТР", "Строка", "КодЗКППТР")]
        [JsonProperty("КодЗКППТР")]
        public String CodeByZKPPTR { get; set; }

        [OneC("Boolean", "Кусто_Водители", "Булево", "Кусто_Водители")]
        [JsonProperty("Кусто_Водители")]
        public Boolean? KustoDrivers { get; set; }

        [OneC("Boolean", "Кусто_Агрономы", "Булево", "Кусто_Агрономы")]
        [JsonProperty("Кусто_Агрономы")]
        public Boolean? KustoAgronomists { get; set; }

        [OneC("Boolean", "Кусто_МенеджерыПоля", "Булево", "Кусто_МенеджерыПоля")]
        [JsonProperty("Кусто_МенеджерыПоля")]
        public Boolean? KustoFieldManagers { get; set; }

        [OneC("Boolean", "Кусто_Заправщики", "Булево", "Кусто_Заправщики")]
        [JsonProperty("Кусто_Заправщики")]
        public Boolean? KustoRefuellers { get; set; }
        // ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■

        //[OneC("String", "", "Строка", "")]
        //[JsonProperty("")]
        //public String Prop { get; set; }

        //[OneC("Boolean", "", "Булево", "")]
        //[JsonProperty("")]
        //public Boolean? Prop { get; set; }


        //[OneC("Guid?", "", "-", "-")]
        //[JsonProperty("")]
        //public Guid? PropId { get; set; }

        //[OneC("?", "NavProp()", "", "")]
        //[JsonIgnore]
        //public Object NavProp_ { get; set; }
    }
}
