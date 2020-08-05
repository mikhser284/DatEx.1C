namespace DatEx.OneC.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using DatEx.OneC.DataModel.Auxilary;
    using Newtonsoft.Json;

    [JsonObject("Catalog_ФизическиеЛица")]
    public class Person : OneCBaseHierarchicalLookup
    {
        [CreatioPropertyMap("Guid", "Id", "IdCreatio")]
        [JsonProperty("IdCreatio")]
        public String IdCreatio { get; set; }

        [CreatioAux]
        [JsonProperty("ДатаРождения")]
        public DateTime? DateOfBirth { get; set; }

        [CreatioAux]
        [JsonProperty("ИНН")]
        public String INN { get; set; }

        [CreatioAux]
        [JsonProperty("")]
        public String PropName { get; set; }
        
        [CreatioAux]
        [JsonProperty("Комментарий")]
        public String Remarks { get; set; }
        
        [CreatioAux]
        [JsonProperty("Пол")]
        public String Gender { get; set; }
        
        [CreatioAux]
        [JsonProperty("МестоРождения")]
        public String PlaceOfBirth { get; set; }
        
        [CreatioIgnore]
        [JsonProperty("ОсновноеИзображение_Key")]
        public Guid? PrimaryImageId { get; set; }
        
        [CreatioAux]
        [JsonProperty("КодПоДРФО")]
        public String CodeByDrfo { get; set; }
        
        [CreatioAux]
        [JsonProperty("ГруппаДоступаФизическогоЛица_Key")]
        public Guid? PersonAccessGroupId { get; set; }
        
        [CreatioAux]
        [JsonProperty("СоставСемьи")]
        public List<Object> FamilyComposition { get; set; }
        
        [CreatioAux]
        [JsonProperty("Образование")]
        public List<Object> Education { get; set; }
        
        [CreatioAux]
        [JsonProperty("ТрудоваяДеятельность")]
        public List<Object> LaborActivity { get; set; }

        [CreatioAux]
        [JsonProperty("ЗнаниеЯзыков")]
        public List<Object> KnowledgeOfLanguages { get; set; }

        [CreatioAux]
        [JsonProperty("Профессии")]
        public List<Object> Professions { get; set; }

        [CreatioAux]
        [JsonProperty("Стажи")]
        public List<Object> Experiences { get; set; }

        [CreatioAux]
        [JsonProperty("Parent")]
        public Person Parent { get; set; }

        [JsonIgnore]
        public IRContactInfo ContactInfoEmail { get; set; }

        [JsonIgnore]
        public IRContactInfo ContactInfoPhone { get; set; }

        [JsonIgnore]
        public IRContactInfo ContactInfoWorkPhone { get; set; }
    }
}
