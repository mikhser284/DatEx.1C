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
        
        [JsonIgnore]
        public IRNamesOfPersons NameInfo_ { get; set; }

        
        [JsonIgnore]
        public IRContactInfo ContactInfoEmail_ { get; set; }

        
        [JsonIgnore]
        public IRContactInfo _ContactInfoPhone { get; set; }

        
        [JsonIgnore]
        public IRContactInfo _ContactInfoWorkPhone { get; set; }

        
        [JsonProperty("IdCreatio")]
        public String IdCreatio { get; set; }

        
        [JsonProperty("ДатаРождения")]
        public DateTime? DateOfBirth { get; set; }

        
        [JsonProperty("ИНН")]
        public String INN { get; set; }

        
        [JsonProperty("Комментарий")]
        public String Remarks { get; set; }

        
        [JsonProperty("Пол")]
        public String Gender { get; set; }

        
        [JsonProperty("МестоРождения")]
        public String PlaceOfBirth { get; set; }

        
        [JsonProperty("ОсновноеИзображение_Key")]
        public Guid? PrimaryImageId { get; set; }

        
        [JsonProperty("КодПоДРФО")]
        public String CodeByDrfo { get; set; }

        
        [JsonProperty("ГруппаДоступаФизическогоЛица_Key")]
        public Guid? PersonAccessGroupId { get; set; }

        
        [JsonProperty("СоставСемьи")]
        public List<Object> FamilyComposition { get; set; }

        
        [JsonProperty("Образование")]
        public List<Object> Education { get; set; }

        
        [JsonProperty("ТрудоваяДеятельность")]
        public List<Object> LaborActivity { get; set; }

        
        [JsonProperty("ЗнаниеЯзыков")]
        public List<Object> KnowledgeOfLanguages { get; set; }

        
        [JsonProperty("Профессии")]
        public List<Object> Professions { get; set; }

        
        [JsonProperty("Стажи")]
        public List<Object> Experiences { get; set; }

        
        [JsonProperty("Parent")]
        public Person Parent { get; set; }

        public override String ToString() => $"{Description}";
    }
}
