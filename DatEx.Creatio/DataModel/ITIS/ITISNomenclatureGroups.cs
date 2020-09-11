namespace DatEx.Creatio.DataModel.ITIS
{
    using System;
    using DatEx.Creatio.DataModel.Auxilary;
    using Newtonsoft.Json;
    using Terrasoft = DatEx.Creatio.DataModel.Terrasoft.Base;


    /// <summary> Статья закупоки </summary>
    [CreatioType("Статья закупок")]
    public class ITISNomenclatureGroups : Terrasoft.BaseEntity
    {
        /// <summary> Название </summary>
        [Map()]
        [CreatioProp("Название")]
        public String ITISName { get; set; }



        /// <summary> Заметки </summary>
        [Map()]
        [CreatioProp("Заметки")]
        public String ITISNotes { get; set; }



        /// <summary> Родительская група Id </summary>
        [JsonConverter(typeof(JsonConverter_Guid))]
        [Map()]
        [CreatioProp("Родительская група Id")]
        public Guid? ITISParentGroupId { get; set; }




        /// <summary> Родительская група </summary>
        [JsonConverter(typeof(JsonConverter_Guid))]
        [CreatioProp("Родительская група")]
        public ITISNomenclatureGroups ITISParentGroup { get; set; }



        /// <summary> Ответственный за проверку (Id) </summary>
        [JsonConverter(typeof(JsonConverter_Guid))]
        [CreatioProp("Ответственный за проверку Id")]
        public Guid? ITISResponsibleForVerificationId { get; set; }



        /// <summary> Ответственный за проверку </summary>
        [JsonIgnoreSerialization]
        [CreatioProp("Ответственный за проверку")]
        public Contact ITISResponsibleForVerification { get; set; }



        /// <summary> Код 1С </summary>
        [JsonIgnoreSerialization]
        [Obsolete("У свойства Код 1С не верно задан тип данных - String в место Guid")]
        [ObsoleteCreatioProp("Код 1С")]
        [CreatioProp("Код 1С")]
        public Contact ITISOneCCode { get; set; }



        /// <summary> Деактивирована </summary>
        [JsonIgnoreSerialization]
        [CreatioProp("Деактивирована")]
        public Contact RecordInactive { get; set; }



        /// <summary> Id объекта в 1C </summary>
        [Map()]
        [CreatioPropNotExistInDataModelOfITIS]
        [CreatioProp("Guid", "Id объекта в 1C", Color = ConsoleColor.Red)]
        public Guid ITISOneSId { get; set; }

    }
}
