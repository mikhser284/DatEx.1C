namespace DatEx.OneC.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DatEx.OneC.DataModel.Auxilary;
    using Newtonsoft.Json;

    [OneC("Catalog_ФизическиеЛица", "Catalog_ФизическиеЛица", "Справочник.ФизическиеЛица", "Справочник.ФизическиеЛица")]
    [JsonObject("Catalog_ФизическиеЛица")]
    public class Person : OneCBaseHierarchicalLookup
    {
        /// <summary> Список соттудников (и их должностей) которые соответствуют одному физ. лицу </summary>
        [JsonIgnore]
        public List<Employee> RelatedObjs_RelatedEmployeePositions { get; set; } = new List<Employee>();

        [OneC("InformationRegister_ФИОФизЛиц", "-", "РегистрСведений.ФИОФизЛиц", "-", Remarks = "Прямая связь между объектами отсутствует", Color = ConsoleColor.Green)]
        [JsonIgnore]
        public IRNamesOfPersons RelatedObj_NameInfo { get; set; }

        [OneC("InformationRegister_КонтактнаяИнформация", "-", "РегистрСведений.КонтактнаяИнформация", "-", Remarks = "Прямая связь между объектами отсутствует", Color = ConsoleColor.Green)]
        [JsonIgnore]
        public IRContactInfo RelatedObj_ContactInfoEmail { get; set; }

        [OneC("InformationRegister_КонтактнаяИнформация", "-", "РегистрСведений.КонтактнаяИнформация", "-", Remarks = "Прямая связь между объектами отсутствует", Color = ConsoleColor.Green)]
        [JsonIgnore]
        public IRContactInfo RelatedObj_ContactInfoPhone { get; set; }

        [OneC("InformationRegister_КонтактнаяИнформация", "-", "РегистрСведений.КонтактнаяИнформация", "-", Remarks = "Прямая связь между объектами отсутствует", Color = ConsoleColor.Green)]
        [JsonIgnore]
        public IRContactInfo RelatedObj_ContactInfoWorkPhone { get; set; }

        [OneC("DateTime?", "ДатаРождения", "Дата", "ДатаРождения", Color = ConsoleColor.Green)]
        [JsonProperty("ДатаРождения")]
        public DateTime? BirthDate { get; set; }

        [OneC("String", "ИНН", "Строка", "ИНН", Color = ConsoleColor.Green)]
        [JsonProperty("ИНН")]
        public String INN { get; set; }

        [OneC("String", "Комментарий", "Строка", "Комментарий")]
        [JsonProperty("Комментарий")]
        public String Remarks { get; set; }

        [OneC("String", "Пол", "ПеречислениеСсылка.ПолФизическихЛиц", "Пол", Color = ConsoleColor.Green)]
        [JsonProperty("Пол")]
        public String Gender { get; set; }

        [OneC("String", "МестоРождения", "Строка", "МестоРождения")]
        [JsonProperty("МестоРождения")]
        public String PlaceOfBirth { get; set; }

        [OneC("Guid?", "ОсновноеИзображение_Key", "-", "-")]
        [JsonProperty("ОсновноеИзображение_Key")]
        public Guid? PrimaryImageId { get; set; }

        [OneC("?", "NavProp(ОсновноеИзображение)", "Справочник.ХранилищеДополнительнойИнформации", "ОсновноеИзображение")]
        [JsonIgnore]
        public Object NavProp_PrimaryImage { get; set; }

        [OneC("String", "КодПоДРФО", "Строка", "КодПоДРФО")]
        [JsonProperty("КодПоДРФО")]
        public String CodeByDrfo { get; set; }

        [OneC("Guid?", "ГруппаДоступаФизическогоЛица_Key", "-", "-")]
        [JsonProperty("ГруппаДоступаФизическогоЛица_Key")]
        public Guid? PersonAccessGroupId { get; set; }

        [OneC("?", "NavProp(ГруппаДоступаФизическогоЛица)", "СправочникСсылка.ГруппыДоступаФизическихЛиц", "ГруппаДоступаФизическогоЛица")]
        [JsonIgnore]
        public Object NavProp_PersonAccessGroup { get; set; }
        
        [OneC("String", "IdCreatio", "Строка", "IdCreatio")]
        [JsonProperty("IdCreatio")]
        
        public String IdCreatio { get; set; }
        
        public override String ToString() => $"{Description}";

        public String GetShortNameAndActualPositions()
        {
            String shortNameString = $"{RelatedObj_NameInfo.Surname} {RelatedObj_NameInfo.GivenName?[0]}.{RelatedObj_NameInfo.MiddleName?[0]}.";
            var actualPositions = RelatedObjs_RelatedEmployeePositions.Where(x => x.DateOfDismisal is null || x.DateOfDismisal == default(DateTime)).OrderBy(x => x.NavProp_Organization.Prefix).ToList();
            String actualPositionsString = $"{String.Join("; ", actualPositions.Select(x => $"{x.NavProp_CurrentPositionInOrganization} [{x.NavProp_Organization.Prefix}]"))}";
            return $"{shortNameString} ({actualPositionsString})";
        }
    }
}
