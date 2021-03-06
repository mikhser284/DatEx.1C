﻿
namespace DatEx.Creatio.DataModel.ITIS
{
    using System;
    using DatEx.Creatio.DataModel.Auxilary;
    using Newtonsoft.Json;

    /// <summary> Карьера сотрудника в нашей компании </summary>
    [CreatioType("Карьера сотрудника в нашей компании")]
    public class EmployeeCareer : DatEx.Creatio.DataModel.Terrasoft.Base.EmployeeCareer
    {
        /// <summary> Основное </summary>
        [MapRemarks("Если значение перечисления ВидыЗанятостиВОрганизации равно 'ОсновноеМестоРаботы' - true, иначе false")]
        [Map(true, DataType.Lookup, "Catalog_СотрудникиОрганизаций", DataType.Enum, "Перечисление.ВидыЗанятостиВОрганизации/ВидЗанятости")]
        [CreatioProp("Основное", Color = ConsoleColor.Yellow)]
        public Boolean? ITISPrimary { get; set; }

        /// <summary> Вид занятости (Id) </summary>
        [JsonConverter(typeof(JsonConverter_Guid))]
        [MapRemarks("Значение преобразуется в Id объекта Creatio используя словарь Map_OneSEnum_EmploymentType_CreatioGuidOf_EmploymentType из объекта настроек синхронизации")]
        [Map(true, DataType.Lookup, "Catalog_СотрудникиОрганизаций", DataType.String, "ВидЗанятости")]
        [CreatioProp("Вид занятости (Id)", Color = ConsoleColor.Blue)]
        public Guid? ITISTypeOfEmploymentId { get; set; }

        /// <summary> Вид занятости </summary>
        [JsonIgnoreSerialization]
        [CreatioProp("Вид занятости", Color = ConsoleColor.Yellow)]
        public ITISEmploymentType ITISTypeOfEmployment { get; set; }

        /// <summary> Деактивирована </summary>
        [CreatioProp("Деактивирована")]
        public Boolean? RecordInactive { get; set; }

        [JsonConverter(typeof(JsonConverter_Guid))]
        [Map(true, DataType.Lookup, "Catalog_СотрудникиОрганизаций", DataType.Guid, "Ref_Key")]
        [CreatioPropNotExistInDataModelOfITIS]
        [CreatioProp("Идентификатор объекта в 1С", Remarks = "Поле отсутствует в оригинальном пакете 'ItisWorkFlowBase'", Color = ConsoleColor.Red)]
        public Guid? ITISOneSId { get; set; }
    }
}
