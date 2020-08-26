namespace DatEx.Creatio.DataModel.Terrasoft.Base
{
    using System;
    using DatEx.Creatio.DataModel.Auxilary;
    using Newtonsoft.Json;

    /// <summary> Элемент организационной струкруры </summary>
    [CreatioType("Элемент организационной струкруры")]
    public class OrgStructureUnit : BaseEntity
    {
        /// <summary> Название </summary>
        [CreatioProp("Название", Color = ConsoleColor.Yellow)]
        public String Name { get; set; }

        /// <summary> Руководитель(Id) </summary>
        [JsonConverter(typeof(JsonConverter_Guid))]
        [CreatioProp("Руководитель(Id)")]
        public Guid? HeadId { get; set; }

        /// <summary> Руководитель </summary>
        [JsonIgnoreSerialization]
        [CreatioProp("Руководитель")]
        public Employee Head { get; set; }

        /// <summary> Родительское подразделение (Id) </summary>
        [JsonConverter(typeof(JsonConverter_Guid))]
        [CreatioProp("Родительское подразделение (Id)")]
        public Guid? ParentId { get; set; }

        /// <summary> Родительское подразделение </summary>
        [JsonIgnoreSerialization]
        [CreatioProp("Родительское подразделение")]
        public OrgStructureUnit Parent { get; set; }

        /// <summary> Полное название </summary>
        [CreatioProp("Полное название", Color = ConsoleColor.Yellow)]
        public String FullName { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
