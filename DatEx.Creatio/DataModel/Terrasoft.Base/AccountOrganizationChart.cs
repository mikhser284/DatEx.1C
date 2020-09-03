namespace DatEx.Creatio.DataModel.Terrasoft.Base
{
    using System;
    using System.Drawing;
    using System.Text.Json.Serialization;
    using DatEx.Creatio.DataModel.Auxilary;

    /// <summary> Структура организации контрагента </summary>
    [CreatioType("Структура организации контрагента")]
    public class AccountOrganizationChart : BaseEntity
    {
        /// <summary> Контрагент (Id) </summary>
        [MapRemarks("Объект Контрагент должен быть уже синхронизирован с 1С, здесь же необходимо найти ранее созданный объект по его ITISOneSId и присвоить его Id в Creatio")]
        [Map(true)]
        [JsonConverter(typeof(JsonConverter_Guid))]
        [CreatioProp("Контрагент (Id)", Color = ConsoleColor.Blue)]
        public Guid? AccountId { get; set; }



        /// <summary> Контрагент </summary>
        [JsonIgnoreSerialization]
        [CreatioProp("Контрагент")]
        public Account Account { get; set; }

        
        
        /// <summary> Подразделение </summary>
        [CreatioProp("Подразделение")]
        public String CustomDepartmentName { get; set; }



        /// <summary> Департамент (Id) </summary>        
        [JsonConverter(typeof(JsonConverter_Guid))]
        [MapRemarks("Объект Департамент быть уже синхронизирован с 1С, здесь же необходимо найти ранее созданный объект по его ITISOneSId и присвоить его Id в Creatio")]
        [Map(true)]
        [CreatioProp("Департамент (Id)", Color = ConsoleColor.Blue)]
        public Guid? DepartmentId { get; set; }



        /// <summary> Департамент </summary>
        [JsonIgnoreSerialization]
        [CreatioProp("Департамент")]
        public Department Department { get; set; }



        /// <summary> Руководитель (Id) </summary>
        [JsonConverter(typeof(JsonConverter_Guid))]
        [CreatioProp("Руководитель (Id)")]
        public Guid? ManagerId { get; set; }

        
        
        /// <summary> Руководитель </summary>
        [CreatioProp("Руководитель")]
        public Contact Manager { get; set; }

        
        
        /// <summary> Описание </summary>
        [CreatioProp("Описание")]
        public String Description { get; set; }



        /// <summary> Родительское подразделение (Id) </summary>
        [JsonConverter(typeof(JsonConverter_Guid))]
        [CreatioProp("Родительское подразделение (Id)")]
        public Guid? ParentId { get; set; }



        /// <summary> Родительское подразделение </summary>
        [JsonIgnoreSerialization]
        [CreatioProp("Родительское подразделение")]
        public AccountOrganizationChart Parent { get; set; }

        public override string ToString()
        {
            return CustomDepartmentName;
        }
    }
}
